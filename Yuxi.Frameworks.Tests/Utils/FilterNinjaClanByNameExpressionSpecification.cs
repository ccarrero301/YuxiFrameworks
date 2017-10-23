namespace Yuxi.Frameworks.Tests.Utils
{
    using System;
    using System.Linq.Expressions;
    using Specification.Base;
    using Entities;

    public class FilterNinjaClanByNameExpressionSpecification : ExpressionSpecification<NinjaClan>
    {
        private readonly string _name;

        public FilterNinjaClanByNameExpressionSpecification(string name)
        {
            _name = name;
        }

        public override Expression<Func<NinjaClan, bool>> ToExpression()
        {
            return ninjaClan => ninjaClan.ClanName == _name;
        }
    }
}
