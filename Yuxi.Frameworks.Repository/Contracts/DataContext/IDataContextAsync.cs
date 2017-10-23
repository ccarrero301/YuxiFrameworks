namespace Yuxi.Frameworks.Repository.Contracts.DataContext
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDataContextAsync : IDataContext
    {
        #region Public Methods

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<int> SaveChangesAsync();

        #endregion
    }
}