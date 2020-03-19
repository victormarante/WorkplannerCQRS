using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkplannerCQRS.API;
using Respawn;
using WorkplannerCQRS.API.Data;

namespace WorkplannerCQRS.IntegrationTests.Setup
{
    public class TestFixture
    {
        private static readonly IConfigurationRoot _configuration;
        private static readonly IServiceScopeFactory _scopeFactory;
        private static readonly Checkpoint _checkPoint;

        static TestFixture()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testappsettings.json", true, true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();
            
            var startup = new Startup(_configuration);
            var services = new ServiceCollection();
            services.AddLogging();
            startup.ConfigureServices(services);
            var provider = services.BuildServiceProvider();
            _scopeFactory = provider.GetService<IServiceScopeFactory>();
            _checkPoint = new Checkpoint();
        }

        /// <summary>
        /// ResetCheckpoint resets the database to last known state before tests were run
        /// </summary>
        public static Task ResetCheckpoint() => _checkPoint.Reset(_configuration.GetConnectionString("WorkplannerDb"));

        public static async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<WorkplannerDbContext>();

            try
            {
                await dbContext.BeginTransactionAsync();
                
                await action(scope.ServiceProvider);
                
                await dbContext.CommitTransactionAsync();
            }
            catch (Exception)
            {
                dbContext.RollbackTransaction();
                throw;
            }
        }

        public static async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> func)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<WorkplannerDbContext>();

            try
            {
                await dbContext.BeginTransactionAsync();

                var result = await func(scope.ServiceProvider);
                
                await dbContext.CommitTransactionAsync();

                return result;
            }
            catch (Exception)
            {
                dbContext.RollbackTransaction();
                throw;
            }
        }

        public static Task ExecuteDbContextAsync(Func<WorkplannerDbContext, Task> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<WorkplannerDbContext>()));
        
        public static Task<T> ExecuteDbContextAsync<T>(Func<WorkplannerDbContext, Task<T>> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<WorkplannerDbContext>()));

        public static Task InsertAsync<TEntity>(TEntity entity) where TEntity : class
        {
            return ExecuteDbContextAsync(db =>
            {
                db.Set<TEntity>().Add(entity);
                return db.SaveChangesAsync();
            });
        }
        
        public static Task InsertAsync<TEntity>(params TEntity[] entities) where TEntity : class
        {
            return ExecuteDbContextAsync(db =>
            {
                foreach (var entity in entities)
                {
                    db.Set<TEntity>().Add(entity);
                }

                return db.SaveChangesAsync();
            });
        }

        public static Task<T> FindAsync<T>(params object[] ids) where T : class 
        {
            return ExecuteDbContextAsync(db => db.Set<T>().FindAsync(ids).AsTask());
        }
        
        /*
        public static Task<T> FindAsync<T>(Guid id) where T : class
        {
            return ExecuteDbContextAsync(db => db.Set<T>().FindAsync(id).AsTask());
        }
        
        public static Task<T> FindAsync<T>(int id) where T : class 
        {
            return ExecuteDbContextAsync(db => db.Set<T>().FindAsync(id).AsTask());
        }
        */

        public static Task SendRequestAsync(IRequest request)
        {
            return ExecuteScopeAsync(sp =>
            {
                var mediator = sp.GetService<IMediator>();
                return mediator.Send(request);
            });
        }
        
        public static Task<TResponse> SendRequestAsync<TResponse>(IRequest<TResponse> request)
        {
            return ExecuteScopeAsync(sp =>
            {
                var mediator = sp.GetService<IMediator>();
                return mediator.Send(request);
            });
        }
        
        private static int _objectNumber = 1;

        public static int NextObjectNumber() => Interlocked.Increment(ref _objectNumber);

    }
}