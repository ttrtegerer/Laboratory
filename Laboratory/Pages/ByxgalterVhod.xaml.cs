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

namespace Laboratory.Pages
{
    /// <summary>
    /// Логика взаимодействия для ByxgalterVhod.xaml
    /// </summary>
    public partial class ByxgalterVhod : Page
    {
        Entities.Byxgalter Byxgalter;
        public ByxgalterVhod(Entities.Byxgalter CurrentByxgalter)
        {
            InitializeComponent();
            Byxgalter = CurrentByxgalter;
            CboxStraxovaya.ItemsSource = App.Context.InsuranceCompany.ToList();
        }

        private void BtnAddOtchet_Click(object sender, RoutedEventArgs e)
        {
            try { 
            var ErrorMessedge = CheckErrors();
            if (ErrorMessedge.Length>0)
            {
                MessageBox.Show(ErrorMessedge, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                    ConvertToPDF();
            }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
        private string CheckErrors()
        {
            var ErrorBuilder = new StringBuilder();
            if (CboxStraxovaya.SelectedItem == null) ErrorBuilder.Append("Выберите сраховую");
            if (DateStart.SelectedDate == null) ErrorBuilder.Append("Выберите стартовый день отчёта");
            if (DateEnd.SelectedDate == null) ErrorBuilder.Append("Выберите финальный день отчёта");
            if (DateEnd.SelectedDate < DateStart.SelectedDate) ErrorBuilder.Append("");
            return ErrorBuilder.ToString();
        }
        private void ConvertToPDF()
        {
            var Straxovaya = (Entities.InsuranceCompany)CboxStraxovaya.SelectedItem;
            var document = new iTextSharp.text.Document();
            using (var writer = PdfWriter.GetInstance(document, new FileStream("Otchet v straxovyu" +Straxovaya.Name+ ".pdf", FileMode.Create)))
            {
                document.Open();
                BaseFont baseFont = BaseFont.CreateFont("C:/Windows/Fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

                writer.DirectContent.BeginText();
                writer.DirectContent.SetFontAndSize(baseFont, 12f);
                writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_LEFT, "Отчёт в страховую компанию : " +Straxovaya.Name ,15, 766, 0);
                writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_LEFT, "За период с : " +DateStart.SelectedDate+" по : "+DateEnd.SelectedDate, 55, 736, 0);
                writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_LEFT, "Период оплаты с : " + DateEnd.SelectedDate.Value.AddDays(1) + " по : " + DateEnd.SelectedDate.Value.AddDays(31), 55, 706, 0);
                 writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_LEFT, "Стоимость всех услуг: "+App.Context.Zakaz.Where(p=>p.Date>=DateStart.SelectedDate&& p.Date<=DateEnd.SelectedDate).Sum(p=>p.TotalSum), 55, 676, 0);
                 var ZakazYslugs = App.Context.ZakazYsluga.Where(p => p.s5_Zakaz.Date >= DateStart.SelectedDate && p.s5_Zakaz.Date <= DateEnd.SelectedDate).ToList();
                 int Ylengeht = 646;
                 for (int i = 0; i < ZakazYslugs.Count; i++)
                 {
                     writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_LEFT, "Клиент: " + ZakazYslugs[i].s5_Zakaz.s5_Pacient.Fio + " Услуга: " + ZakazYslugs[i].s5_YslugiLaboratorii.Name+" Стоимость услуги: "+ZakazYslugs[i].s5_YslugiLaboratorii.Prise, 55, Ylengeht, 0);
                     Ylengeht -= 30;
                     if (Ylengeht == 30) break;
                 }

                writer.DirectContent.EndText();

                document.Close();
                writer.Close();
                MessageBox.Show("Pdf-документ c именем: " + "OtchetVStraxovyu" + Straxovaya.Name + ".pdf" + "сохранен");

                NavigationService.Navigate(new Pages.ByxgalterVhod(Byxgalter));
            }
        }
    }
}
