using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts._ProjectExtensions
{
    public class AsyncLock
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public async UniTask<Releaser> LockAsync()
        {
            await _semaphore.WaitAsync();
            return new Releaser(_semaphore);
        }

        public struct Releaser : IDisposable
        {
            private readonly SemaphoreSlim _semaphore;

            public Releaser(SemaphoreSlim semaphore)
            {
                _semaphore = semaphore;
            }

            public void Dispose()
            {
                _semaphore.Release();
            }
        }
    }
}