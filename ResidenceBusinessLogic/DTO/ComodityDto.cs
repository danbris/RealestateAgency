using Residence.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidenceBusinessLogic.DTO
{
    public class ComodityDto
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Currency Currency { get; set; }

        public ComodityDto()
        {

        }
    }
}
