namespace Yuxi.Frameworks.Specification.Implementations
{
    using Base;
    using Contracts;

    internal sealed class OrSpecification<TEntity> : CompositeSpecification<TEntity>
    {
        #region Read Only Members

        private readonly ISpecification<TEntity> _leftSpecification;
        private readonly ISpecification<TEntity> _rightSpecification;

        #endregion

        #region Constructors

        public OrSpecification(ISpecification<TEntity> left, ISpecification<TEntity> right)
        {
            _leftSpecification = left;
            _rightSpecification = right;
        }

        #endregion

        #region Overriden Methods

        public override bool IsSatisfiedBy(TEntity entityToTest)
        {
            return _leftSpecification.IsSatisfiedBy(entityToTest) || _rightSpecification.IsSatisfiedBy(entityToTest);
        }

        #endregion
    }
}