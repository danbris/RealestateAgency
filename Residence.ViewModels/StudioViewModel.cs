using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace Residence.ViewModels
{
    public class StudioViewModel: BindableBase
    {
        private int _number;
        private int _floorNumber;
        private ICommand _increaseFloorNumberCommand;

        public int FloorNumber
        {
            get => _floorNumber;
            set => SetProperty(ref _floorNumber, value);
        }

        public ICommand IncreaseFloorNumberCommand =>
            _increaseFloorNumberCommand ?? (_increaseFloorNumberCommand = new DelegateCommand(IncreseFloorNumber));

        private void IncreseFloorNumber()
        {
            FloorNumber++;
        }
    }
}
