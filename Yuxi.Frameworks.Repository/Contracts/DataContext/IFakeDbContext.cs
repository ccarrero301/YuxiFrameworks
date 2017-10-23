namespace Yuxi.Frameworks.Repository.Contracts.DataContext
{
    using System.Data.Entity;
    using Implementations.DataContext;
    using Implementations.Entity;

    public interface IFakeDbContext : IDataContextAsync
    {
        #region Public Methods

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        void AddFakeDbSet<TEntity, TFakeDbSet>()
            where TEntity : Entity, new()
            where TFakeDbSet : FakeDbSet<TEntity>, IDbSet<TEntity>, new();

        #endregion
    }
}