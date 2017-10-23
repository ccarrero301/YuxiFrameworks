namespace Yuxi.Frameworks.Tests.Fake
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Repository.Implementations.DataContext;
    using Entities;

    public class NinjaClanFakeDbSet : FakeDbSet<NinjaClan>
    {
        public override NinjaClan Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.Id == (int) keyValues.FirstOrDefault());
        }

        public override Task<NinjaClan> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<NinjaClan>(() => this.SingleOrDefault(t => t.Id == (int) keyValues.FirstOrDefault()));
        }
    }
}