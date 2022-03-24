using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ATQ1MR_HFT_2021221.Models.Entities
{
    [Table("Processors")]
    public class Processor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string Socket { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public double BaseClock { get; set; }
        public double BoostClock { get; set; }
        public int Cores { get; set; }
        public int Threads { get; set; }
        public int Price { get; set; }
        public int BrandId { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual PBrand Brand { get; set; }
        public override bool Equals(object obj)
        {
            var other = obj as Processor;
            if (other == null)
            {
                return false;
            }
            else
            {
                return other.Id == Id && other.Socket == Socket && other.Name == Name && other.BaseClock == BaseClock && other.BoostClock == BoostClock 
                    && other.Cores == Cores && other.Threads == Threads && other.Price == Price && other.BrandId == BrandId;
            }
        }
        public override string ToString()
        {
            return $"Id: {Id}\nName: {Name}\nSocket: {Socket}\nBaseClock: {BaseClock}\nBoostClock: {BoostClock}\nCores: {Cores}\nThreads: {Threads}\nPrice: {Price}\nBrandId: {BrandId}";
        }
    }
}
