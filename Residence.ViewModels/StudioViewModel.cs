using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Residence.ViewModels
{
    public class StudioViewModel: ViewModelBase
    {
        private int _number;
        private int _floorNumber;

        public int FloorNumber
        {
            get => _floorNumber;
            set => SetValue(ref _floorNumber, value, nameof(FloorNumber));
        }
    }
}
