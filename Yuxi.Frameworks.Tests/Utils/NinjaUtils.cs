namespace Yuxi.Frameworks.Tests.Utils
{
    using System;
    using System.Collections.Generic;
    using Entities;
    using Entities.Enums;
    using Repository.Contracts.Repository;
    using Repository.Contracts.UnitOfWork;
    using TrackableEntities;

    internal static class NinjaUtils
    {
        #region Utility Methods

        internal static NinjaClan SetNinjaInClanWithWeapon(int clanId, string clanName, string ninjaName, ICollection<NinjaEquipment> ninjaEquipment)
        {
            var theClan = new NinjaClan
            {
                Id = clanId,
                ClanName = clanName,
                Ninjas = new List<Ninja>(),
                TrackingState = TrackingState.Added
            };

            var theNinja = new Ninja
            {
                Id = clanId,
                Name = ninjaName,
                ServedInOniwaban = true,
                ClanId = theClan.Id,
                Clan = theClan,
                EquipmentOwned = new List<NinjaEquipment>(),
                TrackingState = TrackingState.Added
            };

            theNinja.EquipmentOwned = ninjaEquipment;
            theNinja.Clan = theClan;
            theNinja.ClanId = theClan.Id;

            foreach (var equipment in ninjaEquipment)
            {
                equipment.Ninja = theNinja;
            }

            theClan.Ninjas.Add(theNinja);

            return theClan;
        }

        internal static NinjaEquipment SetEquipment(string equipmentName, EquipmentType equipmentType)
        {
            return new NinjaEquipment
            {
                Name = equipmentName,
                Type = equipmentType
            };
        }

        internal static Ninja SetNinja(string ninjaName, ICollection<NinjaEquipment> ninjaEquipment, bool servedInOniwaban, DateTime dateOfBirth)
        {
            return new Ninja
            {
                Name = ninjaName,
                EquipmentOwned = ninjaEquipment,
                ServedInOniwaban = servedInOniwaban,
                DateOfBirth = dateOfBirth
            };
        }

        internal static void CreateClan(int clanId, string clanName, IEnumerable<Ninja> ninjas, IRepository<NinjaClan> ninjaClanRepository, IUnitOfWorkAsync unitOfWorkAsync)
        {
            foreach (var ninja in ninjas)
            {
                var clan = SetNinjaInClanWithWeapon(clanId, clanName, ninja.Name, ninja.EquipmentOwned);
                ninjaClanRepository.Add(clan);
            }

            unitOfWorkAsync.SaveChanges();
        }

        #endregion
    }
}
