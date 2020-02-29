using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using WorkplannerCQRS.API.Domain;
using WorkplannerCQRS.API.Domain.Worker;
using WorkplannerCQRS.API.Domain.Worker.Models;
using WorkplannerCQRS.API.Domain.WorkOrder.Models;

namespace WorkplannerCQRS.API.Data
{
    public class WorkplannerDbContext : DbContext
    {
        private IDbContextTransaction _currentTransaction;
        
        public WorkplannerDbContext(DbContextOptions<WorkplannerDbContext> options)
            : base(options)
        {
        }

        public DbSet<WorkOrder> WorkOrders { get; set; }

        public DbSet<Worker> Workers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(WorkplannerDbContext)));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        #region Custom transaction logic
        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();
                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
        #endregion
        
        #region Custom SaveChanges logic
        
        public override int SaveChanges()
        {
            var addedEntities = ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.State == EntityState.Added)
                .ToList();
            foreach (var addedEntity in addedEntities)
            {
                addedEntity.Entity.CreatedAt = DateTimeOffset.Now;
                addedEntity.Entity.ModifiedAt = DateTimeOffset.Now;
            }
            
            var modifiedEntities = ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.State == EntityState.Modified)
                .ToList();

            foreach (var modifiedEntity in modifiedEntities)
            {
                modifiedEntity.Entity.ModifiedAt = DateTimeOffset.Now;
            }

            var affectedRows = base.SaveChanges();
            return affectedRows;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var addedEntities = ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.State == EntityState.Added)
                .ToList();
            
            foreach (var addedEntity in addedEntities)
            {
                addedEntity.Entity.CreatedAt = DateTimeOffset.Now;
                addedEntity.Entity.ModifiedAt = DateTimeOffset.Now;
            }
            
            var modifiedEntities = ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.State == EntityState.Modified)
                .ToList();

            foreach (var modifiedEntity in modifiedEntities)
            {
                modifiedEntity.Entity.ModifiedAt = DateTimeOffset.Now;
            }
            
            var affectedRows = await base.SaveChangesAsync(cancellationToken);
            return affectedRows;
        }

        #endregion
    }
}