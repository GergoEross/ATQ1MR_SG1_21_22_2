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
    [Table("ProcessorBrands")]
    public class PBrand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Processor> Processors { get; set; }
        public override string ToString()
        {
            return $"Id: {Id}\nName: {Name}\n";
        }
    }
}
