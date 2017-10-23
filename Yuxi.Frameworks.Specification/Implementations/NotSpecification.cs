namespace Yuxi.Frameworks.Specification.Implementations
{
    using Base;
    using Contracts;

    internal sealed class NotSpecification<TEntity> : CompositeSpecification<TEntity>
    {
        #region Read Only Members

        private readonly ISpecification<TEntity> _specification;

        #endregion

        #region Constructors

        public NotSpecification(ISpecification<TEntity> specification)
        {
            _specification = specification;
        }

        #endregion

        #region Overriden Methods

        public override bool IsSatisfiedBy(TEntity entityToTest)
        {
            return !_specification.IsSatisfiedBy(entityToTest);
        }

        #endregion
    }
}