namespace Yuxi.Frameworks.Repository.Standard.Contracts.DataContext
{
    using System.Data.Entity;
    using Implementations.DataContext;
    using Implementations.Entity;
    using EFCore = Microsoft.EntityFrameworkCore;

    public interface IFakeDbContext : IDataContextAsync
    {
        #region Public Methods

        EFCore.DbSet<TEntity> Set<TEntity>() where TEntity : class;

        void AddFakeDbSet<TEntity, TFakeDbSet>()
            where TEntity : Entity, new()
            where TFakeDbSet : FakeDbSet<TEntity>, IDbSet<TEntity>, new();

        #endregion
    }
}