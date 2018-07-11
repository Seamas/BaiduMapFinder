using System;
using System.Collections.Generic;
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

namespace BaiduMapFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var control = sender as Control;
            var name = control.Name;
            switch(name)
            {
                case "option":
                    var optionWindow = new OptionWindow();
                    optionWindow.Owner = this;
                    optionWindow.ShowDialog();
                    break;
                case "about":
                    var aboutWindow = new AboutWindow();
                    aboutWindow.Owner = this;
                    aboutWindow.ShowDialog();
                    break;
                case "exit":
                    Application.Current.Shutdown();
                    break;
                default:
                    break;
            }
        }


    }
}
