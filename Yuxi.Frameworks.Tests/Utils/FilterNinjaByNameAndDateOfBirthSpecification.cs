namespace Yuxi.Frameworks.Tests.Utils
{
    using System;
    using System.Linq.Expressions;
    using Specification.Base;
    using Entities;

    public class FilterNinjaByNameAndDateOfBirthSpecification : ExpressionSpecification<Ninja>
    {
        private readonly string _name;
        private readonly DateTime _dateOfBirth;

        public FilterNinjaByNameAndDateOfBirthSpecification(string name, DateTime dateOfBirth)
        {
            _name = name;
            _dateOfBirth = dateOfBirth;
        }

        public override Expression<Func<Ninja, bool>> ToExpression()
        {
            return ninja => ninja.Name == _name && ninja.DateOfBirth == _dateOfBirth;
        }
    }
}
