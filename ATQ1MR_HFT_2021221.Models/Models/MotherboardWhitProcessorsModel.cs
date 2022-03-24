using ATQ1MR_HFT_2021221.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_HFT_2021221.Models.Models
{
    public class MotherboardWhitProcessorsModel
    {
        public string Type { get; set; }
        public string Chipset { get; set; }
        public string Brand { get; set; }
        public List<Processor> Processors { get; set; }
        public override bool Equals(object obj)
        {
            var other = obj as MotherboardWhitProcessorsModel;
            if (other == null)
            {
                return false;
            }
            else
            {
                return other.Brand == Brand && other.Chipset == Chipset && other.Type == Type && other.Processors.SequenceEqual(Processors);
            }
        }
        public override string ToString()
        {
            string result = "";
            foreach (var processor in Processors)
            {
                result += "\n" + processor.ToString();
            }
            return $"Motherboard:{Brand} {Type} {Chipset}\nProcessors:{result}";
        }
    }
}
