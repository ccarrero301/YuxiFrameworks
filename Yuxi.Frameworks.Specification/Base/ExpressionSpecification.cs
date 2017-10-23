namespace Yuxi.Frameworks.Specification.Base
{
    using System;
    using System.Linq.Expressions;

    public abstract class ExpressionSpecification<TEntity> : CompositeSpecification<TEntity>
    {
        #region Public Abstract Methods

        public abstract Expression<Func<TEntity, bool>> ToExpression();

        #endregion

        #region Public Overriden Methods

        public override bool IsSatisfiedBy(TEntity entityToTest)
        {
            var predicate = ToExpression().Compile();
            return predicate(entityToTest);
        }

        #endregion
    }
}