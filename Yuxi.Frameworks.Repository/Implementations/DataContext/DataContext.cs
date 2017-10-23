namespace Yuxi.Frameworks.Repository.Implementations.DataContext
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Data.Entity;
    using System.Linq;
    using Utils;
    using Contracts.DataContext;
    using TrackableEntities;
    using TrackableEntities.EF6;

    public class DataContext : DbContext, IDataContextAsync
    {
        #region Constructors

        public DataContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            InstanceId = Guid.NewGuid();

            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        #endregion

        #region Public Properties

        public Guid InstanceId { get; }

        #endregion

        #region Public Overriden Methods

        public override int SaveChanges()
        {
            SyncObjectsStatePreCommit();
            var changes = base.SaveChanges();
            SyncObjectsStatePostCommit();
            return changes;
        }

        public override async Task<int> SaveChangesAsync()
        {
            return await SaveChangesAsync(CancellationToken.None);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            SyncObjectsStatePreCommit();
            var changesAsync = await base.SaveChangesAsync(cancellationToken);
            SyncObjectsStatePostCommit();
            return changesAsync;
        }

        #endregion

        #region Public Methods

        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, ITrackable
        {
            this.ApplyChanges(entity);
        }

        public void SyncObjectsStatePostCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                ((ITrackable) dbEntityEntry.Entity).TrackingState = StateHelper.ConvertState(dbEntityEntry.State);
            }
        }

        #endregion

        #region Private Methods

        private void SyncObjectsStatePreCommit()
        {
            var entities = ChangeTracker.Entries().Select(x => x.Entity).OfType<ITrackable>();
            this.ApplyChanges(entities);
        }

        #endregion
    }
}