using System;

public static class Disposable
{
    public static readonly IDisposable Empty = EmptyDisposable.Singleton;

    class EmptyDisposable : IDisposable
    {
        public static EmptyDisposable Singleton = new EmptyDisposable();

        private EmptyDisposable()
        {
        }

        public void Dispose()
        {
        }
    }
}