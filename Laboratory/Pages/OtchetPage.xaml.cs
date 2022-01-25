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
using OxyPlot;

namespace Laboratory.Pages
{
    /// <summary>
    /// Логика взаимодействия для OtchetPage.xaml
    /// </summary>
    /// 
    using System.Collections.Generic;
    using OxyPlot;
    public partial class OtchetPage : Page
    {
        public OtchetPage()
        {
            InitializeComponent();
        }

        private void CboxKonrolK_Click(object sender, RoutedEventArgs e)
        {
            if (CboxKonrolK.IsChecked == true)
            {
                CboxOtchetYalygi.IsChecked = false;
                PeriodYslug.Visibility = Visibility.Hidden;
            }
            else
            {
                CboxKonrolK.IsChecked = false;
                PeriodYslug.Visibility = Visibility.Visible;
                CboxOtchetYalygi.IsChecked = true;
            }
        }

        private void CboxOtchetYalygi_Click(object sender, RoutedEventArgs e)
        {
            if (CboxOtchetYalygi.IsChecked == true)
            {
                CboxKonrolK.IsChecked = false;
                PeriodYslug.Visibility = Visibility.Visible;
            }
            else
            {
                CboxOtchetYalygi.IsChecked = false;
                PeriodYslug.Visibility = Visibility.Hidden;
                CboxKonrolK.IsChecked = true;
            }

        }

        private void CboxGrapik_Click(object sender, RoutedEventArgs e)
        {
            if (CboxGrapik.IsChecked == true)
            {
                CboxTablica.IsChecked = false;
            }
            else
            {
                CboxTablica.IsChecked = true;
            }
        }

        private void CboxTablica_Click(object sender, RoutedEventArgs e)
        {
            if (CboxTablica.IsChecked == true)
            {
                CboxGrapik.IsChecked = false;
            }
            else
            {
                CboxGrapik.IsChecked = true;
            }
        }

        private void СBoxGrapikPdf_Click(object sender, RoutedEventArgs e)
        {
            if (СboxGrapikPdf.IsChecked == true)
            {
                CboxTablicaPdf.IsChecked = false;
                CboxGraphikTablicaPdf.IsChecked = false;
            }
         
        }

        private void CboxTablicaPdf_Click(object sender, RoutedEventArgs e)
        {
            if (CboxTablicaPdf.IsChecked == true)
            {
                СboxGrapikPdf.IsChecked = false;
                CboxGraphikTablicaPdf.IsChecked = false;
            }

        }

        private void CboxGraphikTablicaPdf_Click(object sender, RoutedEventArgs e)
        {
            if (CboxGraphikTablicaPdf.IsChecked == true)
            {
                CboxTablicaPdf.IsChecked = false;
                СboxGrapikPdf.IsChecked = false;
            }
        }

        private void BtnCreateOtchet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnVigryzitOtchet_Click(object sender, RoutedEventArgs e)
        {

        }
       
    }
}
