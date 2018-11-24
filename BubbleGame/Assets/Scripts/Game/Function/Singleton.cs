using System;

public class Singleton<T> : IDisposable
    where T : Singleton<T>, new()
{
    private static T instance;

    public static bool HasInstance
    {
        get { return instance != null; }
    }

    public static T Instance
    {
        get { return instance; }
    }

    public static IDisposable CreateInstance()
    {
        if (HasInstance)
        {
            return Disposable.Empty;
        }

        instance = new T();
        instance.OnCreated();
        return instance;
    }

    public void Dispose()
    {
        if (!HasInstance)
        {
            return;
        }

        instance.OnDisposed();
        instance = null;
    }

    protected virtual void OnCreated()
    {
    }

    protected virtual void OnDisposed()
    {

    }
}