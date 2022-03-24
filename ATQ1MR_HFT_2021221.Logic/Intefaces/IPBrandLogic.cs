using ATQ1MR_HFT_2021221.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_HFT_2021221.Logic.Intefaces
{
    public interface IPBrandLogic
    {
        IList<PBrand> ReadAll();
        PBrand Read(int id);
        PBrand Create(PBrand entity);
        PBrand Update(PBrand entity);
        void Delete(int id);
    }
}
