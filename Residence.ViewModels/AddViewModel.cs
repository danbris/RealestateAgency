using Prism.Commands;
using Prism.Mvvm;
using Residence.DataLayer;
using ResidenceBusinessLogic;
using ResidenceBusinessLogic.DTO;
using System.Collections.Generic;
using System.Windows.Input;

namespace Residence.ViewModels
{
    public class AddViewModel : BindableBase, IPageViewModel
    {
        #region Variables

        private string _description; //variable for description to handle edit/update 
        private double _surface; //variable for surface to handle edit/update 
        private int _noOfRooms;
        private int _noOfFlats;
        private int _flatNo;
        private int _houseNo;
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
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        public double Surface
        {
            get => _surface;
            set => SetProperty(ref _surface, value);
        }
        public int NoOfRooms
        {
            get => _noOfRooms;
            set => SetProperty(ref _noOfRooms, value);
        }

        public int NoOfFlats
        {
            get => _noOfFlats;
            set => SetProperty(ref _noOfFlats, value);
        }
        public int FlatNo
        {
            get => _flatNo;
            set => SetProperty(ref _flatNo, value);
        }
        public int HouseNo
        {
            get => _houseNo;
            set => SetProperty(ref _houseNo, value);
        }

        #endregion

        #region Commands with their private methods

        private ICommand _add;
        public ICommand Add => _add ?? (_add = new DelegateCommand(AddExecute));

        private void AddExecute()
        {
            var newHouse = new HousingDto();

            newHouse.HousingType = SelectedHousingType;

            //the VM should validate only if there is a minimum amount of information to be saved
            // or if there are any other UI constraints.
            // the business point of view validation should be in BusinessLogic


            if (!string.IsNullOrEmpty(Description))
                newHouse.Description = Description;

            if (!double.IsNaN(Surface))
                newHouse.Surface = Surface;

            if (NoOfRooms != 0)
                newHouse.NoOfRooms = NoOfRooms;

            if (NoOfFlats != 0)
                newHouse.NoOfFlats = NoOfFlats;

            if (FlatNo != 0)
                newHouse.FlatNo = FlatNo;

            if (HouseNo != 0)
                newHouse.HouseNo = HouseNo;

            _housingDataProvider.SaveHousing(newHouse);

            BackToMainPage();
        }

        private ICommand _backToMainPage;
        public ICommand Back => _backToMainPage ?? (_backToMainPage = new DelegateCommand(BackToMainPage));
        private void BackToMainPage() 
        {
            //what if I Notify("GoToEditPage")
            //I should not have an option to go somewhere else
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
