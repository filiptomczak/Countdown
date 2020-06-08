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
using System.Windows.Shapes;

namespace Minutnik
{
    /// <summary>
    /// Logika interakcji dla klasy WindowSetTime.xaml
    /// </summary>
    public partial class WindowSetTime : Window
    {
        public delegate void SettedTime(int mins, int secs, bool shutdown);
        public SettedTime settedTime;
        public WindowSetTime()
        {
            InitializeComponent();
        }
               
        private void on_addMin(object sender, RoutedEventArgs e)
        {
            if (text_mins.Text != null)
            {
                try
                {
                    int min = Int32.Parse(text_mins.Text);
                    min++;
                    text_mins.Text = min.ToString();
                }
                catch (FormatException fe)
                {
                    MessageBox.Show($"To nie jest liczba {fe}");
                }
            }
        }

        private void on_addSec(object sender, RoutedEventArgs e)
        {
            if (text_secs.Text != null)
            {
                try
                {
                    int sec = Int32.Parse(text_secs.Text);
                    sec++;
                    text_secs.Text = sec.ToString();
                }
                catch (FormatException fe)
                {
                    MessageBox.Show($"To nie jest liczba {fe}");
                }
            }
        }

        private void on_subMin(object sender, RoutedEventArgs e)
        {
            if (text_mins.Text != null)
            {
                try
                {
                    int min = Int32.Parse(text_mins.Text);
                    if (min > 0)
                    {
                        min--;
                        text_mins.Text = min.ToString();
                    }
                }
                catch (FormatException fe)
                {
                    MessageBox.Show($"To nie jest liczba {fe}");
                }
            }
        }

        private void on_subSec(object sender, RoutedEventArgs e)
        {
            if (text_secs.Text != null)
            {
                try
                {
                    int sec = Int32.Parse(text_secs.Text);
                    if (sec > 0)
                    {
                        sec--;
                        text_secs.Text = sec.ToString();
                    }
                }
                catch (FormatException fe)
                {
                    MessageBox.Show($"To nie jest liczba {fe}");
                }
            }
        }

        private void button_start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool shutdownChecked = false;
                if ((bool)check_shutdown.IsChecked) shutdownChecked = true;
                int min = Int32.Parse(text_mins.Text);
                int sec = Int32.Parse(text_secs.Text);
                if (settedTime != null) settedTime(min, sec, shutdownChecked);
                this.Close();
            }
            catch(FormatException fe)
            {
                MessageBox.Show($"To nie jest liczba {fe}");
            }

        }

        private void text_secs_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Int32.Parse(text_secs.Text);
                if (button_addSec != null && button_subSec != null && button_start != null)
                {
                    button_addSec.IsEnabled = true;
                    button_subSec.IsEnabled = true;
                    if (button_addMin.IsEnabled == true)
                        button_start.IsEnabled = true;
                }
            }
            catch (FormatException fe)
            {
                button_addSec.IsEnabled = false;
                button_subSec.IsEnabled = false;
                button_start.IsEnabled = false;
            }
        }
        private void text_mins_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Int32.Parse(text_mins.Text);
                if (button_addMin != null && button_subMin != null && button_start != null)
                {
                    button_addMin.IsEnabled = true;
                    button_subMin.IsEnabled = true;
                    if(button_addSec.IsEnabled==true)
                        button_start.IsEnabled = true;
                }
            }
            catch (FormatException fe)
            {
                button_addMin.IsEnabled = false;
                button_subMin.IsEnabled = false;
                button_start.IsEnabled = false;
            }
        }
    }
}
