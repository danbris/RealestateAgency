using Prism.Commands;
using Prism.Mvvm;
using ResidenceBusinessLogic;
using System.Windows.Input;

namespace Residence.ViewModels
{
    public class EditViewModel : BindableBase, IPageViewModel
    {
        #region Variables
        private int _id;
        private string _description; //variable for description to handle edit/update 
        private double _surface; //variable for surface to handle edit/update 
        private int _noOfRooms;
        private int _noOfFlats;
        private int _flatNo;
        private int _houseNo;
        private readonly HousingDataProvider _housingDataProvider; //instance of dataprovider

        #endregion

        #region Properties
        public int ID
        {
            get => _id;
            set => SetProperty(ref _id, value);
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

        #region Commands and their private methods

        private ICommand _update;
        public ICommand Update => _update ?? (_update = new DelegateCommand(UpdateExecute));

        private void UpdateExecute()
        {
            var selectedHouseDto = _housingDataProvider.GetHouse(ID);

            selectedHouseDto.Description = Description;
            selectedHouseDto.Surface = Surface;
            selectedHouseDto.NoOfRooms = NoOfRooms;
            selectedHouseDto.NoOfFlats = NoOfFlats;
            selectedHouseDto.FlatNo = FlatNo;
            selectedHouseDto.HouseNo = HouseNo;

            if(!_housingDataProvider.SaveHousing(selectedHouseDto, out string validationMessage))
            {
                //show validationMessage to user and wait for fixes
                return;
            }

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
        public EditViewModel(HousingViewModel selectedItem) 
        {
            ID = selectedItem.ID;
            Description = selectedItem.Description;
            Surface = selectedItem.Surface;
            NoOfRooms = selectedItem.NoOfRooms;
            NoOfFlats = selectedItem.NoOfFlats;
            FlatNo = selectedItem.FlatNo;
            HouseNo = selectedItem.HouseNo;

            _housingDataProvider = new HousingDataProvider();
        }

        #endregion

    }
}
