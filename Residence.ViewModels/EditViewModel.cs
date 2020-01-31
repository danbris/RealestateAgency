using Prism.Commands;
using Prism.Mvvm;
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
    public class EditViewModel : BindableBase, IPageViewModel
    {
        #region Variables
        private int _id;
        private string _descriptionInEdit; //variable for description to handle edit/update 
        private double _surfaceInEdit; //variable for surface to handle edit/update 
        private int _noOfRoomsInEdit;
        private int _noOfFlatsInEdit;
        private int _flatNoInEdit;
        private int _houseNoInEdit;
        private readonly HousingDataProvider _housingDataProvider; //instance of dataprovider

        #endregion

        #region Properties
        public int ID
        {
            get => _id;
            set => SetProperty(ref _id, value);
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

        #endregion

        #region Commands and their private methods

        private ICommand _update;
        public ICommand Update => _update ?? (_update = new DelegateCommand(UpdateExecuted));

        private void UpdateExecuted()
        {
            var selectedHouseDto = _housingDataProvider.GetHouse(ID);

            selectedHouseDto.Description = DescriptionInEdit;
            selectedHouseDto.Surface = SurfaceInEdit;
            selectedHouseDto.NoOfRooms = NoOfRoomsInEdit;
            selectedHouseDto.NoOfFlats = NoOfFlatsInEdit;
            selectedHouseDto.FlatNo = FlatNoInEdit;
            selectedHouseDto.HouseNo = HouseNoInEdit;

            _housingDataProvider.SaveHousing(selectedHouseDto);
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
            DescriptionInEdit = selectedItem.Description;
            SurfaceInEdit = selectedItem.Surface;
            NoOfRoomsInEdit = selectedItem.NoOfRooms;
            NoOfFlatsInEdit = selectedItem.NoOfFlats;
            FlatNoInEdit = selectedItem.FlatNo;
            HouseNoInEdit = selectedItem.HouseNo;

            _housingDataProvider = new HousingDataProvider();
        }

        #endregion

    }
}
