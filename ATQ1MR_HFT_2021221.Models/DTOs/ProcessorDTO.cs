using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_HFT_2021221.Models.DTOs
{
    public class ProcessorDTO
    {
        public int Id { get; set; }
        public string Socket { get; set; }
        public string Name { get; set; }
        public double BaseClock { get; set; }
        public double BoostClock { get; set; }
        public int Cores { get; set; }
        public int Threads { get; set; }
        public int Price { get; set; }
        public bool IsOvercolckable { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int BrandId { get; set; }
    }
}
