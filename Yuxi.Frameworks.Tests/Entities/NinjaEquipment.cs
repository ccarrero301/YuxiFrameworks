namespace Yuxi.Frameworks.Tests.Entities
{
    using Enums;
    using Repository.Implementations.Entity;

    public class NinjaEquipment : Entity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public EquipmentType Type { get; set; }

        public Ninja Ninja { get; set; }
    }
}