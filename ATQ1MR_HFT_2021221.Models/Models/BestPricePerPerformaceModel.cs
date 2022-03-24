using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_HFT_2021221.Models.Models
{
    public class BestPricePerPerformaceModel
    {
        public string ProcessorName { get; set; }
        public double PPP { get; set; }
        public override bool Equals(object obj)
        {
            var other = obj as BestPricePerPerformaceModel;
            if (other == null)
            {
                return false;
            }
            else
            {
                return other.ProcessorName == ProcessorName && other.PPP == PPP;
            }
        }
        public override string ToString()
        {
            return $"Processor name: {ProcessorName}\nPrice/Performance: {PPP}\n";
        }
    }
}
