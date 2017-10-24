namespace Yuxi.Frameworks.Specification.Standard.Contracts
{
    public interface ISpecification<TEntity>
    {
        #region Public Methods

        bool IsSatisfiedBy(TEntity entityToTest);

        ISpecification<TEntity> And(ISpecification<TEntity> specification);

        ISpecification<TEntity> Or(ISpecification<TEntity> specification);

        ISpecification<TEntity> Not();

        ISpecification<TEntity> All();

        #endregion
    }
}