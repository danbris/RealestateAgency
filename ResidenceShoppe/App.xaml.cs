using Residence.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ResidenceShoppe
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            NavigationView app = new NavigationView();
            MainViewModel context = new MainViewModel();
            app.DataContext = context;
            app.Show();
        }
    }
}
