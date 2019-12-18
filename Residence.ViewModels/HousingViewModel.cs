using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Residence.DataLayer;
using ResidenceBusinessLogic;
using ResidenceBusinessLogic.DTO;

namespace Residence.ViewModels
{
    public class HousingViewModel : BindableBase
    {
        private readonly HousingDto _housingDto;

        public HousingType HousingType => _housingDto.HousingType;
        public double Surface => _housingDto.Surface;
        public int NoOfRooms => _housingDto.NoOfRooms;
        public int NoOfFlats => _housingDto.NoOfFlats;
        public int FlatNo => _housingDto.FlatNo;
        public int HouseNo => _housingDto.HouseNo;
        public string Description => _housingDto.Description;

        public string ComoditiesSummary => _housingDto.Comodities == null
            ? string.Empty
            : string.Join(", ", _housingDto.Comodities.Select(x => x.Description));

        public HousingViewModel(HousingDto housing)
        {
            _housingDto = housing;
        }

        
    }
}
