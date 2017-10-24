namespace Yuxi.Frameworks.Repository.Standard.Contracts.DataContext
{
    using System;
    using TrackableEntities.Common.Core;

    public interface IDataContext : IDisposable
    {
        #region Public Methods

        int SaveChanges();

        void SyncObjectState<TAgreggate>(TAgreggate entity) where TAgreggate : class, ITrackable;

        void SyncObjectsStatePostCommit();

        #endregion
    }
}