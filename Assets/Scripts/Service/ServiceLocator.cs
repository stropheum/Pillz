using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pillz.Service
{
    public sealed partial class ServiceLocator : PersistentSingleton<ServiceLocator>
    {
        [SerializeField] private Service[] _startupServices;
        private readonly Dictionary<Type, Service> _services = new();

        private IEnumerator Start()
        {
            yield return InitializeStartupServices();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AutoInitialize()
        {
            if (IsValid) { return; }

            Debug.Log("[ServiceLocator] No scene instance found. Auto-initializing ServiceLocator");
            Initialize();
            // TODO: Instead of ServiceLocator Auto initializing, maybe we simply have a bootstrapper  
        }

        private IEnumerator InitializeStartupServices()
        {
            if (_startupServices == null || !Application.isPlaying) { yield break; }

            foreach (Service service in _startupServices)
            {
                Type serviceType = service.GetType();
                Debug.Assert(!_services.ContainsKey(serviceType), "Service " + serviceType.Name + " is already registered");
                
                Service currentService = Instantiate(service, transform);
                _services[serviceType] = currentService; 
                yield return new WaitUntil(() => currentService != null && currentService.Initialized);
            }
            
            // TODO: Implement game event dispatcher and a bootstrap script that has guaranteed first script execution, so it can listen to an event from this and load landing page
            SceneManager.LoadScene("MainMenu");
        }

        public void Register<T>(T service) where T : Service
        {
            Type serviceType = typeof(T);
            if (_services.ContainsKey(serviceType) && _services[serviceType] != null)
            {
                Debug.LogError("Service " + serviceType.Name + " is already registered");
                return;
            }

            service.transform.parent = transform;
            service.gameObject.name = "<Service> " + serviceType.Name;
            _services[serviceType] = service;
        }

        public bool Unregister<T>(T service) where T : Service
        {
            Type serviceType = typeof(T);
            if (!_services.ContainsKey(serviceType) || _services[serviceType] != service) { return false; }

            _services.Remove(serviceType);
            return true;
        }

        public bool TryGet<T>(out T service) where T : Service
        {
            Type serviceType = typeof(T);
            if (!_services.ContainsKey(serviceType) || !_services[serviceType])
            {
                service = null;
                return false;
            }

            service = (T)_services[serviceType];
            return true;
        }
    }
}