namespace Yuxi.Frameworks.Repository.Standard.Implementations.DataContext
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;
    using Utils;
    using Contracts.DataContext;
    using EFCore = Microsoft.EntityFrameworkCore;
    using TrackableEntities.Common.Core;
    using TrackableEntities.EF.Core;

    public class DataContext : EFCore.DbContext, IDataContextAsync
    {
        #region Constructors

        public DataContext(EFCore.DbContextOptions options) : base(options)
        {
            InstanceId = Guid.NewGuid();
        }

        public DataContext()
        {
            InstanceId = Guid.NewGuid();
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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
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

            this.ApplyChanges(entities.ToList());
        }

        #endregion
    }
}