namespace Yuxi.Frameworks.Tests.Fake
{
    using Repository.Implementations.DataContext;
    using Entities;

    public class NinjaClanFakeContext : FakeDbContext
    {
        public NinjaClanFakeContext()
        {
            AddFakeDbSet<NinjaClan, NinjaClanFakeDbSet>();
        }
    }
}