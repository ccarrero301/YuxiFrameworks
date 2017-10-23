namespace Yuxi.Frameworks.Repository.Implementations.Utils
{
    using System;
    using System.Linq.Expressions;
    using Specification.Base;

    internal sealed class AllExpressionSpecification<TEntity> : ExpressionSpecification<TEntity>
    {
        #region Overriden Methods

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            return entity => true;
        }

        #endregion
    }
}