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
    [Table("Motherboards")]
    public class Motherboard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Price { get; set; }
        [Required]
        [MaxLength(25)]
        public string Type { get; set; }
        [Required]
        [MaxLength(15)]
        public string Chipset { get; set; }
        [Required]
        [MaxLength(15)]
        public string Socket { get; set; }
        public int BrandId { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual MBrand Brand { get; set; }
    }
}
