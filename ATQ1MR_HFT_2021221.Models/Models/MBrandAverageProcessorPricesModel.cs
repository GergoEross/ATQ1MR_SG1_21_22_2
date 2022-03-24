using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_HFT_2021221.Models.Models
{
    public class MBrandAverageProcessorPricesModel
    {
        public string MBrandName { get; set; }
        public double Average { get; set; }
        public override bool Equals(object obj)
        {
            var other = obj as MBrandAverageProcessorPricesModel;
            return other.MBrandName == MBrandName && other.Average == Average;
        }
        public override string ToString()
        {
            return $"\nBrand: {MBrandName}\nAverage: {Average}";
        }
    }
}
