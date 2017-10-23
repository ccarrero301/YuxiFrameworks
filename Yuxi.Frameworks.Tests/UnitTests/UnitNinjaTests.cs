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
    using Repository.Implementations.Query;
    using Repository.Implementations.Repository;
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

            CreateClans();
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

            Assert.AreEqual(2, ninjaClan.Count);
            Assert.AreEqual(1, ninjaClan.First().Ninjas.Count);
            Assert.AreEqual(3, ninjaClan.First().Ninjas.First().EquipmentOwned.Count);
        }

        [TestCase]
        public void UpdateTestClan()
        {
            var ninjaClan = _ninjaClanRepository.Query(new FilterNinjaClanByNameExpressionSpecification("TestClan")).Select().FirstOrDefault();

            ninjaClan.ClanName = "modified clan name";
            ninjaClan.TrackingState = TrackingState.Modified;

            _ninjaClanRepository.Update(ninjaClan);

            var updatedNinjaClan = _ninjaClanRepository.Query(new FilterNinjaClanByNameExpressionSpecification("modified clan name")).Select().Single();

            Assert.AreEqual("modified clan name", updatedNinjaClan.ClanName);
        }

        [TestCase]
        public void DeleteTestClan()
        {
            var ninjaClan =_ninjaClanRepository.Query().Select().ToList().First();

            _ninjaClanRepository.Delete(ninjaClan);

            var shouldNotExistClan = _ninjaClanRepository.Query(new FilterNinjaClanByNameExpressionSpecification("TestClan")).Select().SingleOrDefault();

            Assert.IsNull(shouldNotExistClan);
        }

        [TestCase]
        public void QuerySelectTestClan()
        {
            _query = new QueryFluent<NinjaClan>(new Repository<NinjaClan>(_ninjaClanContext, _unitOfWorkAsync), new AllNinjaExpressionSpecification());

            var ninjaClan = _query.Select().ToList();

            Assert.AreEqual(2, ninjaClan.Count);
            Assert.AreEqual(1, ninjaClan.First().Ninjas.Count);
            Assert.AreEqual(3, ninjaClan.First().Ninjas.First().EquipmentOwned.Count);
        }

        [TestCase]
        public void QuerySelectExpressionTestClan()
        {
            _query = new QueryFluent<NinjaClan>(new Repository<NinjaClan>(_ninjaClanContext, _unitOfWorkAsync), new AllNinjaExpressionSpecification());

            var ninjaClan = _query.Select(x => x.ClanName).ToList();

            Assert.AreEqual(2, ninjaClan.Count);
            Assert.AreEqual("TestClan", ninjaClan.First());
        }

        [TestCase]
        public void QuerySelectOrderByExpressionTestClan()
        {
            _query = new QueryFluent<NinjaClan>(new Repository<NinjaClan>(_ninjaClanContext, _unitOfWorkAsync), new AllNinjaExpressionSpecification());

            var ninjaClan = _query.OrderBy(x => x.OrderBy(clan => clan.ClanName)).Select().First();

            Assert.AreEqual("AnotherClan", ninjaClan.ClanName);
        }

        [TestCase]
        public void QuerySelectOrderByAndIncludeTestClan()
        {
            _query = new QueryFluent<NinjaClan>(new Repository<NinjaClan>(_ninjaClanContext, _unitOfWorkAsync), new AllNinjaExpressionSpecification());

            var ninjaEquipmentOwned = _query.Include(clan => clan.Ninjas).Select().First().Ninjas.First().EquipmentOwned;

            Assert.IsNotNull(ninjaEquipmentOwned);
            Assert.AreEqual(3, ninjaEquipmentOwned.Count);
        }

        [TestCase]
        public void QuerySelectAndPageTestClan()
        {
            _query = new QueryFluent<NinjaClan>(new Repository<NinjaClan>(_ninjaClanContext, _unitOfWorkAsync), new AllNinjaExpressionSpecification());

            var ninjaClans = _query.SelectPage(1, 1, out var totalCount).ToList();

            Assert.AreEqual(2, totalCount);
            Assert.AreEqual(1, ninjaClans.Count);
        }

        [TestCase]
        public void FindTestClan()
        {
            var ninjaClan = _ninjaClanRepository.Find(1);

            Assert.IsNotNull(ninjaClan);
            Assert.AreEqual(1, ninjaClan.Id);
        }

        #endregion

        #region Utility Methods

        private void CreateClans()
        {
            var ninjaEquipment = new List<NinjaEquipment>
            {
                new NinjaEquipment { Name = "Katana", Type = EquipmentType.Weapon },
                new NinjaEquipment { Name = "Sword", Type = EquipmentType.Weapon },
                new NinjaEquipment { Name = "Kimono", Type = EquipmentType.Outwear }
            };

            var ninjas = new List<Ninja>
            {
                new Ninja {Name = "Ninja1", EquipmentOwned = ninjaEquipment, ServedInOniwaban = true}
            };

            NinjaUtils.CreateClan(1, "TestClan", ninjas, _ninjaClanRepository, _unitOfWorkAsync);

            ninjaEquipment = new List<NinjaEquipment>
            {
                new NinjaEquipment { Name = "Chacos", Type = EquipmentType.Weapon },
                new NinjaEquipment { Name = "Star", Type = EquipmentType.Weapon },
                new NinjaEquipment { Name = "Strength", Type = EquipmentType.Tool },
                new NinjaEquipment { Name = "Velocity", Type = EquipmentType.Tool }
            };

            ninjas = new List<Ninja>
            {
                new Ninja {Name = "Ninja2", EquipmentOwned = ninjaEquipment, ServedInOniwaban = true}
            };

            NinjaUtils.CreateClan(2, "AnotherClan", ninjas, _ninjaClanRepository, _unitOfWorkAsync);
        }

        #endregion
    }
}