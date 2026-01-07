using UnityEngine;

namespace Pillz
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (!IsValid) { Initialize(); }

                return _instance;
            }
        }

        // ReSharper disable once MemberCanBeProtected.Global
        public static bool IsValid => _instance != null;

        // ReSharper disable once MemberCanBeProtected.Global
        public static void Initialize()
        {
            if (IsValid) { return; }

            _instance = new GameObject("<Singleton> " + typeof(T).Name).AddComponent<T>();
        }

        protected virtual void Awake()
        {
            if (IsValid && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = (T)this;
        }
    }
}