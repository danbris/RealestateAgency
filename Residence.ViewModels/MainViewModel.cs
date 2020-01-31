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
    public class MainViewModel : BindableBase, IPageViewModel
    {
        #region Variables

        private ObservableCollection<HousingViewModel> _houses;   //collection for houses
        private readonly HousingDataProvider _housingDataProvider; //instance of dataprovider
        private HousingViewModel _selectedItem; //selected house 
        //navigation vars
        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        #endregion

        #region Properties
        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get => _currentPageViewModel;
            set => SetProperty(ref _currentPageViewModel, value);
        }

        public HousingViewModel SelectedItem 
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public ObservableCollection<HousingViewModel> Houses
        {
            get => _houses;
            set => SetProperty(ref _houses, value);
        }

        #endregion

        #region Commands and their private methods

        private ICommand _edit;
        
        public ICommand Edit => _edit ?? (_edit = new DelegateCommand(EditExecuted));

        private void EditExecuted()
        {
            Mediator.Notify("GoToEditPage");
        }
        private void GoToEditPage(Object obj)
        {
            PageViewModels.Add(new EditViewModel(SelectedItem));
            ChangeViewModel(PageViewModels[1]);
        }

        private ICommand _delete;
        public ICommand Delete => _delete ?? (_delete = new DelegateCommand(DeleteExecute));
        private void DeleteExecute()
        {
            var selectedHouseDto = _housingDataProvider.GetHouse(SelectedItem.ID);
            _housingDataProvider.DeleteHousing(selectedHouseDto);
            LoadData();
        }

        private ICommand _goToAdd;

        public ICommand Add => _goToAdd ?? (_goToAdd = new DelegateCommand(AddExecuted));

        private void AddExecuted()
        {
            Mediator.Notify("GoToAddPage");
        }

        private void GoToAddPage(Object obj)
        {
            PageViewModels.Add(new AddViewModel());
            ChangeViewModel(PageViewModels[1]);
        }

        private void GoToMainPage(Object obj)
        {
            ChangeViewModel(PageViewModels[0]);
            PageViewModels.Remove(PageViewModels[1]);
            LoadData();
        }

        #endregion

        #region Constructor
        public MainViewModel()
        {
            PageViewModels.Add(this);

            CurrentPageViewModel = PageViewModels[0];

            Mediator.Subscribe("GoToEditPage", GoToEditPage);
            Mediator.Subscribe("GoToAddPage", GoToAddPage);
            Mediator.Subscribe("GoToMainPage", GoToMainPage);

            _housingDataProvider = new HousingDataProvider();
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

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        #endregion
    }
}
