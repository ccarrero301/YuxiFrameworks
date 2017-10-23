namespace Yuxi.Frameworks.Tests.Entities
{
    using System.Collections.Generic;
    using Repository.Implementations.Entity;

    public class NinjaClan : Entity
    {
        public int Id { get; set; }

        public string ClanName { get; set; }

        public ICollection<Ninja> Ninjas { get; set; }
    }
}