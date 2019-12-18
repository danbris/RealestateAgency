using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using ResidenceBusinessLogic;

namespace Residence.ViewModels
{
    public class MainViewModel : BindableBase
    {

        private ObservableCollection<HousingViewModel> _houses;
        private readonly HousingDataProvider _housingDataProvider;


        public ObservableCollection<HousingViewModel> Houses
        {
            get => _houses;
            set => SetProperty(ref _houses, value);
        }

        public MainViewModel()
        {
            _housingDataProvider = new HousingDataProvider();
            LoadData();
        }

        private void LoadData()
        {
            var housesDtos = _housingDataProvider.GetHouses();
            Houses = new ObservableCollection<HousingViewModel>(housesDtos.Select(houseDto =>
                new HousingViewModel(houseDto)));
        }
    }
}
