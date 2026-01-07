using UnityEngine;

namespace Pillz.Service
{
    public abstract class Service : MonoBehaviour
    {
        public bool Initialized { get; private set; }
        
        protected virtual void Start()
        {
            Initialized = true;
            Debug.Log("Service Initialized: " + GetType().Name);
        }
    }
}