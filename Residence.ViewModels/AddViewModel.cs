using Prism.Commands;
using Prism.Mvvm;
using Residence.DataLayer;
using ResidenceBusinessLogic;
using ResidenceBusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Residence.ViewModels
{
    public class AddViewModel : BindableBase, IPageViewModel
    {
        #region Variables

        private string _descriptionToAdd; //variable for description to handle edit/update 
        private double _surfaceIToAdd; //variable for surface to handle edit/update 
        private int _noOfRoomsToAdd;
        private int _noOfFlatsToAdd;
        private int _flatNoToAdd;
        private int _houseNoToAdd;
        private readonly HousingDataProvider _housingDataProvider; //instance of dataprovider
        private Dictionary<HousingType, string> _housingTypes = new Dictionary<HousingType, string>(); //instance of housingType
        private HousingType _selectedHouseType;

        #endregion

        #region Properties
        public Dictionary<HousingType, string> HousingTypes
        {
            get => _housingTypes;
            set => SetProperty(ref _housingTypes, value);
        }

        public HousingType SelectedHousingType
        {
            get => _selectedHouseType;
            set => SetProperty(ref _selectedHouseType, value);
        }
        public string DescriptionToAdd
        {
            get => _descriptionToAdd;
            set => SetProperty(ref _descriptionToAdd, value);
        }
        public double SurfaceToAdd
        {
            get => _surfaceIToAdd;
            set => SetProperty(ref _surfaceIToAdd, value);
        }
        public int NoOfRoomsToAdd
        {
            get => _noOfRoomsToAdd;
            set => SetProperty(ref _noOfRoomsToAdd, value);
        }

        public int NoOfFlatsToAdd
        {
            get => _noOfFlatsToAdd;
            set => SetProperty(ref _noOfFlatsToAdd, value);
        }
        public int FlatNoToAdd
        {
            get => _flatNoToAdd;
            set => SetProperty(ref _flatNoToAdd, value);
        }
        public int HouseNoToAdd
        {
            get => _houseNoToAdd;
            set => SetProperty(ref _houseNoToAdd, value);
        }

        #endregion

        #region Commands with their private methods

        private ICommand _add;
        public ICommand Add => _add ?? (_add = new DelegateCommand(AddExecute));

        private void AddExecute()
        {
            var newHouse = new HousingDto();

            newHouse.HousingType = SelectedHousingType;

            if (!string.IsNullOrEmpty(DescriptionToAdd))
                newHouse.Description = DescriptionToAdd;

            if (!double.IsNaN(SurfaceToAdd))
                newHouse.Surface = SurfaceToAdd;

            if (NoOfRoomsToAdd != 0)
                newHouse.NoOfRooms = NoOfRoomsToAdd;

            if (NoOfFlatsToAdd != 0)
                newHouse.NoOfFlats = NoOfFlatsToAdd;

            if (FlatNoToAdd != 0)
                newHouse.FlatNo = FlatNoToAdd;

            if (HouseNoToAdd != 0)
                newHouse.HouseNo = HouseNoToAdd;

            _housingDataProvider.SaveHousing(newHouse);

            BackToMainPage();
        }

        private ICommand _backToMainPage;
        public ICommand Back => _backToMainPage ?? (_backToMainPage = new DelegateCommand(BackToMainPage));
        private void BackToMainPage() 
        {
            Mediator.Notify("GoToMainPage");
        }

        #endregion

        #region Constructor

        public AddViewModel() 
        {
            _housingDataProvider = new HousingDataProvider();

            _housingTypes.Add(HousingType.Apartment, "Apartment");
            _housingTypes.Add(HousingType.House, "House");
            _housingTypes.Add(HousingType.Penthouse, "Penthouse");
            _housingTypes.Add(HousingType.Studio, "Studio");
        }

        #endregion
    }
}
