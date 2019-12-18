using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Residence.DataLayer
{
    public class Housing
    {
        [Key]
        public int ID { get; set; }
        public HousingType HousingType { get; set; }
        public double Surface { get; set; }
        public int NoOfRooms { get; set; }
        public int NoOfFlats { get; set; }
        public int FlatNo { get; set; }
        public int HouseNo { get; set; }
        public string Description { get; set; }
        [ForeignKey("ComodityID")]
        public virtual ICollection<Comodity> Comodities { get; set; }
    }
}
