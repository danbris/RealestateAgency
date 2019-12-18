using Residence.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidenceBusinessLogic.DTO
{
    public class HousingDto
    {
        public int ID { get; set; }
        public HousingType HousingType { get; set; }
        public double Surface { get; set; }
        public int NoOfRooms { get; set; }
        public int NoOfFlats { get; set; }
        public int FlatNo { get; set; }
        public int HouseNo { get; set; }
        public string Description { get; set; }
        public ICollection<ComodityDto> Comodities { get; set; }

        public HousingDto()
        {

        }
    }
}
