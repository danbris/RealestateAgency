using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResidenceBusinessLogic.DTO;

namespace ResidenceBusinessLogic
{
    public class HousingDataProvider
    {
        private Residence.DataLayer.ResidenceContext _context;
        
        public HousingDataProvider()
        {
            _context = new Residence.DataLayer.ResidenceContext();
        }

        public IList<HousingDto> GetHouses()
        {
            return _context.Houses.Select(x => new HousingDto()
            {
                ID = x.ID,
                Description = x.Description,
                FlatNo = x.FlatNo,
                HouseNo = x.HouseNo,
                HousingType = x.HousingType,
                NoOfFlats = x.NoOfFlats,
                NoOfRooms = x.NoOfRooms,
                Surface = x.Surface,
                Comodities = x.Comodities.Select(y => new ComodityDto()
                {
                    ID = y.ID,
                    Description = y.Description,
                    Currency = y.Currency,
                    Price = y.Price
                }).ToList()
            }).ToList();
        }
    }
}
