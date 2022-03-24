using ATQ1MR_HFT_2021221.Logic.Intefaces;
using ATQ1MR_HFT_2021221.Models.Entities;
using ATQ1MR_HFT_2021221.Repository.Interfaces;
using ATQ1MR_HFT_2021221.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_HFT_2021221.Logic.Services
{
    public class MBrandLogic : IMBrandLogic
    {
        IMBrandRepository _mBrandRepository;
        IMotherboardRepository _motherboardRepository;
        IProcessorRepository _processorRepository;

        public MBrandLogic(IMBrandRepository mBrandRepository, IMotherboardRepository motherboardRepository, IProcessorRepository processorRepository)
        {
            _mBrandRepository = mBrandRepository;
            _motherboardRepository = motherboardRepository;
            _processorRepository = processorRepository;
        }

        public IList<MBrand> ReadAll()
        {
            return _mBrandRepository.ReadAll().ToList();
        }
        public MBrand Read(int id)
        {
            return _mBrandRepository.Read(id);
        }
        public MBrand Create(MBrand entity)
        {
            if (entity != null && entity.Name != "")
            {
                var v = _mBrandRepository.Read(entity.Id);
                if (v == null)
                {
                    var result = _mBrandRepository.Create(entity);
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
        public MBrand Update(MBrand entity)
        {
            if (entity != null)
            {
                var v = _mBrandRepository.Read(entity.Id);
                if (v != null)
                {
                    v.Name = entity.Name;
                    var result = _mBrandRepository.Update(v);
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
            var v = _mBrandRepository.Read(id);
            if (v != null)
            {
                _mBrandRepository.Delete(id);
            }
            else
            {
                throw new Exception("No entity found!");
            }
        }
        public IEnumerable<MBrandAverageProcessorPricesModel> MBrandsWithAvarageProcessorPrices()
        {
            var mBrands = _mBrandRepository.ReadAll().ToList();
            var motherboards = _motherboardRepository.ReadAll().ToList();
            var processors = _processorRepository.ReadAll().ToList();

            
            var procg = from processor in processors
                        group processor by processor.Socket into g
                        select new
                        {
                            Socket = g.Key,
                            Prices = g.Select(x => x.Price)
                        };
            var mbavg = from mb in motherboards
                        join proc in procg
                        on mb.Socket equals proc.Socket
                        select new
                        {
                            BrandId = mb.BrandId,
                            Sum = proc.Prices.Sum(),
                            Count = proc.Prices.Count()
                        } into s
                        group s by s.BrandId into g
                        select new
                        {
                            BrandId = g.Key,
                            Average = g.Select(x=>x.Sum).Sum()/(double)g.Select(x=>x.Count).Sum()
                        };

            var result = from mBrand in mBrands
                         join mb in mbavg
                         on mBrand.Id equals mb.BrandId
                         select new MBrandAverageProcessorPricesModel
                         {
                             MBrandName = mBrand.Name,
                             Average = mb.Average
                         };

            return result.ToList();
        }
    }
}
