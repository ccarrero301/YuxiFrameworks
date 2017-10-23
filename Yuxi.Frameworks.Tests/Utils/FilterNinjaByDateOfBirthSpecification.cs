namespace Yuxi.Frameworks.Tests.Utils
{
    using System;
    using Specification.Base;
    using Entities;

    public class FilterNinjaByDateOfBirthSpecification : CompositeSpecification<Ninja>
    {
        #region Read Only Members

        private readonly DateTime _dateOfBirth;

        #endregion

        #region Constructors

        public FilterNinjaByDateOfBirthSpecification(DateTime dateOfBirth)
        {
            _dateOfBirth = dateOfBirth;
        }

        #endregion

        #region Overriden Methods

        public override bool IsSatisfiedBy(Ninja entityToTest)
        {
            return entityToTest.DateOfBirth == _dateOfBirth;
        }

        #endregion
    }
}
