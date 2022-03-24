using ATQ1MR_HFT_2021221.Logic.Intefaces;
using ATQ1MR_HFT_2021221.Models.Entities;
using ATQ1MR_HFT_2021221.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_HFT_2021221.Logic.Services
{
    public class PBrandLogic : IPBrandLogic
    {
        IMBrandRepository _mBrandRepository;
        IMotherboardRepository _motherboardRepository;
        IProcessorRepository _processorRepository;
        IPBrandRepository _pBrandRepository;

        public PBrandLogic(IMBrandRepository mBrandRepository, IMotherboardRepository motherboardRepository, IProcessorRepository processorRepository, IPBrandRepository pBrandRepository)
        {
            _mBrandRepository = mBrandRepository;
            _motherboardRepository = motherboardRepository;
            _processorRepository = processorRepository;
            _pBrandRepository = pBrandRepository;
        }
        public PBrand Create(PBrand entity)
        {
            if (entity != null && entity.Name != "")
            {
                var v = _pBrandRepository.Read(entity.Id);
                if (v == null)
                {
                    var result = _pBrandRepository.Create(entity);
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

        public void Delete(int id)
        {
            var v = _pBrandRepository.Read(id);
            if (v != null)
            {
                _pBrandRepository.Delete(id);
            }
            else
            {
                throw new Exception("No entity found!");
            }
        }

        public PBrand Read(int id)
        {
            return _pBrandRepository.Read(id);
        }

        public IList<PBrand> ReadAll()
        {
            return _pBrandRepository.ReadAll().ToList();
        }

        public PBrand Update(PBrand entity)
        {
            if (entity != null)
            {
                var v = _pBrandRepository.Read(entity.Id);
                if (v != null)
                {
                    v.Name = entity.Name;
                    var result = _pBrandRepository.Update(v);
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
    }
}
