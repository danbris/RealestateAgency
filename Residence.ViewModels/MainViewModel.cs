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

namespace Residence.ViewModels
{
    public class MainViewModel : BindableBase
    {

        private ObservableCollection<HousingViewModel> _houses;
        private readonly HousingDataProvider _housingDataProvider;
        private Dictionary<HousingType, string> _housingTypes = new Dictionary<HousingType, string>();
        private HousingViewModel _selectedItem;

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

        private ICommand _edit;
        public ICommand Edit => _edit ?? (_edit = new DelegateCommand(EditExecuted));
        private void EditExecuted()
        {
            
        }

        private ICommand _view;
        public ICommand View => _view ?? (_view = new DelegateCommand(ViewExecuted));
        private void ViewExecuted()
        {

        }

        public MainViewModel()
        {
            _housingDataProvider = new HousingDataProvider();
            _housingTypes.Add(HousingType.Apartment, "Apartment");
            _housingTypes.Add(HousingType.House, "House");
            _housingTypes.Add(HousingType.Penthouse, "Penthouse");
            _housingTypes.Add(HousingType.Studio, "Studio");
            LoadData();
        }

        private void LoadData()
        {
            var housesDtos = _housingDataProvider.GetHouses();
            Houses = new ObservableCollection<HousingViewModel>(housesDtos.Select(houseDto =>
                new HousingViewModel(houseDto)));
        }

        /*public IEnumerable<HousingType> GetHouseTypeValues()
        {
             return Enum.GetValues(typeof(HousingType)).Cast<HousingType>();
        }*/
    }
}
