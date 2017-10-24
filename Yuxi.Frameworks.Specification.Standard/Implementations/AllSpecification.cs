namespace Yuxi.Frameworks.Specification.Standard.Implementations
{
    using Base;

    internal sealed class AllSpecification<TEntity> : CompositeSpecification<TEntity>
    {
        #region Overriden Methods

        public override bool IsSatisfiedBy(TEntity entityToTest)
        {
            return true;
        }

        #endregion
    }
}