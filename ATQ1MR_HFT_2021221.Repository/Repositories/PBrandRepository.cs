using ATQ1MR_HFT_2021221.Data;
using ATQ1MR_HFT_2021221.Models.Entities;
using ATQ1MR_HFT_2021221.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_HFT_2021221.Repository
{
    public class PBrandRepository : RepositoryBase<PBrand, int>, IPBrandRepository
    {
        public PBrandRepository(PcPartsDbContext context) : base(context)
        {
        }

        public override PBrand Read(int id)
        {
            return ReadAll().SingleOrDefault(x => x.Id == id);
        }
    }
}
