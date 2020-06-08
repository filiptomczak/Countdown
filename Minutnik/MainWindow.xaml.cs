using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Minutnik
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

        }

        private void button_set_Click(object sender, RoutedEventArgs e)
        {
            zegar.Restart();
            WindowSetTime wst = new WindowSetTime();
            wst.settedTime = new WindowSetTime.SettedTime(zegar.readSettedTime);
            wst.Show();
        }
        private void button_mouseEnter(object sender, MouseEventArgs e)
        {
        }

    }
}
