using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    protected bool IsDestroyOnLoad { get; set; } = true;

    private static T s_instance;

    public static T Instance => s_instance;


    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        if (s_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        s_instance = this as T;
        if (IsDestroyOnLoad == false)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        Dispose();
    }

    protected virtual void Dispose()
    {
        if (s_instance == this)
        {
            s_instance = null;
        }
    }
}
