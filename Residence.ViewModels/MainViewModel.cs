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
        private string _descriptionInEdit; //variable for description to handle edit/update 
        private double _surfaceInEdit; //variable for surface to handle edit/update 
        private int _noOfRooms;
        private int _noOfFlats;
        private int _flatNo;
        private int _houseNo;
        private object _selectedViewModel; //used for navigation

        #endregion

        #region Properties
        public object SelectedViewModel
        {
            get => _selectedViewModel;
            set => SetProperty(ref _selectedViewModel, value);
        }
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
            get => _noOfRooms;
            set => SetProperty(ref _noOfRooms, value);
        }
        public int NoOfFlatsInEdit
        {
            get => _noOfFlats;
            set => SetProperty(ref _noOfFlats, value);
        }
        public int FlatNoInEdit
        {
            get => _flatNo;
            set => SetProperty(ref _flatNo, value);
        }
        public int HouseNoInEdit
        {
            get => _houseNo;
            set => SetProperty(ref _houseNo, value);
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

        /*private ICommand _runAddView;
        public ICommand RunAddView => _runAddView ?? (_runAddView = new DelegateCommand(RunAddViewMethod));
        private void RunAddViewMethod()
        {
            SelectedViewModel = new AddViewModel();
        }*/

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

            if (!string.IsNullOrEmpty(DescriptionInEdit))
                newHouse.Description = DescriptionInEdit;

            if (!double.IsNaN(SurfaceInEdit))
                newHouse.Surface = SurfaceInEdit;

            if (NoOfRoomsInEdit != 0)
                newHouse.NoOfRooms = NoOfRoomsInEdit;
            
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
