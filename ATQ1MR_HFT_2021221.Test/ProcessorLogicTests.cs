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
    class ProcessorLogicTests
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
            var processorRepo = new Mock<IProcessorRepository>();

            var pro1 = new Processor() { Id = 1, BrandId = 1, Name = "testname1", Socket = "testsocket1", BaseClock = 3.5, BoostClock = 4.1, Cores = 4, Price = 2000, Threads = 8 };

            processorRepo.Setup(x => x.Create(pro1)).Returns(pro1);

            var logic = new ProcessorLogic(null, null, processorRepo.Object, null);
            //Act
            var result = logic.Create(pro1);
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(pro1));
        }
        [Test]
        public void CreateTestWithNull()
        {
            //Arrange
            var processorRepo = new Mock<IProcessorRepository>();

            var logic = new ProcessorLogic(null, null, processorRepo.Object, null);
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
            var processorRepo = new Mock<IProcessorRepository>();

            var pro1 = new Processor() { Id = 1, BrandId = 1, Name = "", Socket = "testsocket1", BaseClock = 3.5, BoostClock = 4.1, Cores = 4, Price = 2000, Threads = 8 };

            var logic = new ProcessorLogic(null, null, processorRepo.Object, null);
            //Act
            var result1 = Assert.Throws(typeof(Exception), () => logic.Create(pro1));
            
            //Assert
            Assert.That(result1, Is.Not.Null);
            Assert.That(result1.Message, Is.EqualTo("Must contain the required data!"));
        }

        [Test]
        public void UpdateTestWithNonExistetData()
        {
            //Arrange
            var processorRepo = new Mock<IProcessorRepository>();

            var pro1 = new Processor() { Id = 1, BrandId = 1, Name = "testname1", Socket = "testsocket1", BaseClock = 3.5, BoostClock = 4.1, Cores = 4, Price = 2000, Threads = 8 };

            processorRepo.Setup(x => x.Update(pro1)).Returns(pro1);

            var logic = new ProcessorLogic(null, null, processorRepo.Object, null);
            //Act
            var result = Assert.Throws(typeof(Exception), () => logic.Update(pro1));
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Message, Is.EqualTo("No entity found!"));
        }
        [Test]
        public void UpdateTestWithNull()
        {
            //Arrange
            var processorRepo = new Mock<IProcessorRepository>();

            var logic = new ProcessorLogic(null, null, processorRepo.Object, null);
            //Act
            var result = Assert.Throws(typeof(Exception), () => logic.Update(null));
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Message, Is.EqualTo("Must contain data!"));
        }
        [TestCaseSource(nameof(GetProcessorWhitHighestPriceMotherboardTestData))]
        public void ProcessorWhitHighestPriceMotherboardTest(List<Processor> pros, List<MBrand> mBrands, List<PBrand> pBrands, List<Motherboard> mbs, List<ProcessorWhitHighestPriceMotherboardModel> expected)
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

            var logic = new ProcessorLogic(pBrandRepo.Object, motherboardRepo.Object, processorRepo.Object, mBrandRepo.Object);
            //Act
            var result = logic.ProcessorWhitHighestPriceMotherboard();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EquivalentTo(expected));
        }
        #region Utils
        static List<TestCaseData> GetProcessorWhitHighestPriceMotherboardTestData()
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
            var pro2 = new Processor() { Id = 2, BrandId = 2, Name = "testname2", Socket = socket2, BaseClock = 3.8, BoostClock = 4.5, Cores = 4, Price = 1500, Threads = 4 };
            var pro3 = new Processor() { Id = 3, BrandId = 2, Name = "testname3", Socket = socket3, BaseClock = 4, BoostClock = 4.8, Cores = 6, Price = 3200, Threads = 12 };

            var mBrands = new List<MBrand>() { mBrand1, mBrand2 };
            var pBrands = new List<PBrand>() { pBrand1, pBrand2 };
            var mbs = new List<Motherboard>() { mb1, mb2, mb3, mb4, mb5 };
            var pros = new List<Processor>() { pro1, pro2, pro3};

            var res1 = new ProcessorWhitHighestPriceMotherboardModel() { Brand = mBrand2.Name, Chipset = mb4.Chipset, Type = mb4.Type, Price = mb4.Price, Name = pro1.Name };
            var res2 = new ProcessorWhitHighestPriceMotherboardModel() { Brand = mBrand1.Name, Chipset = mb3.Chipset, Type = mb3.Type, Price = mb3.Price, Name = pro2.Name };
            var res3 = new ProcessorWhitHighestPriceMotherboardModel() { Brand = mBrand2.Name, Chipset = mb5.Chipset, Type = mb5.Type, Price = mb5.Price, Name = pro3.Name };


            var expected = new List<ProcessorWhitHighestPriceMotherboardModel>() { res1, res2, res3 };

            result.Add(new TestCaseData(pros, mBrands, pBrands, mbs, expected));

            return result;
        }
        #endregion
    }
}
