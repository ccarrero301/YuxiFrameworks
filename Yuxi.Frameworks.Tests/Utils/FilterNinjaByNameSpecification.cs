namespace Yuxi.Frameworks.Tests.Utils
{
    using Specification.Base;
    using Entities;

    public class FilterNinjaByNameSpecification : CompositeSpecification<Ninja>
    {
        private readonly string _name;

        public FilterNinjaByNameSpecification(string name)
        {
            _name = name;
        }

        public override bool IsSatisfiedBy(Ninja entityToTest)
        {
            return entityToTest.Name == _name;
        }
    }
}
