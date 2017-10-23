namespace Yuxi.Frameworks.Tests.Utils
{
    using System;
    using Specification.Base;
    using Entities;

    public class FilterNinjaByDateOfBirthSpecification : CompositeSpecification<Ninja>
    {
        private readonly DateTime _dateOfBirth;

        public FilterNinjaByDateOfBirthSpecification(DateTime dateOfBirth)
        {
            _dateOfBirth = dateOfBirth;
        }

        public override bool IsSatisfiedBy(Ninja entityToTest)
        {
            return entityToTest.DateOfBirth == _dateOfBirth;
        }
    }
}
