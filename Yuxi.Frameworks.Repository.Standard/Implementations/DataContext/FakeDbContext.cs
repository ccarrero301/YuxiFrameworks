namespace Yuxi.Frameworks.Repository.Standard.Implementations.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Data.Entity;
    using Contracts.DataContext;
    using TrackableEntities.Common.Core;
    using EFCore = Microsoft.EntityFrameworkCore;

    public abstract class FakeDbContext : IFakeDbContext
    {
        #region Read Only Members

        private readonly Dictionary<Type, object> _fakeDbSets;

        #endregion

        #region Constructors

        protected FakeDbContext()
        {
            _fakeDbSets = new Dictionary<Type, object>();
        }

        #endregion

        #region Public Methods

        public int SaveChanges()
        {
            return default(int);
        }

        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, ITrackable
        {
            // no implentation needed, unit tests which uses FakeDbContext since there is no actual database for unit tests
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return new Task<int>(() => default(int));
        }

        public Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return new Task<int>(() => default(int));
        }

        public Task<int> SaveChangesAsync()
        {
            return new Task<int>(() => default(int));
        }

        public void Dispose()
        {
        }

        public EFCore.DbSet<T> Set<T>() where T : class
        {
            return (EFCore.DbSet<T>) _fakeDbSets[typeof(T)];
        }

        public void AddFakeDbSet<TEntity, TFakeDbSet>()
            where TEntity : Entity.Entity, new()
            where TFakeDbSet : FakeDbSet<TEntity>, IDbSet<TEntity>, new()
        {
            var fakeDbSet = Activator.CreateInstance<TFakeDbSet>();
            _fakeDbSets.Add(typeof(TEntity), fakeDbSet);
        }

        public void SyncObjectsStatePostCommit()
        {
        }

        #endregion
    }
}