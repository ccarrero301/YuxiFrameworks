namespace Yuxi.Frameworks.Tests.Entities
{
    using System;
    using System.Collections.Generic;
    using Repository.Implementations.Entity;

    public class Ninja : Entity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool ServedInOniwaban { get; set; }

        public NinjaClan Clan { get; set; }

        public int ClanId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ICollection<NinjaEquipment> EquipmentOwned { get; set; }
    }
}