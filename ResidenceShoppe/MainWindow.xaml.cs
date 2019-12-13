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
using Residence.DataLayer;

namespace ResidenceShoppe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public StudioViewModel StudioVM { get; } = new StudioViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = StudioVM;

            ResidenceContext context = new ResidenceContext();
            dgHousing.ItemsSource = context.Houses.ToList();
            dgComodities.ItemsSource = context.Comodities.ToList();
        }
    }
}
