namespace Yuxi.Frameworks.Repository.Standard.Contracts.UnitOfWork
{
    using System;
    using System.Data;
    using Repository;
    using TrackableEntities.Common.Core;

    public interface IUnitOfWork : IDisposable
    {
        #region Public Methods

        int SaveChanges();

        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);

        bool Commit();

        void Rollback();

        IRepository<TAgreggate> Repository<TAgreggate>() where TAgreggate : class, ITrackable;

        #endregion
    }
}