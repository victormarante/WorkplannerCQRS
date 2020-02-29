using System.Threading.Tasks;
using Nito.AsyncEx;
using Xunit;

namespace WorkplannerCQRS.IntegrationTests.Setup
{
    public abstract class IntegrationTestBase : IAsyncLifetime
    {
        private static readonly AsyncLock Mutex = new AsyncLock();

        private static bool _initialized;
        
        public virtual async Task InitializeAsync()
        {
            if (_initialized) return;

            using (await Mutex.LockAsync())
            {
                if (_initialized) return;

                // removes all data from testing db tables
                // to have a clean slate when running tests
                await TestFixture.ResetCheckpoint();

                _initialized = true;
            }
        }

        public virtual Task DisposeAsync() => Task.CompletedTask;
    }
}