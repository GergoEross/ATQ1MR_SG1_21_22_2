using ATQ1MR_HFT_2021221.Models.Entities;
using ATQ1MR_HFT_2021221.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_HFT_2021221.Logic.Intefaces
{
    public interface IProcessorLogic
    {
        IList<Processor> ReadAll();
        Processor Read(int id);
        Processor Create(Processor entity);
        Processor Update(Processor entity);
        void Delete(int id);
        IEnumerable<ProcessorWhitHighestPriceMotherboardModel> ProcessorWhitHighestPriceMotherboard();
    }
}
