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
using System.Windows.Threading;

namespace Laboratory.Pages
{
    /// <summary>
    /// Логика взаимодействия для LaborantVhod.xaml
    /// </summary>
    public partial class LaborantVhod : Page
    {
        DispatcherTimer DispatcherTimer;
        TimeSpan Time;
        Entities.Laborant CurrentLaborantZakaz = null;
        public LaborantVhod(Entities.Laborant CurrentLaborant)
        {
            
            InitializeComponent();
            CurrentLaborantZakaz = CurrentLaborant;
            Time = TimeSpan.FromSeconds(600);
            DispatcherTimer = new DispatcherTimer();
            DispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            DispatcherTimer.Tick += DispatcherTimer_Tick;
            DispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (Time == TimeSpan.Zero) 
            { 
                DispatcherTimer.Stop();
                MessageBox.Show("Сеанс окончен войдите в учётную запись через минуту");
                NavigationService.Navigate(new LoginPage());
            }
            else
            {
                Time = Time.Add(TimeSpan.FromSeconds(-1));
                Timer.Text = Time.ToString("c");
            }
            if (Time == TimeSpan.FromSeconds(300))
            {
                MessageBox.Show("До конца сеанса осталось 5 минут");
            }
        }

        private void BtnZakaz_Click(object sender, RoutedEventArgs e)
        {
            int Seconds =Convert.ToInt32(Time.TotalSeconds.ToString());
                NavigationService.Navigate(new Zakaz(Seconds, CurrentLaborantZakaz));
        }

        private void EditPacient_Click(object sender, RoutedEventArgs e)
        {
            int Seconds = Convert.ToInt32(Time.TotalSeconds.ToString());
            NavigationService.Navigate(new EditPacient(Seconds, CurrentLaborantZakaz));
        }
    }
}

