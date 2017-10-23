namespace Yuxi.Frameworks.Tests.Utils
{
    using Specification.Base;
    using Entities;

    public class AllNinjaSpecification : CompositeSpecification<Ninja>
    {
        #region Overriden Methods

        public override bool IsSatisfiedBy(Ninja entityToTest)
        {
            return All().IsSatisfiedBy(entityToTest);
        }

        #endregion
    }
}
