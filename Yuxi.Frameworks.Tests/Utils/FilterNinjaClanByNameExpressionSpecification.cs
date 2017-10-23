namespace Yuxi.Frameworks.Tests.Utils
{
    using System;
    using System.Linq.Expressions;
    using Specification.Base;
    using Entities;

    public class FilterNinjaClanByNameExpressionSpecification : ExpressionSpecification<NinjaClan>
    {
        #region Read Only Members

        private readonly string _name;

        #endregion

        #region Constructors

        public FilterNinjaClanByNameExpressionSpecification(string name)
        {
            _name = name;
        }

        #endregion

        #region Overriden Methods

        public override Expression<Func<NinjaClan, bool>> ToExpression()
        {
            return ninjaClan => ninjaClan.ClanName == _name;
        }

        #endregion
    }
}
