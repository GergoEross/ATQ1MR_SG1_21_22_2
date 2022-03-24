using ATQ1MR_HFT_2021221.Client.Infrastructure;
using ATQ1MR_HFT_2021221.Models.Entities;
using ATQ1MR_HFT_2021221.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATQ1MR_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Waiting for server...");
            Console.ReadLine();
            Motherboards();
            Console.WriteLine("*************************************");
            Console.WriteLine("*************************************");
            Console.WriteLine("*************************************");
            Processors();
            Console.WriteLine("*************************************");
            Console.WriteLine("*************************************");
            Console.WriteLine("*************************************");
            MBrands();
            Console.WriteLine("*************************************");
            Console.WriteLine("*************************************");
            Console.WriteLine("*************************************");
            PBrands();

            Console.ReadLine();
        }
        private static void DisplayMotherboardWhitProcessors(List<MotherboardWhitProcessorsModel> motherboardWhitProcessors)
        {
            foreach (var item in motherboardWhitProcessors)
            {
                Console.WriteLine(item);
                Console.WriteLine("************************************************\n");
            }
        }
        private static void DisplayMotherboardProcessorAvaragePrices(List<MotherboardPAvarageModel> motherboardPAvarages)
        {
            foreach (var item in motherboardPAvarages)
            {
                Console.WriteLine(item);
                Console.WriteLine("************************************************\n");
            }
        }
        private static void DisplayBestPricePerPerformace(List<BestPricePerPerformaceModel> bestPricePerPerformaces)
        {
            foreach (var item in bestPricePerPerformaces)
            {
                Console.WriteLine(item);
                Console.WriteLine("************************************************\n");
            }
        }
        private static void DisplayMotherboard(Motherboard motherboard)
        {
            Console.WriteLine("Id: {0}\nType: {1}\nChipset: {2}\nSocket: {3}\nPrice: {4}\nBrandId: {5}\n",
                motherboard.Id, motherboard.Type, motherboard.Chipset, motherboard.Socket, motherboard.Price, motherboard.BrandId);
        }

        private static void DisplayMotherboards(List<Motherboard> motherboards)
        {
            Console.WriteLine();

            foreach (var motherboard in motherboards)
            {
                DisplayMotherboard(motherboard);
            }
        }
        private static void Motherboards()
        {
            var mbHttpService = new HttpService("Motherboard", "http://localhost:51252/api/");

            //Get all
            Console.WriteLine("All motherboards");
            var motherboards = mbHttpService.GetAll<Motherboard>();
            DisplayMotherboards(motherboards);
            Console.WriteLine("************************************************\n");
            //Get one
            Console.WriteLine("Motherboard with Id: 1");
            var motherboard = mbHttpService.Get<Motherboard, int>(1);
            DisplayMotherboard(motherboard);
            Console.WriteLine("************************************************\n");
            //Create
            Console.WriteLine("Create new motherboard");
            var newMotherboard = new Motherboard()
            {
                Type = "PRIME",
                Chipset = "B550",
                Socket = "AM4",
                Price = 25000,
                BrandId = 2
            };
            var result = mbHttpService.Create<Motherboard>(newMotherboard);
            if (result.IsSuccess)
            {
                Console.WriteLine("Creation succsesfull!\n");
            }
            Console.WriteLine("************************************************\n");
            //Check
            motherboards = mbHttpService.GetAll<Motherboard>();
            DisplayMotherboards(motherboards);
            Console.WriteLine("************************************************\n");
            //Update
            Console.WriteLine("Update the created motherboards price");
            var updateMotherboard = motherboards.Last();
            updateMotherboard.Price = 32000;
            result = mbHttpService.Update<Motherboard>(updateMotherboard);
            if (result.IsSuccess)
            {
                Console.WriteLine("Update succsesfull!\n");
            }
            Console.WriteLine("************************************************\n");
            //Check
            motherboards = mbHttpService.GetAll<Motherboard>();
            DisplayMotherboards(motherboards);
            Console.WriteLine("************************************************\n");
            //Delete
            Console.WriteLine("Delete updated motherboard");
            mbHttpService.Delete(motherboards.Last().Id);
            //Check
            motherboards = mbHttpService.GetAll<Motherboard>();
            DisplayMotherboards(motherboards);
            Console.WriteLine("************************************************\n");
            //Get Motherboard Whit Processors
            Console.WriteLine("Motherboards with all of its processors");
            var motherboardWithProcessors = mbHttpService.GetAll<MotherboardWhitProcessorsModel>("GetMotherboardWhitProcessors");
            DisplayMotherboardWhitProcessors(motherboardWithProcessors);
            //Get Motherboard Processor Avarage Prices
            Console.WriteLine("Motherboards with its processors avarage prices");
            var motherboardProcessorAvgPrices = mbHttpService.GetAll<MotherboardPAvarageModel>("GetMotherboardProcessorAvaragePrices");
            DisplayMotherboardProcessorAvaragePrices(motherboardProcessorAvgPrices);
            //Get Best PPP For Motherboard
            Console.WriteLine("Best price/performance processor for motherboard with Id: 1");
            var bestPPPForMotherboard = mbHttpService.GetAll<BestPricePerPerformaceModel, int>(1, "GetBestPPPForMotherboard");
            DisplayBestPricePerPerformace(bestPPPForMotherboard);
        }
        private static void Processors()
        {
            var httpService = new HttpService("Processor", "http://localhost:51252/api/");

            //Get all
            var processors = httpService.GetAll<Processor>();
            Console.WriteLine("All processors");
            DisplayProcessors(processors);
            Console.WriteLine("************************************************\n");
            //Get one
            var processor = httpService.Get<Processor, int>(1);
            Console.WriteLine("Processor whit Id: 1");
            DisplayProcessor(processor);
            Console.WriteLine("************************************************\n");
            //Create
            Console.WriteLine("Create new processor");
            var newProcessor = new Processor() { Name = "Ryzen 7 3700X", BaseClock = 3.6, BoostClock = 4.4, BrandId = 2, 
                Cores = 8, Threads = 16, Socket = "AM4", Price = 114000 };
            var result = httpService.Create<Processor>(newProcessor);
            if (result.IsSuccess)
            {
                Console.WriteLine("Creation succsesfull!\n");
            }
            Console.WriteLine("************************************************\n");
            //Check
            processors = httpService.GetAll<Processor>();
            DisplayProcessors(processors);
            Console.WriteLine("************************************************\n");
            //Update
            Console.WriteLine("Update created processor's price");
            var updateProcessor = processors.Last();
            updateProcessor.Price = 110000;
            result = httpService.Update(updateProcessor);
            if (result.IsSuccess)
            {
                Console.WriteLine("Update succsesfull!\n");
            }
            Console.WriteLine("************************************************\n");
            //Check
            processors = httpService.GetAll<Processor>();
            DisplayProcessors(processors);
            Console.WriteLine("************************************************\n");
            //Delete
            Console.WriteLine("Delete updated processor");
            httpService.Delete(processors.Last().Id);
            processors = httpService.GetAll<Processor>();
            DisplayProcessors(processors);
            //Get Processors Whit Highest Price Motherboard
            Console.WriteLine("Processors with highest price motherboard");
            var pwhpm = httpService.GetAll<ProcessorWhitHighestPriceMotherboardModel>("GetProcessorsWhitHighestPriceMotherboard");
            DisplayProcessorsWhitHighestPriceMotherboard(pwhpm);
        }
        private static void DisplayProcessorsWhitHighestPriceMotherboard(List<ProcessorWhitHighestPriceMotherboardModel> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
        private static void DisplayProcessor(Processor processor)
        {
            Console.WriteLine(processor);
        }
        private static void DisplayProcessors(List<Processor> processors)
        {
            Console.WriteLine();
            foreach (var item in processors)
            {
                DisplayProcessor(item);
            }
        }
        private static void MBrands()
        {
            var httpService = new HttpService("MBrand", "http://localhost:51252/api/");

            //Get all
            var mBrands = httpService.GetAll<MBrand>();
            Console.WriteLine("All MBrands");
            DisplayMBrands(mBrands);
            Console.WriteLine("************************************************\n");
            //Get one
            var mBrand = httpService.Get<MBrand, int>(1);
            Console.WriteLine("MBrand whit Id: 1");
            DisplayMBrand(mBrand);
            Console.WriteLine("************************************************\n");
            //Create
            Console.WriteLine("Create new MBrand");
            var newMBRand = new MBrand() { Name = "GIGABYTE" };
            var result = httpService.Create(newMBRand);
            if (result.IsSuccess)
            {
                Console.WriteLine("Creation succsesfull!");
            }
            Console.WriteLine("************************************************\n");
            //Check
            mBrands = httpService.GetAll<MBrand>();
            DisplayMBrands(mBrands);
            Console.WriteLine("************************************************\n");
            //Update
            Console.WriteLine("Update created MBrand");
            var updateMBrand = mBrands.Last();
            updateMBrand.Name = "ASROCK";
            result = httpService.Update(updateMBrand);
            if (result.IsSuccess)
            {
                Console.WriteLine("Update succsesfull!");
            }
            Console.WriteLine("************************************************\n");
            //Check
            mBrands = httpService.GetAll<MBrand>();
            DisplayMBrands(mBrands);
            Console.WriteLine("************************************************\n");
            //Delete
            Console.WriteLine("Delete updated MBrand");
            httpService.Delete(mBrands.Last().Id);
            //Check
            mBrands = httpService.GetAll<MBrand>();
            DisplayMBrands(mBrands);
            Console.WriteLine("************************************************\n");
            //Get MBrands With Avarage Processor Prices
            Console.WriteLine("MBrands with their average processor prices");
            var mBrandsWithAverageProcessorPrices = httpService.GetAll<MBrandAverageProcessorPricesModel>("GetMBrandsWithAvarageProcessorPrices");
            DisplayMBrandsWithAvarageProcessorPrices(mBrandsWithAverageProcessorPrices);
        }
        private static void DisplayMBrandsWithAvarageProcessorPrices(List<MBrandAverageProcessorPricesModel> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
        private static void DisplayMBrand(MBrand mBrand)
        {
            Console.WriteLine(mBrand);
        }
        private static void DisplayMBrands(List<MBrand> mBrands)
        {
            Console.WriteLine();
            foreach (var item in mBrands)
            {
                DisplayMBrand(item);
            }
        }
        private static void PBrands()
        {
            var httpService = new HttpService("PBrand", "http://localhost:51252/api/");

            //Get all
            var pBrands = httpService.GetAll<PBrand>();
            Console.WriteLine("All PBrands");
            DisplayPBrands(pBrands);
            Console.WriteLine("************************************************\n");
            //Get one
            var pBrand = httpService.Get<PBrand, int>(1);
            Console.WriteLine("PBrand whit Id: 1");
            DisplayPBrand(pBrand);
            Console.WriteLine("************************************************\n");
            //Create
            Console.WriteLine("Create new PBrand");
            var newPBrand = new PBrand() { Name = "Hygon" };
            var result = httpService.Create(newPBrand);
            if (result.IsSuccess)
            {
                Console.WriteLine("Creation succsesfull!");
            }
            Console.WriteLine("************************************************\n");
            //Check
            pBrands = httpService.GetAll<PBrand>();
            DisplayPBrands(pBrands);
            Console.WriteLine("************************************************\n");
            //Update
            Console.WriteLine("Update created PBrand");
            var updatePBrand = pBrands.Last();
            updatePBrand.Name = "Via";
            result = httpService.Update(updatePBrand);
            if (result.IsSuccess)
            {
                Console.WriteLine("Update succsesfull!");
            }
            Console.WriteLine("************************************************\n");
            //Check
            pBrands = httpService.GetAll<PBrand>();
            DisplayPBrands(pBrands);
            Console.WriteLine("************************************************\n");
            //Delete
            Console.WriteLine("Delete updated PBrand");
            httpService.Delete(pBrands.Last().Id);
            //Check
            pBrands = httpService.GetAll<PBrand>();
            DisplayPBrands(pBrands);
        }
        private static void DisplayPBrand(PBrand pBrand)
        {
            Console.WriteLine(pBrand);
        }
        private static void DisplayPBrands(List<PBrand> pBrands)
        {
            Console.WriteLine();
            foreach (var item in pBrands)
            {
                DisplayPBrand(item);
            }
        }
    }
}
