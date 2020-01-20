using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Residence.DataLayer;
using ResidenceBusinessLogic;
using ResidenceBusinessLogic.DTO;

namespace Residence.ViewModels
{
    public class MainViewModel : BindableBase
    {
        #region Variables

        private ObservableCollection<HousingViewModel> _houses;   //collection for houses
        private readonly HousingDataProvider _housingDataProvider; //instance of dataprovider
        private Dictionary<HousingType, string> _housingTypes = new Dictionary<HousingType, string>(); //instance of housingType
        private HousingViewModel _selectedItem; //selected house 
        //for edit part
        private string _descriptionInEdit; //variable for description to handle edit/update 
        private double _surfaceInEdit; //variable for surface to handle edit/update 
        private int _noOfRoomsInEdit;
        private int _noOfFlatsInEdit;
        private int _flatNoInEdit;
        private int _houseNoInEdit;
        //for add part
        private string _descriptionToAdd; //variable for description to handle edit/update 
        private double _surfaceIToAdd; //variable for surface to handle edit/update 
        private int _noOfRoomsToAdd;
        private int _noOfFlatsToAdd;
        private int _flatNoToAdd;
        private int _houseNoToAdd;
        // private object _selectedViewModel; //used for navigation

        #endregion

        #region Properties

        //NAVIGATION PROPERTY
        //public object SelectedViewModel
        //{
        //    get => _selectedViewModel;
        //    set => SetProperty(ref _selectedViewModel, value);
        //}

        //EDIT PROPERTIES
        public string DescriptionInEdit
        {
            get => _descriptionInEdit;
            set => SetProperty(ref _descriptionInEdit, value);
        }
        public double SurfaceInEdit
        {
            get => _surfaceInEdit;
            set => SetProperty(ref _surfaceInEdit, value);
        }
        public int NoOfRoomsInEdit
        {
            get => _noOfRoomsInEdit;
            set => SetProperty(ref _noOfRoomsInEdit, value);
        }

        public int NoOfFlatsInEdit
        {
            get => _noOfFlatsInEdit;
            set => SetProperty(ref _noOfFlatsInEdit, value);
        }
        public int FlatNoInEdit
        {
            get => _flatNoInEdit;
            set => SetProperty(ref _flatNoInEdit, value);
        }
        public int HouseNoInEdit
        {
            get => _houseNoInEdit;
            set => SetProperty(ref _houseNoInEdit, value);
        }

        //ADD PART PROPERTIES
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

        public HousingViewModel SelectedItem 
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public Dictionary<HousingType, string> HousingTypes
        {
            get => _housingTypes;
            set => SetProperty(ref _housingTypes, value);
        }

        public ObservableCollection<HousingViewModel> Houses
        {
            get => _houses;
            set => SetProperty(ref _houses, value);
        }

       
        #endregion

        #region Commands

        private ICommand _edit;
        public ICommand Edit => _edit ?? (_edit = new DelegateCommand(EditExecuted));

        private void EditExecuted()
        {
            DescriptionInEdit = SelectedItem.Description;
            SurfaceInEdit = SelectedItem.Surface;
            NoOfRoomsInEdit = SelectedItem.NoOfRooms;
            NoOfFlatsInEdit = SelectedItem.NoOfFlats;
            FlatNoInEdit = SelectedItem.FlatNo;
            HouseNoInEdit = SelectedItem.HouseNo;
        }

        private ICommand _update;
        public ICommand Update => _update ?? (_update = new DelegateCommand(UpdateExecuted));

        private void UpdateExecuted()
        {
            var selectedHouseDto = _housingDataProvider.GetHouse(SelectedItem.ID);

            selectedHouseDto.Description = DescriptionInEdit;
            selectedHouseDto.Surface = SurfaceInEdit;
            selectedHouseDto.NoOfRooms = NoOfRoomsInEdit;
            selectedHouseDto.NoOfFlats = NoOfFlatsInEdit;
            selectedHouseDto.FlatNo = FlatNoInEdit;
            selectedHouseDto.HouseNo = HouseNoInEdit;

            _housingDataProvider.SaveHousing(selectedHouseDto);

            LoadData();
        }

        private ICommand _delete;
        public ICommand Delete => _delete ?? (_delete = new DelegateCommand(DeleteExecute));
        private void DeleteExecute()
        {
            var selectedHouseDto = _housingDataProvider.GetHouse(SelectedItem.ID);
            _housingDataProvider.DeleteHousing(selectedHouseDto);
            LoadData();
        }

        private ICommand _view;
        public ICommand View => _view ?? (_view = new DelegateCommand(ViewExecuted));
        private void ViewExecuted()
        {
            
        }

        private ICommand _add;
        public ICommand Add => _add ?? (_add = new DelegateCommand(AddExecute));
        private void AddExecute()
        {
            var newHouse = new HousingDto();

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

            LoadData();
        }

        #endregion

        #region Constructor
        public MainViewModel()
        {
            _housingDataProvider = new HousingDataProvider();
            _housingTypes.Add(HousingType.Apartment, "Apartment");
            _housingTypes.Add(HousingType.House, "House");
            _housingTypes.Add(HousingType.Penthouse, "Penthouse");
            _housingTypes.Add(HousingType.Studio, "Studio");
            LoadData();
        }

        #endregion

        #region Private Methods
        private void LoadData()
        {
            var housesDtos = _housingDataProvider.GetHouses();
            Houses = new ObservableCollection<HousingViewModel>(housesDtos.Select(houseDto =>
                new HousingViewModel(houseDto)));
        }

        

        #endregion
    }
}
