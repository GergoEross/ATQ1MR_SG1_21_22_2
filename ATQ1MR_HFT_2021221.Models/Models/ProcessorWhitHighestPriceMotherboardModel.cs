using ATQ1MR_HFT_2021221.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_HFT_2021221.Models.Models
{
    public class ProcessorWhitHighestPriceMotherboardModel
    {
        public string Type { get; set; }
        public string Chipset { get; set; }
        public int Price { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public override bool Equals(object obj)
        {
            var other = obj as ProcessorWhitHighestPriceMotherboardModel;
            return other.Brand == Brand && other.Chipset == Chipset && other.Name == Name && other.Price == Price && other.Type == Type;
        }
        public override string ToString()
        {
            return $"\nProcessor: {Name}\nMotherboard: {Brand} {Type} {Chipset}";
        }
    }
}
