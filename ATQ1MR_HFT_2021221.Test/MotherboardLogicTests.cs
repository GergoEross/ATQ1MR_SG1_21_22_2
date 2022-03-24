using ATQ1MR_HFT_2021221.Logic.Services;
using ATQ1MR_HFT_2021221.Models.Entities;
using ATQ1MR_HFT_2021221.Models.Models;
using ATQ1MR_HFT_2021221.Repository.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_HFT_2021221.Test
{
    [TestFixture]
    public class MotherboardLogicTests
    {
        #region constants
        const string socket1 = "testsocket1";
        const string socket2 = "testsocket2";
        #endregion
        [Test]
        public void CreateTestWithProperData()
        {
            //Arrange
            var motherboardRepo = new Mock<IMotherboardRepository>();

            var mboard1 = new Motherboard() { Id = 1, Chipset = "B450", Price = 30000, Socket = "AM4", Type = "TOMAHAWK MAX" };

            motherboardRepo.Setup(x => x.Create(mboard1)).Returns(mboard1);

            var logic = new MotherboardLogic(null, motherboardRepo.Object, null);
            //Act
            var result = logic.Create(mboard1);
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(mboard1));
        }
        [Test]
        public void CreateTestWithNull()
        {
            //Arrange
            var motherboardRepo = new Mock<IMotherboardRepository>();

            var logic = new MotherboardLogic(null, motherboardRepo.Object, null);
            //Act
            var result = Assert.Throws(typeof(Exception), () => logic.Create(null));
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Message, Is.EqualTo("Must contain the required data!"));
        }

        [Test]
        public void CreateTestWithEmptyString()
        {
            //Arrange
            var motherboardRepo = new Mock<IMotherboardRepository>();

            var mboard1 = new Motherboard() { Id = 2, Price = 1050, Socket="", Chipset = "testset", Type = "asd" };
            var mboard2 = new Motherboard() { Id = 2, Price = 1050, Socket="testsocket", Chipset = "", Type = "asd" };
            var mboard3 = new Motherboard() { Id = 2, Price = 1050, Socket="testsocket", Chipset = "testset", Type = "" };

            var logic = new MotherboardLogic(null, motherboardRepo.Object, null);
            //Act
            var result1 = Assert.Throws(typeof(Exception), () => logic.Create(mboard1));
            var result2 = Assert.Throws(typeof(Exception), () => logic.Create(mboard2));
            var result3 = Assert.Throws(typeof(Exception), () => logic.Create(mboard3));
            //Assert
            Assert.That(result1, Is.Not.Null);
            Assert.That(result1.Message, Is.EqualTo("Must contain the required data!"));
            Assert.That(result2, Is.Not.Null);
            Assert.That(result2.Message, Is.EqualTo("Must contain the required data!"));
            Assert.That(result3, Is.Not.Null);
            Assert.That(result3.Message, Is.EqualTo("Must contain the required data!"));
        }
        [Test]
        public void UpdateTestWithNonExistetData()
        {
            //Arrange
            var motherboardRepo = new Mock<IMotherboardRepository>();

            var mboard1 = new Motherboard() { Id = 1, Chipset = "B450", Price = 30000, Socket = "AM4", Type = "TOMAHAWK MAX" };

            motherboardRepo.Setup(x => x.Update(mboard1)).Returns(mboard1);

            var logic = new MotherboardLogic(null, motherboardRepo.Object, null);
            //Act
            var result = Assert.Throws(typeof(Exception), () => logic.Update(mboard1));
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Message, Is.EqualTo("No entity found!"));
        }
        [Test]
        public void UpdateTestWithNull()
        {
            //Arrange
            var motherboardRepo = new Mock<IMotherboardRepository>();

            var logic = new MotherboardLogic(null, motherboardRepo.Object, null);
            //Act
            var result = Assert.Throws(typeof(Exception), () => logic.Update(null));
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Message, Is.EqualTo("Must contain data!"));
        }
        [TestCaseSource(nameof(GetMotherboardsWhitItsProcessorsTestData))]
        public void MotherboardsWhitItsProcessorsTest(List<Processor> pros, List<MBrand> mBrands, List<PBrand> pBrands, List<Motherboard> mbs, List<MotherboardWhitProcessorsModel> expectedRes)
        {
            //Arrange
            var motherboardRepo = new Mock<IMotherboardRepository>();
            var processorRepo = new Mock<IProcessorRepository>();
            var mBrandRepo = new Mock<IMBrandRepository>();
            var pBrandRepo = new Mock<IPBrandRepository>();

            motherboardRepo.Setup(x => x.ReadAll()).Returns(mbs.AsQueryable());
            processorRepo.Setup(x => x.ReadAll()).Returns(pros.AsQueryable());
            mBrandRepo.Setup(x => x.ReadAll()).Returns(mBrands.AsQueryable());
            pBrandRepo.Setup(x => x.ReadAll()).Returns(pBrands.AsQueryable());

            var logic = new MotherboardLogic(mBrandRepo.Object, motherboardRepo.Object, processorRepo.Object);

            //Act
            var result = logic.MotherboardsWhitItsProcessors();
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EquivalentTo(expectedRes));
        }
        [TestCaseSource(nameof(GetMotherboardProcessorAvaragePricesTestData))]
        public void MotherboardProcessorAvaragePricesTest(List<Processor> pros, List<MBrand> mBrands, List<PBrand> pBrands, List<Motherboard> mbs, List<MotherboardPAvarageModel> expectedRes)
        {
            //Arrange
            var motherboardRepo = new Mock<IMotherboardRepository>();
            var processorRepo = new Mock<IProcessorRepository>();
            var mBrandRepo = new Mock<IMBrandRepository>();
            var pBrandRepo = new Mock<IPBrandRepository>();

            motherboardRepo.Setup(x => x.ReadAll()).Returns(mbs.AsQueryable());
            processorRepo.Setup(x => x.ReadAll()).Returns(pros.AsQueryable());
            mBrandRepo.Setup(x => x.ReadAll()).Returns(mBrands.AsQueryable());
            pBrandRepo.Setup(x => x.ReadAll()).Returns(pBrands.AsQueryable());

            var logic = new MotherboardLogic(mBrandRepo.Object, motherboardRepo.Object, processorRepo.Object);

            //Act
            var result = logic.MotherboardProcessorAvaragePrices();
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EquivalentTo(expectedRes));
        }
        [TestCaseSource(nameof(GetBestPPPForMotherboardTestData))]
        public void BestPPPForMotherboardTest(List<Processor> pros, List<MBrand> mBrands, List<PBrand> pBrands, List<Motherboard> mbs, List<BestPricePerPerformaceModel> expectedRes, int id)
        {
            //Arrange
            var motherboardRepo = new Mock<IMotherboardRepository>();
            var processorRepo = new Mock<IProcessorRepository>();
            var mBrandRepo = new Mock<IMBrandRepository>();
            var pBrandRepo = new Mock<IPBrandRepository>();

            motherboardRepo.Setup(x => x.ReadAll()).Returns(mbs.AsQueryable());
            processorRepo.Setup(x => x.ReadAll()).Returns(pros.AsQueryable());
            mBrandRepo.Setup(x => x.ReadAll()).Returns(mBrands.AsQueryable());
            pBrandRepo.Setup(x => x.ReadAll()).Returns(pBrands.AsQueryable());

            var logic = new MotherboardLogic(mBrandRepo.Object, motherboardRepo.Object, processorRepo.Object);
            //Act
            var result = logic.BestPPPForMotherboard(id);
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EquivalentTo(expectedRes));
        }

        #region Utils
        
        static List<TestCaseData> GetMotherboardsWhitItsProcessorsTestData()
        {
            var result = new List<TestCaseData>();

            var mBrand1 = new MBrand() { Id = 1, Name = "mBrandtest1" };
            var mBrand2 = new MBrand() { Id = 2, Name = "mBrandtest2" };
            var pBrand1 = new PBrand() { Id = 1, Name = "AMD" };
            var pBrand2 = new PBrand() { Id = 2, Name = "INTEL" };

            var mb1 = new Motherboard() { Id = 1, BrandId = 1, Chipset = "testset1", Price = 1000, Socket = socket1, Type = "testtype1" };
            var mb2 = new Motherboard() { Id = 2, BrandId = 2, Chipset = "testset2", Price = 1050, Socket = socket2, Type = "testtype2" };

            var pro1 = new Processor() { Id = 1, BrandId = 1, Name = "testname1", Socket = socket1, BaseClock = 3.5, BoostClock = 4.1, Cores = 4, Price = 2000, Threads = 8 };
            var pro2 = new Processor() { Id = 2, BrandId = 1, Name = "testname2", Socket = socket1, BaseClock = 3.8, BoostClock = 4.5, Cores = 4, Price = 1500, Threads = 4 };
            var pro3 = new Processor() { Id = 3, BrandId = 1, Name = "testname3", Socket = socket1, BaseClock = 4, BoostClock = 4.8, Cores = 6, Price = 3200, Threads = 12 };
            var pro4 = new Processor() { Id = 4, BrandId = 2, Name = "testname4", Socket = socket2, BaseClock = 3, BoostClock = 4, Cores = 6, Price = 2200, Threads = 12 };
            var pro5 = new Processor() { Id = 5, BrandId = 2, Name = "testname5", Socket = socket2, BaseClock = 4.2, BoostClock = 5, Cores = 8, Price = 5200, Threads = 16 };
            var pro6 = new Processor() { Id = 6, BrandId = 2, Name = "testname6", Socket = socket2, BaseClock = 3.4, BoostClock = 4.2, Cores = 4, Price = 2600, Threads = 8 };

            var mBrands = new List<MBrand>() { mBrand1, mBrand2 };
            var pBrands = new List<PBrand>() { pBrand1, pBrand2 };
            var mbs = new List<Motherboard>() { mb1, mb2 };
            var pros = new List<Processor>() { pro1, pro2, pro3, pro4, pro5, pro6 };

            var ordpros1 = new List<Processor>();
            ordpros1.Add(pro3);
            ordpros1.Add(pro1);
            ordpros1.Add(pro2);
            var res1 = new MotherboardWhitProcessorsModel() { Brand = "mBrandtest1", Chipset = "testset1", Type = "testtype1", Processors = ordpros1 };

            var ordpros2 = new List<Processor>();
            ordpros2.Add(pro5);
            ordpros2.Add(pro6);
            ordpros2.Add(pro4);
            var res2 = new MotherboardWhitProcessorsModel() { Brand = "mBrandtest2", Chipset = "testset2", Type = "testtype2", Processors = ordpros2 };

            var expectedRes = new List<MotherboardWhitProcessorsModel>();
            expectedRes.Add(res1);
            expectedRes.Add(res2);

            //Empty
            result.Add(new TestCaseData(
                new List<Processor>(),
                new List<MBrand>(),
                new List<PBrand>(),
                new List<Motherboard>(),
                new List<MotherboardWhitProcessorsModel>()
            ));
            //Multiple motherboards with multiple processors
            result.Add(new TestCaseData(pros, mBrands, pBrands, mbs, expectedRes));

            return result;
        }
        static List<TestCaseData> GetMotherboardProcessorAvaragePricesTestData()
        {
            var result = new List<TestCaseData>();

            var mBrand1 = new MBrand() { Id = 1, Name = "mBrandtest1" };
            var mBrand2 = new MBrand() { Id = 2, Name = "mBrandtest2" };
            var pBrand1 = new PBrand() { Id = 1, Name = "AMD" };
            var pBrand2 = new PBrand() { Id = 2, Name = "INTEL" };

            var mb1 = new Motherboard() { Id = 1, BrandId = 1, Chipset = "testset1", Price = 1000, Socket = socket1, Type = "testtype1" };
            var mb2 = new Motherboard() { Id = 2, BrandId = 2, Chipset = "testset2", Price = 1050, Socket = socket2, Type = "testtype2" };

            var pro1 = new Processor() { Id = 1, BrandId = 1, Name = "testname1", Socket = socket1, BaseClock = 3.5, BoostClock = 4.1, Cores = 4, Price = 2000, Threads = 8 };
            var pro2 = new Processor() { Id = 2, BrandId = 1, Name = "testname2", Socket = socket1, BaseClock = 3.8, BoostClock = 4.5, Cores = 4, Price = 1500, Threads = 4 };
            var pro3 = new Processor() { Id = 3, BrandId = 1, Name = "testname3", Socket = socket1, BaseClock = 4, BoostClock = 4.8, Cores = 6, Price = 3200, Threads = 12 };
            var pro4 = new Processor() { Id = 4, BrandId = 2, Name = "testname4", Socket = socket2, BaseClock = 3, BoostClock = 4, Cores = 6, Price = 2200, Threads = 12 };
            var pro5 = new Processor() { Id = 5, BrandId = 2, Name = "testname5", Socket = socket2, BaseClock = 4.2, BoostClock = 5, Cores = 8, Price = 5200, Threads = 16 };
            var pro6 = new Processor() { Id = 6, BrandId = 2, Name = "testname6", Socket = socket2, BaseClock = 3.4, BoostClock = 4.2, Cores = 4, Price = 2600, Threads = 8 };

            var mBrands = new List<MBrand>() { mBrand1, mBrand2 };
            var pBrands = new List<PBrand>() { pBrand1, pBrand2 };
            var mbs = new List<Motherboard>() { mb1, mb2 };
            var pros = new List<Processor>() { pro1, pro2, pro3, pro4, pro5, pro6 };

            var pros1 = new List<Processor>();
            pros1.Add(pro1);
            pros1.Add(pro2);
            pros1.Add(pro3);
            var res1 = new MotherboardPAvarageModel() { Brand = "mBrandtest1", Chipset = "testset1", Type = "testtype1", Avarage = pros1.Average(x => x.Price) };

            var pros2 = new List<Processor>();
            pros2.Add(pro4);
            pros2.Add(pro5);
            pros2.Add(pro6);
            var res2 = new MotherboardPAvarageModel() { Brand = "mBrandtest2", Chipset = "testset2", Type = "testtype2", Avarage = pros2.Average(x => x.Price) };

            var expectedRes = new List<MotherboardPAvarageModel>();
            expectedRes.Add(res1);
            expectedRes.Add(res2);

            //Empty
            result.Add(new TestCaseData(
                new List<Processor>(),
                new List<MBrand>(),
                new List<PBrand>(),
                new List<Motherboard>(),
                new List<MotherboardPAvarageModel>()
            ));
            //Multiple motherboards with multiple processors
            result.Add(new TestCaseData(pros, mBrands, pBrands, mbs, expectedRes));

            return result;
        }
        static List<TestCaseData> GetBestPPPForMotherboardTestData()
        {
            var result = new List<TestCaseData>();

            var mBrand1 = new MBrand() { Id = 1, Name = "mBrandtest1" };
            var mBrand2 = new MBrand() { Id = 2, Name = "mBrandtest2" };
            var pBrand1 = new PBrand() { Id = 1, Name = "AMD" };
            var pBrand2 = new PBrand() { Id = 2, Name = "INTEL" };

            var mb1 = new Motherboard() { Id = 1, BrandId = 1, Chipset = "testset1", Price = 1000, Socket = socket1, Type = "testtype1" };
            var mb2 = new Motherboard() { Id = 2, BrandId = 2, Chipset = "testset2", Price = 1050, Socket = socket2, Type = "testtype2" };

            var pro1 = new Processor() { Id = 1, BrandId = 1, Name = "testname1", Socket = socket1, BaseClock = 3.5, BoostClock = 4.1, Cores = 4, Price = 2000, Threads = 8 };
            var pro2 = new Processor() { Id = 2, BrandId = 1, Name = "testname2", Socket = socket1, BaseClock = 3.8, BoostClock = 4.5, Cores = 4, Price = 1500, Threads = 4 };
            var pro3 = new Processor() { Id = 3, BrandId = 1, Name = "testname3", Socket = socket1, BaseClock = 4, BoostClock = 4.8, Cores = 6, Price = 3200, Threads = 12 };
            var pro4 = new Processor() { Id = 4, BrandId = 2, Name = "testname4", Socket = socket2, BaseClock = 3, BoostClock = 4, Cores = 6, Price = 2200, Threads = 12 };
            var pro5 = new Processor() { Id = 5, BrandId = 2, Name = "testname5", Socket = socket2, BaseClock = 4.2, BoostClock = 5, Cores = 8, Price = 5200, Threads = 16 };
            var pro6 = new Processor() { Id = 6, BrandId = 2, Name = "testname6", Socket = socket2, BaseClock = 3.4, BoostClock = 4.2, Cores = 4, Price = 2600, Threads = 8 };

            var mBrands = new List<MBrand>() { mBrand1, mBrand2 };
            var pBrands = new List<PBrand>() { pBrand1, pBrand2 };
            var mbs = new List<Motherboard>() { mb1, mb2 };
            var pros = new List<Processor>() { pro1, pro2, pro3, pro4, pro5, pro6 };

            var res1 = new BestPricePerPerformaceModel() { ProcessorName = "testname2", PPP = pro2.Price / (pro2.Cores * ((pro2.BaseClock + pro2.BoostClock) / 2)) };

            var expectedRes = new List<BestPricePerPerformaceModel>();
            expectedRes.Add(res1);

            //Empty
            result.Add(new TestCaseData(
                new List<Processor>(),
                new List<MBrand>(),
                new List<PBrand>(),
                new List<Motherboard>(),
                new List<BestPricePerPerformaceModel>(),
                null
            ));
            //Multiple motherboards with multiple processors
            result.Add(new TestCaseData(pros, mBrands, pBrands, mbs, expectedRes, 1));

            return result;
        }
        #endregion
    }
}
