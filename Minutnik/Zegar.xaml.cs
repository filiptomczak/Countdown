using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;
using System.Media;

namespace Minutnik
{
    /// <summary>
    /// Logika interakcji dla klasy Zegar.xaml
    /// </summary>
    public partial class Zegar : UserControl
    {
        bool start = true;
        bool stop = false;
        bool shutdown;
        double deg, val, totalSecs;
        static int mins, secs;
        DispatcherTimer timer;
        double timerInterval;
        double skala,licznik;
        public Zegar()
        {
            InitializeComponent();
            RysujPodzialke();
            start = true;
            timerInterval = 500.0;
            skala = timerInterval / 1000.0;
        }
        public void Aktualizuj(int m, int s)
        {
            if (start)
            {
                totalSecs = (m * 60.0 + s)*1000.0/timerInterval; //suma sekund
                Console.WriteLine("Total secs: "+totalSecs);
                if (totalSecs != 0) //gdy m i s!=0
                    deg = 360.0 / totalSecs; //przeskalowanie sumy sekund na ruch wskazowki 
                else
                    stop = true;
                text_timer.Text = deg.ToString();
                val = 0;
                start = false;              
            }
            if (!stop)
            {
                //aktualizacja wskazowki
                Console.WriteLine(val);
                Console.WriteLine(deg);
                val = val + deg;
                wskazowka.X2 = 150 - 150 * Math.Cos(Deg2Rad(90 - val));
                wskazowka.Y2 = 150 - 150 * Math.Sin(Deg2Rad(90 - val));

                wskazowka.X1 = 150 + 20 * Math.Cos(Deg2Rad(90 - val));
                wskazowka.Y1 = 150 + 20 * Math.Sin(Deg2Rad(90 - val));


                if(secs<0 && mins > 0)
                {
                    secs = 59;
                    mins--;
                }

                text_timer.Text = mins + ":" + secs;
                //sprawdzenie konca czasu
                totalSecs--;
                if (totalSecs==0) Stop();
            }
        }
        public void Stop()
        {
            if (timer != null) timer.Stop();
            if (shutdown)
            {
                Process.Start("shutdown", "/s /t 0");
                return;
            }
            start = true;
            stop = true;

            if (timer != null)
            {
                /*SoundPlayer player = new System.Media.SoundPlayer();
                player.SoundLocation = @"Minutnik\\Minutnik\\dzwonek.wav";
                player.Play();*/

                MessageBoxResult result = MessageBox.Show("KONIEC ODLICZANIA", "Minutnik", MessageBoxButton.OK);
                //if (result.ToString() == "OK") player.Stop();
            }
            text_timer.Text = "mm:ss";
            
        }
        public void Restart()
        {
            if (timer != null) timer.Stop();
            start = true;
            stop = true;
        }
        private void RysujPodzialke()
        {           
            for (int i = 0; i < 360; i++)
            {           
               
                if (i % 30 == 0)
                {
                    Line L = new Line();
                    L.Stroke = Brushes.Black;
                    L.StrokeThickness = 4;

                    L.X2 = 150 - 150 * Math.Cos(Deg2Rad(i));
                    L.Y2 = 150 - 150 * Math.Sin(Deg2Rad(i));

                    L.X1 = 150 - 130 * Math.Cos(Deg2Rad(i));
                    L.Y1 = 150 - 130 * Math.Sin(Deg2Rad(i));

                    MyLayout.Children.Add(L);
                }
                else if (i % 6 == 0)
                {
                    Line l = new Line();
                    l.Stroke = Brushes.Black;
                    l.StrokeThickness = 2;

                    l.X2 = 150 - 150 * Math.Cos(Deg2Rad(i));
                    l.Y2 = 150 - 150 * Math.Sin(Deg2Rad(i));

                    l.X1 = 150 - 140 * Math.Cos(Deg2Rad(i));
                    l.Y1 = 150 - 140 * Math.Sin(Deg2Rad(i));

                    MyLayout.Children.Add(l);
                }
            }
        }
        private static double Deg2Rad(double deg)
        {
            return Math.PI / 180 * deg;
        }
        private void SetTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(timerInterval);
            timer.Start();
            timer.Tick += delegate { onTimer(); };
            stop = false;
        }
        private void onTimer()
        {
            licznik += skala;
            Console.WriteLine("licznik: "+licznik);
            if (licznik == 1)
            {
                secs--;
                Console.WriteLine("secs: "+secs);
                licznik = 0;
            }
            Aktualizuj(mins, secs);

            text_timer_Copy.Text = DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
        }       
        public void readSettedTime(int Mins, int Secs, bool Shutdown)
        {
            mins = Mins;
            secs = Secs;
            shutdown = Shutdown;
            text_timer.Text = (mins + ":" + secs);
            SetTimer();
        }
    }
}
