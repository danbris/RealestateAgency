using Residence.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ResidenceShoppe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _text;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
            }
        }

        public StudioViewModel StudioVM { get; }

        public MainWindow()
        {
            InitializeComponent();
            StudioVM = new StudioViewModel();
            DataContext = StudioVM;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Text += DateTime.Now.ToShortDateString();
            StudioVM.FloorNumber += 1;
        }
    }
}
