using ATQ1MR_HFT_2021221.Logic.Intefaces;

using ATQ1MR_HFT_2021221.Models.Entities;
using ATQ1MR_HFT_2021221.Models.Models;
using ATQ1MR_HFT_2021221.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_HFT_2021221.Logic.Services
{
    public class MotherboardLogic : IMotherboardLogic
    {
        IMBrandRepository _mBrandRepository;
        IMotherboardRepository _motherboardRepository;
        IProcessorRepository _processorRepository;

        public MotherboardLogic(IMBrandRepository mBrandRepository, IMotherboardRepository motherboardRepository, IProcessorRepository processorRepository)
        {
            _mBrandRepository = mBrandRepository;
            _motherboardRepository = motherboardRepository;
            _processorRepository = processorRepository;
        }

        public IList<Motherboard> ReadAll()
        {
            return _motherboardRepository.ReadAll().ToList();
        }
        public Motherboard Read(int id)
        {
            return _motherboardRepository.Read(id);
        }
        public Motherboard Create(Motherboard entity)
        {
            if (entity != null && entity.Chipset != "" && entity.Socket != "" && entity.Type != "")
            {
                var v = _motherboardRepository.Read(entity.Id);
                if (v == null)
                {
                    var result = _motherboardRepository.Create(entity);
                    return result;
                }
                else
                {
                    throw new Exception("Already exists!");
                }
            }
            else
            {
                throw new Exception("Must contain the required data!");
            }
        }
        public Motherboard Update(Motherboard entity)
        {
            if (entity != null)
            {
                var v = _motherboardRepository.Read(entity.Id);
                if (v != null)
                {
                    v.BrandId = entity.BrandId;
                    v.Chipset = entity.Chipset;
                    v.Price = entity.Price;
                    v.Socket = entity.Socket;
                    v.Type = entity.Type;
                    var result = _motherboardRepository.Update(v);
                    return result;
                }
                else
                {
                    throw new Exception("No entity found!");
                }
            }
            else
            {
                throw new Exception("Must contain data!");
            }
        }
        public void Delete(int id)
        {
            var v = _motherboardRepository.Read(id);
            if (v != null)
            {
                _motherboardRepository.Delete(id);
            }
            else
            {
                throw new Exception("No entity found!");
            }
        }

        public IEnumerable<MotherboardWhitProcessorsModel> MotherboardsWhitItsProcessors()
        {
            var processors = _processorRepository.ReadAll().ToList();
            var motherboards = _motherboardRepository.ReadAll().ToList();
            var mBrands = _mBrandRepository.ReadAll().ToList();

            var proc = from processor in processors
                       group processor by processor.Socket into g
                       select new
                       {
                           Socket = g.Key,
                           Processors = g.Select(x => x).OrderByDescending(x => x.Price).ToList()
                       };

            var result = from motherboar in motherboards
                         join brand in mBrands
                         on motherboar.BrandId equals brand.Id
                         join processor in proc
                         on motherboar.Socket equals processor.Socket
                         select new MotherboardWhitProcessorsModel
                         {
                             Chipset = motherboar.Chipset,
                             Type = motherboar.Type,
                             Brand = brand.Name,
                             Processors = processor.Processors
                         };

            return result.ToList();
        }

        public IEnumerable<MotherboardPAvarageModel> MotherboardProcessorAvaragePrices()
        {
            var motherboards = _motherboardRepository.ReadAll();
            var processors = _processorRepository.ReadAll();
            var mBrands = _mBrandRepository.ReadAll();

            var avarages = from processor in processors
                           group processor by processor.Socket into g
                           select new
                           {
                               Socket = g.Key,
                               Avarage = g.Average(x => x.Price)
                           };
            var result = from motherboard in motherboards
                         join brand in mBrands
                         on motherboard.BrandId equals brand.Id
                         join avarage in avarages
                         on motherboard.Socket equals avarage.Socket
                         select new MotherboardPAvarageModel
                         {
                             Chipset = motherboard.Chipset,
                             Type = motherboard.Type,
                             Brand = brand.Name,
                             Avarage = avarage.Avarage
                         };

            return result.ToList();
        }

        public IEnumerable<BestPricePerPerformaceModel> BestPPPForMotherboard(int id)
        {
            var motherboards = _motherboardRepository.ReadAll().ToList();
            var processors = _processorRepository.ReadAll().ToList();

            var procppps = from processor in processors
                            select new
                            {
                                Socket = processor.Socket,
                                Name = processor.Name,
                                PPP = processor.Price / (processor.Cores * ((processor.BaseClock + processor.BoostClock) / 2))
                            };
            var ordered = from proc in procppps
                         group proc by proc.Socket into g
                         select new
                         {
                             Socket = g.Key,
                             Name = g.OrderBy(x => x.PPP).Select(x => x.Name).First(),
                             PPP = g.Select(x => x.PPP).OrderBy(x => x).First()
                         };
            var result = from motherboard in motherboards
                         join proc in ordered
                         on motherboard.Socket equals proc.Socket
                         where motherboard.Id == id
                         select new BestPricePerPerformaceModel
                         {
                             ProcessorName = proc.Name,
                             PPP = proc.PPP
                         };


            return result.ToList();
        }
    }
}
