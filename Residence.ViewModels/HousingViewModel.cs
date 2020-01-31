using System.Linq;
using Prism.Mvvm;
using Residence.DataLayer;
using ResidenceBusinessLogic.DTO;

namespace Residence.ViewModels
{
    public class HousingViewModel : BindableBase
    {
        #region Variables

        private readonly HousingDto _housingDto;

        #endregion

        #region Properties 
        public int ID => _housingDto.ID;
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

        #endregion

        #region Constructor
        public HousingViewModel(HousingDto housing)
        {
            _housingDto = housing;
        }

        #endregion
    }
}
