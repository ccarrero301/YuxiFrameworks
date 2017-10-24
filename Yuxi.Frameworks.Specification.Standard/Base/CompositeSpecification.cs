namespace Yuxi.Frameworks.Specification.Standard.Base
{
    using Contracts;
    using Implementations;

    public abstract class CompositeSpecification<TEntity> : ISpecification<TEntity>
    {
        #region Public Abstract Methods

        public abstract bool IsSatisfiedBy(TEntity entityToTest);

        #endregion

        #region Base Implementation

        public ISpecification<TEntity> And(ISpecification<TEntity> specification)
        {
            return new AndSpecification<TEntity>(this, specification);
        }

        public static CompositeSpecification<TEntity> operator &(CompositeSpecification<TEntity> specificationLeft,
            CompositeSpecification<TEntity> specificationRight)
        {
            return new AndSpecification<TEntity>(specificationLeft, specificationRight);
        }

        public ISpecification<TEntity> Or(ISpecification<TEntity> specification)
        {
            return new OrSpecification<TEntity>(this, specification);
        }

        public static CompositeSpecification<TEntity> operator |(CompositeSpecification<TEntity> specificationLeft,
            CompositeSpecification<TEntity> specificationRight)
        {
            return new OrSpecification<TEntity>(specificationLeft, specificationRight);
        }

        public ISpecification<TEntity> Not()
        {
            return new NotSpecification<TEntity>(this);
        }

        public static CompositeSpecification<TEntity> operator !(CompositeSpecification<TEntity> specification)
        {
            return new NotSpecification<TEntity>(specification);
        }

        public ISpecification<TEntity> All()
        {
            return new AllSpecification<TEntity>();
        }

        #endregion
    }
}