namespace Yuxi.Frameworks.Tests.Utils
{
    using Specification.Base;
    using Entities;

    public class FilterNinjaByNameSpecification : CompositeSpecification<Ninja>
    {
        #region Read Only Members

        private readonly string _name;

        #endregion

        #region Constructors

        public FilterNinjaByNameSpecification(string name)
        {
            _name = name;
        }

        #endregion

        #region Overriden Methods

        public override bool IsSatisfiedBy(Ninja entityToTest)
        {
            return entityToTest.Name == _name;
        }

        #endregion
    }
}
