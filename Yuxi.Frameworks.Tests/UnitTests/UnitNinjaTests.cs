using Yuxi.Frameworks.Repository.Implementations.Query;
using Yuxi.Frameworks.Repository.Implementations.Repository;
using Yuxi.Frameworks.Repository.Implementations.Utils;

namespace Yuxi.Frameworks.Tests.UnitTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Entities;
    using Entities.Enums;
    using Fake;
    using Utils;
    using Repository.Contracts.UnitOfWork;
    using Repository.Implementations.UnitOfWork;
    using Repository.Contracts.Repository;
    using TrackableEntities;
    using NUnit.Framework;

    [TestFixture]
    public class UnitNinjaTests
    {
        #region Members

        private NinjaClanFakeContext _ninjaClanContext;
        private IUnitOfWorkAsync _unitOfWorkAsync;
        private IRepository<NinjaClan> _ninjaClanRepository;
        private QueryFluent<NinjaClan> _query;
        
        #endregion

        #region Set Up

        [SetUp]
        public void TestInitialize()
        {
            _ninjaClanContext = new NinjaClanFakeContext();
            _unitOfWorkAsync = new UnitOfWork(_ninjaClanContext);
            _ninjaClanRepository = _unitOfWorkAsync.Repository<NinjaClan>();

            CreateClan();
        }

        [TearDown]
        public void TestCleanup()
        {
            _unitOfWorkAsync.Dispose();
        }

        #endregion

        #region Test Cases

        [TestCase]
        public void InsertTestClan()
        {
            var ninjaClanRepository = _unitOfWorkAsync.Repository<NinjaClan>();

            var ninjaClan = ninjaClanRepository.Query().Select().ToList();

            Assert.AreEqual(1, ninjaClan.Count);
            Assert.AreEqual(1, ninjaClan.First().Ninjas.Count);
            Assert.AreEqual(3, ninjaClan.First().Ninjas.First().EquipmentOwned.Count);
        }

        [TestCase]
        public void UpdateTestClan()
        {
            var ninjaClan = _ninjaClanRepository.Query().Select().Single();

            ninjaClan.ClanName = "modified clan name";
            ninjaClan.TrackingState = TrackingState.Modified;

            _ninjaClanRepository.Update(ninjaClan);

            var updatedNinjaClan = _ninjaClanRepository.Query().Select().Single();

            Assert.AreEqual("modified clan name", updatedNinjaClan.ClanName);
        }

        [TestCase]
        public void DeleteTestClan()
        {
            var ninjaClan =_ninjaClanRepository.Query().Select().SingleOrDefault();

            _ninjaClanRepository.Delete(ninjaClan);

            var shouldNotExistClan = _ninjaClanRepository.Query().Select().SingleOrDefault();

            Assert.IsNull(shouldNotExistClan);
        }

        [TestCase]
        public void QuerySelectTestClan()
        {
            _query = new QueryFluent<NinjaClan>(new Repository<NinjaClan>(_ninjaClanContext, _unitOfWorkAsync), new AllNinjaExpressionSpecification());

            var ninjaClan = _query.Select().ToList();

            Assert.AreEqual(1, ninjaClan.Count);
            Assert.AreEqual(1, ninjaClan.First().Ninjas.Count);
            Assert.AreEqual(3, ninjaClan.First().Ninjas.First().EquipmentOwned.Count);
        }

        [TestCase]
        public void QuerySelectExpressionTestClan()
        {
            _query = new QueryFluent<NinjaClan>(new Repository<NinjaClan>(_ninjaClanContext, _unitOfWorkAsync), new AllNinjaExpressionSpecification());

            var ninjaClan = _query.Select(x => x.ClanName).ToList();

            Assert.AreEqual(1, ninjaClan.Count);
            Assert.AreEqual("TestClan", ninjaClan.First());
        }

        #endregion

        #region Utility Methods

        private void CreateClan()
        {
            var equipmentsForNinja = new List<NinjaEquipment>
            {
                NinjaUtils.SetEquipment("Katana", EquipmentType.Weapon),
                NinjaUtils.SetEquipment("Kimono", EquipmentType.Outwear),
                NinjaUtils.SetEquipment("Strength", EquipmentType.Tool)
            };

            var clan = NinjaUtils.SetNinjaInClanWithWeapon("TestClan", "TestNinja", equipmentsForNinja);

            _ninjaClanRepository.Add(clan);

            _unitOfWorkAsync.SaveChanges();
        }
        
        #endregion
    }
}