namespace Yuxi.Frameworks.Tests.Utils
{
    using System;
    using System.Linq.Expressions;
    using Specification.Base;
    using Entities;

    public class AllNinjaExpressionSpecification : ExpressionSpecification<NinjaClan>
    {
        public override Expression<Func<NinjaClan, bool>> ToExpression()
        {
            return ninja => true;
        }
    }
}
