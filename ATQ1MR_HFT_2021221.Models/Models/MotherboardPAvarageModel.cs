using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_HFT_2021221.Models.Models
{
    public class MotherboardPAvarageModel
    {
        public string Type { get; set; }
        public string Chipset { get; set; }
        public string Brand { get; set; }
        public double Avarage { get; set; }
        public override bool Equals(object obj)
        {
            var other = obj as MotherboardPAvarageModel;
            if (other == null)
            {
                return false;
            }
            else
            {
                return other.Brand == Brand && other.Chipset == Chipset && other.Type == Type && other.Avarage == Avarage;
            }
        }
        public override string ToString()
        {
            return $"Motherboard:{Brand} {Type} {Chipset}\nAvarage: {Avarage}\n";
        }
    }
}
