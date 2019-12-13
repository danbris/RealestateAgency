using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Residence.DataLayer
{
    public class Comodity
    {
        [Key]
        public int ID { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Currency Currency { get; set; }
        [ForeignKey("HousingID")]
        public virtual ICollection<Housing> Houses { get; set; }
    }
}
