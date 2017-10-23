namespace Yuxi.Frameworks.Tests.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entities;
    using Utils;
    using NUnit.Framework;

    [TestFixture]
    public class UnitNinjaSpecificationTests
    {
        #region Members

        private List<Ninja> _ninjas;

        #endregion

        #region Set Up

        [SetUp]
        public void TestInitialize()
        {
            var ninja = NinjaUtils.SetNinja("the ninja", new List<NinjaEquipment>(), true, new DateTime(1982, 4, 23));
            var anotherNinja = NinjaUtils.SetNinja("another ninja", new List<NinjaEquipment>(), false, new DateTime(1950, 10, 28));

            _ninjas = new List<Ninja>() { ninja, anotherNinja };
        }

        [TearDown]
        public void TestCleanup()
        {
            _ninjas = null;
        }

        #endregion

        #region Test Cases

        [TestCase]
        public void AllNinjasSpecificationTest()
        {
            var allNinjaSpecification = new AllNinjaSpecification();

            var ninjasFound = _ninjas.Where(allNinjaSpecification.IsSatisfiedBy);

            Assert.AreEqual(2, ninjasFound.Count());
        }

        [TestCase]
        public void FilterNinjasByName()
        {
            var filterNinjaByNameSpecification = new FilterNinjaByNameSpecification("the ninja");

            var ninjasFound = _ninjas.Where(filterNinjaByNameSpecification.IsSatisfiedBy).ToList();

            Assert.AreEqual(1, ninjasFound.Count);
            Assert.AreEqual("the ninja", ninjasFound.First().Name);
        }

        [TestCase]
        public void FilterNinjasByDateOfBirthAndName()
        {
            var filterNinjaByNameSpecification = new FilterNinjaByNameSpecification("the ninja");
            var filterNinjaByDateOfBirthSpecification = new FilterNinjaByDateOfBirthSpecification(new DateTime(1982, 4, 23));

            var filterNinjasByDateOfBirthAndName = filterNinjaByNameSpecification & filterNinjaByDateOfBirthSpecification;

            var ninjasFound = _ninjas.Where(filterNinjasByDateOfBirthAndName.IsSatisfiedBy).ToList();

            Assert.AreEqual(1, ninjasFound.Count);
            Assert.AreEqual("the ninja", ninjasFound.First().Name);
        }

        [TestCase]
        public void FilterNinjasByDateOfBirthOrName()
        {
            var filterNinjaByNameSpecification = new FilterNinjaByNameSpecification("the ninja");
            var filterNinjaByDateOfBirthSpecification = new FilterNinjaByDateOfBirthSpecification(new DateTime(1950, 10, 28));

            var filterNinjasByDateOfBirthOrName = filterNinjaByNameSpecification | filterNinjaByDateOfBirthSpecification;

            var ninjasFound = _ninjas.Where(filterNinjasByDateOfBirthOrName.IsSatisfiedBy).ToList();

            Assert.AreEqual(2, ninjasFound.Count);
        }

        [TestCase]
        public void FilterNinjasByDateOfBirthAndNameWithoutOperands()
        {
            var filterNinjaByNameSpecification = new FilterNinjaByNameSpecification("the ninja");
            var filterNinjaByDateOfBirthSpecification = new FilterNinjaByDateOfBirthSpecification(new DateTime(1982, 4, 23));

            var ninjasFound = _ninjas.Where(filterNinjaByNameSpecification.And(filterNinjaByDateOfBirthSpecification).IsSatisfiedBy).ToList();

            Assert.AreEqual(1, ninjasFound.Count);
            Assert.AreEqual("the ninja", ninjasFound.First().Name);
        }

        [TestCase]
        public void FilterNinjasByDateOfBirthOrNameWithoutOperands()
        {
            var filterNinjaByNameSpecification = new FilterNinjaByNameSpecification("the ninja");
            var filterNinjaByDateOfBirthSpecification = new FilterNinjaByDateOfBirthSpecification(new DateTime(1950, 10, 28));

            var ninjasFound = _ninjas.Where(filterNinjaByNameSpecification.Or(filterNinjaByDateOfBirthSpecification).IsSatisfiedBy).ToList();

            Assert.AreEqual(2, ninjasFound.Count);
        }

        [TestCase]
        public void FilterNinjasByDateOfBirthAndNameExpression()
        {
            var filterNinjaByNameAndDateOfBirthSpecification = new FilterNinjaByNameAndDateOfBirthSpecification("the ninja", new DateTime(1982, 4, 23));

            var ninjasFound = _ninjas.Where(filterNinjaByNameAndDateOfBirthSpecification.IsSatisfiedBy).ToList();

            Assert.AreEqual(1, ninjasFound.Count);
            Assert.AreEqual("the ninja", ninjasFound.First().Name);
            Assert.AreEqual(new DateTime(1982, 4, 23), ninjasFound.First().DateOfBirth);
        }

        [TestCase]
        public void FilterNinjasByNotName()
        {
            var filterNinjaByNameSpecification = new FilterNinjaByNameSpecification("the ninja");
            var notFilterNinjaByNameSpecification = !filterNinjaByNameSpecification;

            var ninjasFound = _ninjas.Where(notFilterNinjaByNameSpecification.IsSatisfiedBy).ToList();

            Assert.AreEqual(1, ninjasFound.Count);
            Assert.AreEqual("another ninja", ninjasFound.First().Name);
        }

        [TestCase]
        public void FilterNinjasByNotNameWithoutOperands()
        {
            var filterNinjaByNameSpecification = new FilterNinjaByNameSpecification("the ninja");

            var ninjasFound = _ninjas.Where(filterNinjaByNameSpecification.Not().IsSatisfiedBy).ToList();

            Assert.AreEqual(1, ninjasFound.Count);
            Assert.AreEqual("another ninja", ninjasFound.First().Name);
        }

        #endregion
    }
}
