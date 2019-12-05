using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;

namespace Residence.ViewModels
{
    public class StudioViewModel: ViewModelBase
    {
        private int _number;
        private int _floorNumber;
        private ICommand _increaseFloorNumberCommand;

        public int FloorNumber
        {
            get => _floorNumber;
            set => SetValue(ref _floorNumber, value, nameof(FloorNumber));
        }

        public ICommand IncreaseFloorNumberCommand =>
            _increaseFloorNumberCommand ?? (_increaseFloorNumberCommand = new DelegateCommand(IncreseFloorNumber));

        private void IncreseFloorNumber()
        {
            FloorNumber++;
        }
    }
}
