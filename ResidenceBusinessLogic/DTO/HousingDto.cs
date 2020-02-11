using Residence.DataLayer;
using System.Collections.Generic;
using System.Linq;

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

        public HousingDto(Housing entity)
        {
            if (entity == null) return;

            ID = entity.ID;
            Description = entity.Description;
            FlatNo = entity.FlatNo;
            HouseNo = entity.HouseNo;
            HousingType = entity.HousingType;
            NoOfFlats = entity.NoOfFlats;
            NoOfRooms = entity.NoOfRooms;
            Surface = entity.Surface;
            Comodities = entity.Comodities.Select(y => new ComodityDto()
            {
                ID = y.ID,
                Description = y.Description,
                Currency = y.Currency,
                Price = y.Price
            }).ToList();
        }

        public void UpdateEntity(Housing entity)
        {
            entity.Description = Description;
            //...
        }

    }
}
