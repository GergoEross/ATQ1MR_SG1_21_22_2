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
    class MBrandLogicTests
    {
        #region constants
        const string socket1 = "testsocket1";
        const string socket2 = "testsocket2";
        const string socket3 = "testsocket3";
        #endregion
        [Test]
        public void CreateTestWithProperData()
        {
            //Arrange
            var mBrandRepo = new Mock<IMBrandRepository>();

            var mBrand1 = new MBrand() { Id = 1, Name = "mBrandtest1" };

            mBrandRepo.Setup(x => x.Create(mBrand1)).Returns(mBrand1);

            var logic = new MBrandLogic(mBrandRepo.Object, null, null);
            //Act
            var result = logic.Create(mBrand1);
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(mBrand1));
        }
        [Test]
        public void CreateTestWithNull()
        {
            //Arrange
            var mBrandRepo = new Mock<IMBrandRepository>();

            var logic = new MBrandLogic(mBrandRepo.Object, null, null);
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
            var mBrandRepo = new Mock<IMBrandRepository>();

            var mBrand1 = new MBrand() { Id = 1, Name = "" };

            var logic = new MBrandLogic(mBrandRepo.Object, null, null);
            //Act
            var result1 = Assert.Throws(typeof(Exception), () => logic.Create(mBrand1));

            //Assert
            Assert.That(result1, Is.Not.Null);
            Assert.That(result1.Message, Is.EqualTo("Must contain the required data!"));
        }

        [Test]
        public void UpdateTestWithNonExistetData()
        {
            //Arrange
            var mBrandRepo = new Mock<IMBrandRepository>();

            var mBrand1 = new MBrand() { Id = 1, Name = "mBrandtest1" };

            mBrandRepo.Setup(x => x.Update(mBrand1)).Returns(mBrand1);

            var logic = new MBrandLogic(mBrandRepo.Object, null, null);
            //Act
            var result = Assert.Throws(typeof(Exception), () => logic.Update(mBrand1));
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Message, Is.EqualTo("No entity found!"));
        }
        [Test]
        public void UpdateTestWithNull()
        {
            //Arrange
            var mBrandRepo = new Mock<IMBrandRepository>();

            var logic = new MBrandLogic(mBrandRepo.Object, null, null);
            //Act
            var result = Assert.Throws(typeof(Exception), () => logic.Update(null));
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Message, Is.EqualTo("Must contain data!"));
        }
        [TestCaseSource(nameof(GetMBrandsWithAvarageProcessorPricesTestData))]
        public void MBrandsWithAvarageProcessorPricesTest(List<Processor> pros, List<MBrand> mBrands, List<PBrand> pBrands, List<Motherboard> mbs, List<MBrandAverageProcessorPricesModel> expected)
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

            var logic = new MBrandLogic(mBrandRepo.Object, motherboardRepo.Object, processorRepo.Object);
            //Act
            var result = logic.MBrandsWithAvarageProcessorPrices();
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EquivalentTo(expected));
        }
        #region Utils
        static List<TestCaseData> GetMBrandsWithAvarageProcessorPricesTestData()
        {
            var result = new List<TestCaseData>();

            var mBrand1 = new MBrand() { Id = 1, Name = "mBrandtest1" };
            var mBrand2 = new MBrand() { Id = 2, Name = "mBrandtest2" };

            var pBrand1 = new PBrand() { Id = 1, Name = "AMD" };
            var pBrand2 = new PBrand() { Id = 2, Name = "INTEL" };

            var mb1 = new Motherboard() { Id = 1, BrandId = 1, Chipset = "testset1", Price = 1000, Socket = socket1, Type = "testtype1" };
            var mb2 = new Motherboard() { Id = 2, BrandId = 2, Chipset = "testset2", Price = 1050, Socket = socket2, Type = "testtype2" };
            var mb3 = new Motherboard() { Id = 3, BrandId = 1, Chipset = "testset3", Price = 1150, Socket = socket2, Type = "testtype3" };
            var mb4 = new Motherboard() { Id = 4, BrandId = 2, Chipset = "testset4", Price = 1250, Socket = socket1, Type = "testtype4" };
            var mb5 = new Motherboard() { Id = 5, BrandId = 2, Chipset = "testset5", Price = 2250, Socket = socket3, Type = "testtype5" };

            var pro1 = new Processor() { Id = 1, BrandId = 1, Name = "testname1", Socket = socket1, BaseClock = 3.5, BoostClock = 4.1, Cores = 4, Price = 2000, Threads = 8 };
            var pro2 = new Processor() { Id = 2, BrandId = 1, Name = "testname2", Socket = socket1, BaseClock = 3.8, BoostClock = 4.5, Cores = 4, Price = 1500, Threads = 4 };
            var pro3 = new Processor() { Id = 3, BrandId = 1, Name = "testname3", Socket = socket1, BaseClock = 4, BoostClock = 4.8, Cores = 6, Price = 3200, Threads = 12 };
            var pro4 = new Processor() { Id = 4, BrandId = 2, Name = "testname4", Socket = socket2, BaseClock = 3, BoostClock = 4, Cores = 6, Price = 2200, Threads = 12 };
            var pro5 = new Processor() { Id = 5, BrandId = 2, Name = "testname5", Socket = socket2, BaseClock = 4.2, BoostClock = 5, Cores = 8, Price = 5200, Threads = 16 };
            var pro6 = new Processor() { Id = 6, BrandId = 2, Name = "testname6", Socket = socket2, BaseClock = 3.4, BoostClock = 4.2, Cores = 4, Price = 2600, Threads = 8 };
            var pro7 = new Processor() { Id = 7, BrandId = 2, Name = "testname7", Socket = socket2, BaseClock = 3.4, BoostClock = 4.2, Cores = 4, Price = 2100, Threads = 8 };
            var pro8 = new Processor() { Id = 8, BrandId = 1, Name = "testname8", Socket = socket1, BaseClock = 3.4, BoostClock = 4.2, Cores = 4, Price = 2300, Threads = 8 };
            var pro9 = new Processor() { Id = 9, BrandId = 2, Name = "testname9", Socket = socket2, BaseClock = 3.4, BoostClock = 4.2, Cores = 4, Price = 2400, Threads = 8 };
            var pro10 = new Processor() { Id = 10, BrandId = 1, Name = "testname10", Socket = socket1, BaseClock = 3.4, BoostClock = 4.2, Cores = 4, Price = 2700, Threads = 8 };
            var pro11 = new Processor() { Id = 11, BrandId = 1, Name = "testname11", Socket = socket3, BaseClock = 3.4, BoostClock = 4.2, Cores = 4, Price = 3600, Threads = 8 };

            var mBrands = new List<MBrand>() { mBrand1, mBrand2 };
            var pBrands = new List<PBrand>() { pBrand1, pBrand2 };
            var mbs = new List<Motherboard>() { mb1, mb2, mb3, mb4, mb5 };
            var pros = new List<Processor>() { pro1, pro2, pro3, pro4, pro5, pro6, pro7, pro8, pro9, pro10, pro11 };

            var res1 = new MBrandAverageProcessorPricesModel()
            {
                MBrandName = "mBrandtest1",
                Average = (pro1.Price + pro2.Price + pro3.Price + pro4.Price + pro5.Price + pro6.Price + pro7.Price + pro8.Price + pro9.Price + pro10.Price) / (double)10
            };
            var res2 = new MBrandAverageProcessorPricesModel()
            {
                MBrandName = "mBrandtest2",
                Average = (pro1.Price + pro2.Price + pro3.Price + pro4.Price + pro5.Price + pro6.Price + pro7.Price + pro8.Price + pro9.Price + pro10.Price + pro11.Price) / (double)11
            };
            var expected = new List<MBrandAverageProcessorPricesModel>() { res1, res2 };

            result.Add(new TestCaseData(pros, mBrands, pBrands, mbs, expected));

            return result;
        }
        #endregion
    }
}
