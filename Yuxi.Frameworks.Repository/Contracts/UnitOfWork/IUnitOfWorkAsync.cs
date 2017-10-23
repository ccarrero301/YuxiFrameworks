namespace Yuxi.Frameworks.Repository.Contracts.UnitOfWork
{
    using System.Threading;
    using System.Threading.Tasks;
    using Repository;
    using TrackableEntities;

    public interface IUnitOfWorkAsync : IUnitOfWork
    {
        #region Public Methods

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, ITrackable;

        #endregion
    }
}