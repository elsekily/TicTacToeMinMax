using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TicTacToe_Minmax
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Custom startup logic goes here

            // Call the base implementation to ensure normal startup behavior
            base.OnStartup(e);
            var checker = new Checker();
            var helper = new WindowHelper();
            var minimax = new Minimax(checker, helper);

            base.MainWindow = new MainWindow(checker, helper, minimax);
            base.MainWindow.Show();
        }
    }
}
