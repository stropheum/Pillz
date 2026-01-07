namespace Pillz
{
    public class PersistentSingleton<T> : Singleton<T> where T : PersistentSingleton<T>
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnApplicationQuit()
        {
            if (IsValid)
            {
                Destroy(gameObject);
            }
        }
    }
}