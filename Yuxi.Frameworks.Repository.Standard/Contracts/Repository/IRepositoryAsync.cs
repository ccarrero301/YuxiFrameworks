﻿namespace Yuxi.Frameworks.Repository.Standard.Contracts.Repository
{
    using System.Threading;
    using System.Threading.Tasks;
    using TrackableEntities.Common.Core;

    public interface IRepositoryAsync<TAgreggate> : IRepository<TAgreggate> where TAgreggate : class, ITrackable
    {
        #region Public Methods

        Task<TAgreggate> FindAsync(params object[] keyValues);

        Task<TAgreggate> FindAsync(CancellationToken cancellationToken, params object[] keyValues);

        Task<bool> DeleteAsync(params object[] keyValues);

        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);

        #endregion
    }
}