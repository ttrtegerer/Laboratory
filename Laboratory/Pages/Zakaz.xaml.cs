using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для Zakaz.xaml
    /// </summary>
    public partial class Zakaz : Page
    {
        DispatcherTimer DispatcherTimer;
        TimeSpan Time;
        Entities.Laborant CurrentLaborant;
        public Zakaz(int Seconds,Entities.Laborant Laborant)
        {
            InitializeComponent();
            CurrentLaborant = Laborant;
            Time = TimeSpan.FromSeconds(Seconds);
            DispatcherTimer = new DispatcherTimer();
            DispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            DispatcherTimer.Tick += DispatcherTimer_Tick;
            DispatcherTimer.Start();
            CBoxKlient.ItemsSource = App.Context.Pacient.ToList();
            CBoxYslyga.ItemsSource = App.Context.YslugiLaboratorii.ToList();
            CBoxSecondYslyga.ItemsSource = App.Context.YslugiLaboratorii.ToList();
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
        private void OnComboboxTextChanged(object sender, RoutedEventArgs e)
        {
           /* var tb = (TextBox)e.OriginalSource;
            if (tb.SelectionStart != 0)
            {
                CBoxKlient.SelectedItem = null; // Если набирается текст сбросить выбраный элемент
            }
            if (tb.SelectionStart == 0 && CBoxKlient.SelectedItem == null)
            {
                CBoxKlient.IsDropDownOpen = false; // Если сбросили текст и элемент не выбран, сбросить фокус выпадающего списка
            }
            CBoxKlient.IsDropDownOpen = true;
            if (CBoxKlient.SelectedItem == null)
            {
                // Если элемент не выбран менять фильтр
                CollectionView Cv = (CollectionView)CollectionViewSource.GetDefaultView(CBoxKlient.ItemsSource.ToString());
                Cv.Filter = s => ((string)s).IndexOf(CBoxKlient.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
            }
            */
        }


        private void AddYsluga_Click(object sender, RoutedEventArgs e)
        {
            TBlockSecondYsluga.Visibility = Visibility.Visible;
            CBoxSecondYslyga.Visibility = Visibility.Visible;
        }

        private void AddZakaz_Click(object sender, RoutedEventArgs e)
        {
            try
            { var ErrorMessage = ChekErrors().ToString();
                if (ErrorMessage.Length > 0)
                {
                    MessageBox.Show(ErrorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var FirstYslyga = (Entities.YslugiLaboratorii)CBoxYslyga.SelectedItem;
                    var SecondYslyga = (Entities.YslugiLaboratorii)CBoxSecondYslyga.SelectedItem;
                    if(CBoxSecondYslyga.SelectedItem == null)
                    {
                        Entities.Zakaz Zakaz = new Entities.Zakaz
                        {
                            s5_Pacient = (Entities.Pacient)CBoxKlient.SelectedItem,
                            Date = DateTime.Now.Date,
                            TotalSum =FirstYslyga.Prise,
                            StatysZakaz = "Принят",
                        };
                             App.Context.Zakaz.Add(Zakaz);
                        App.Context.SaveChanges();
                    }
                    else { 
                    Entities.Zakaz Zakaz = new Entities.Zakaz
                        {       
                        s5_Pacient = (Entities.Pacient)CBoxKlient.SelectedItem,
                        Date = DateTime.Now.Date,
                        TotalSum= FirstYslyga.Prise+SecondYslyga.Prise,
                        StatysZakaz="Принят",
                        };
                        App.Context.Zakaz.Add(Zakaz);
                        App.Context.SaveChanges();
                    }
               
                Entities.ZakazYsluga ZakazYsluga = new Entities.ZakazYsluga
                {
                    CodLaborant=CurrentLaborant.Id,
                    s5_YslugiLaboratorii=(Entities.YslugiLaboratorii)CBoxYslyga.SelectedItem,
                    CodZakaz=Convert.ToInt32(TBoxKod.Text),
                    StatysYslugi= "Принята"

                };
                App.Context.ZakazYsluga.Add(ZakazYsluga);
                App.Context.SaveChanges();
                if (CBoxSecondYslyga.SelectedItem != null)
                {
                    Entities.ZakazYsluga ZakazYslugaSecond = new Entities.ZakazYsluga
                    {
                        CodLaborant = CurrentLaborant.Id,
                        s5_YslugiLaboratorii = (Entities.YslugiLaboratorii)CBoxSecondYslyga.SelectedItem,
                        CodZakaz = Convert.ToInt32(TBoxKod.Text),
                        StatysYslugi = "Принята"

                    };
                    App.Context.ZakazYsluga.Add(ZakazYslugaSecond);
                    App.Context.SaveChanges();
                }
                    Entities.Stcet Stcet = new Entities.Stcet
                    {
                        CodByxgalter = 1,
                        Cod_Zakaz=Convert.ToInt32(TBoxKod.Text)
                       
                    
                    };
                MessageBox.Show("Заказ добавлен");
                ConvertToPDF();

                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
        private string ChekErrors()
        {
            var ErrorBuilder = new StringBuilder();
            if (TBoxKod.Text=="") ErrorBuilder.AppendLine("Считайт код через штрих код или выберите что бы он заполнился автоматически");
            if (CBoxKlient.SelectedItem == null) ErrorBuilder.AppendLine("Выбирите пациента прежде чем оформить заказ");
            if (CBoxYslyga.SelectedItem == null) ErrorBuilder.AppendLine("Выбирите услугу прежде чем оформить заказ");
            return ErrorBuilder.ToString();
            
        }

        private void AddPacient_Click(object sender, RoutedEventArgs e)
        {
            int Seconds = Convert.ToInt32(Time.TotalSeconds.ToString());
            NavigationService.Navigate(new Pages.AddPacient(Seconds, CurrentLaborant));
        }

        private void ChBoxKod_Click(object sender, RoutedEventArgs e)
        {
            if (ChBoxKod.IsChecked == true)
            {
                var Kod = App.Context.Zakaz.Max(p=>p.Id);
                Kod++;
                TBoxKod.Text = Kod.ToString();
                TBoxKod.IsEnabled = false;
            }
            else {
                TBoxKod.Text = "";
            }
        }
        private void ConvertToPDF()
        {
            var document = new iTextSharp.text.Document();
            using (var writer = PdfWriter.GetInstance(document, new FileStream("Zakaz"+TBoxKod.Text+".pdf", FileMode.Create)))
            {
                document.Open();
                var Klient = (Entities.Pacient)CBoxKlient.SelectedItem;
                var FirstYslyga = (Entities.YslugiLaboratorii)CBoxYslyga.SelectedItem;
                var SecondYslyga = (Entities.YslugiLaboratorii)CBoxSecondYslyga.SelectedItem;
                BaseFont baseFont = BaseFont.CreateFont("C:/Windows/Fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

                writer.DirectContent.BeginText();
                writer.DirectContent.SetFontAndSize(baseFont, 12f);
                writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_CENTER, "Номер заказа: "+TBoxKod.Text, 55, 766, 0);
                writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_LEFT, "Фио клиента: " +Klient.Fio, 55, 736, 0);
                writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_LEFT, "Услуга: " + FirstYslyga.Name, 55, 706, 0);
                writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_LEFT, "Стоимость услуги: " + FirstYslyga.Prise, 55, 676, 0);
                if (SecondYslyga != null)
                {
                    writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_LEFT, "Следующая услуга: " + SecondYslyga.Name, 55, 646, 0);
                    writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_LEFT, "Стоимость второй услуги: " + SecondYslyga.Prise, 55, 616, 0);
                    writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_LEFT, "Итоговая стоимость заказа: " + Convert.ToString(FirstYslyga.Prise+SecondYslyga.Prise), 55, 586, 0);
                }
                else
                {
                    writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_LEFT, "Итоговая стоимость заказа: " + Convert.ToString(FirstYslyga.Prise), 55, 586, 0);
                }
                writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_LEFT, "Статус заказа: Принят", 55, 556, 0);
                writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_LEFT, "Дата заказа: "+Convert.ToString(DateTime.Now.Date), 55, 536, 0);

                writer.DirectContent.EndText();

                document.Close();
                writer.Close();
                MessageBox.Show("Pdf-документ c именем: "+ "Zakaz" + TBoxKod.Text + ".pdf" + "сохранен");

                NavigationService.Navigate(new Pages.Zakaz(Convert.ToInt32(Time.TotalSeconds), CurrentLaborant));
            }
        }
        private void ShtrixKod_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
