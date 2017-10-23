namespace Yuxi.Frameworks.Repository.Contracts.DataContext
{
    using System;
    using TrackableEntities;

    public interface IDataContext : IDisposable
    {
        #region Public Methods

        int SaveChanges();

        void SyncObjectState<TAgreggate>(TAgreggate entity) where TAgreggate : class, ITrackable;

        void SyncObjectsStatePostCommit();

        #endregion
    }
}