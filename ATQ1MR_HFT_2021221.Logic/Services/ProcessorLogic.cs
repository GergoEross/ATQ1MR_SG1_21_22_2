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
    public class ProcessorLogic : IProcessorLogic
    {
        IPBrandRepository _pBrandRepository;
        IMotherboardRepository _motherboardRepository;
        IProcessorRepository _processorRepository;
        IMBrandRepository _mBrandRepository;

        public ProcessorLogic(IPBrandRepository pBrandRepository, IMotherboardRepository motherboardRepository, IProcessorRepository processorRepository, IMBrandRepository mBrandRepository)
        {
            _pBrandRepository = pBrandRepository;
            _motherboardRepository = motherboardRepository;
            _processorRepository = processorRepository;
            _mBrandRepository = mBrandRepository;
        }

        public IList<Processor> ReadAll()
        {
            return _processorRepository.ReadAll().ToList();
        }
        public Processor Read(int id)
        {
            return _processorRepository.Read(id);
        }
        public Processor Create(Processor entity)
        {
            if (entity != null && entity.Name != "")
            {
                var v = _processorRepository.Read(entity.Id);
                if (v == null)
                {
                    var result = _processorRepository.Create(entity);
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
        public Processor Update(Processor entity)
        {
            if (entity != null)
            {
                var v = _processorRepository.Read(entity.Id);
                if (v != null)
                {
                    v.BaseClock = entity.BaseClock;
                    v.BoostClock = entity.BoostClock;
                    v.BrandId = entity.BrandId;
                    v.Cores = entity.Cores;
                    v.Name = entity.Name;
                    v.Price = entity.Price;
                    v.Socket = entity.Socket;
                    v.Threads = entity.Threads;
                    var result = _processorRepository.Update(v);
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
            var v = _processorRepository.Read(id);
            if (v != null)
            {
                _processorRepository.Delete(id);
            }
            else
            {
                throw new Exception("No entity found!");
            }
        }
        public IEnumerable<ProcessorWhitHighestPriceMotherboardModel> ProcessorWhitHighestPriceMotherboard()
        {
            var motherboards = _motherboardRepository.ReadAll().ToList();
            var processors = _processorRepository.ReadAll().ToList();
            var mBrands = _mBrandRepository.ReadAll().ToList();

            var mbg = from motherboard in motherboards
                      group motherboard by motherboard.Socket into g
                      select new
                      {
                          Sokcet = g.Key,
                          Motherboard = g.Select(x => x).OrderByDescending(x => x.Price).First(),
                          BrandId = g.Select(x => x).OrderByDescending(x => x.Price).First().BrandId
                      };
            var result = from proc in processors
                         join mb in mbg
                         on proc.Socket equals mb.Sokcet
                         join mBrand in mBrands
                         on mb.BrandId equals mBrand.Id
                         select new ProcessorWhitHighestPriceMotherboardModel
                         {
                             Name = proc.Name,
                             Type = mb.Motherboard.Type,
                             Chipset = mb.Motherboard.Chipset,
                             Brand = mBrand.Name,
                             Price = mb.Motherboard.Price
                         };
            return result.ToList();
        }
    }
}
