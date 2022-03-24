using ATQ1MR_HFT_2021221.Models.Entities;
using ATQ1MR_HFT_2021221.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_HFT_2021221.Logic.Intefaces
{
    public interface IMBrandLogic
    {
        IList<MBrand> ReadAll();
        MBrand Read(int id);
        MBrand Create(MBrand entity);
        MBrand Update(MBrand entity);
        void Delete(int id);
        IEnumerable<MBrandAverageProcessorPricesModel> MBrandsWithAvarageProcessorPrices();
    }
}
