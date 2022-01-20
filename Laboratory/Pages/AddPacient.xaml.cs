using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AddPacient.xaml
    /// </summary>
    public partial class AddPacient : Page
    {
        DispatcherTimer DispatcherTimer;
        TimeSpan Time;
        Entities.Laborant CurrentLaborantZakaz = null;
        public AddPacient(int Seconds, Entities.Laborant CurrentLaborant)
        {
            InitializeComponent();
            Time = TimeSpan.FromSeconds(Seconds);
            DispatcherTimer = new DispatcherTimer();
            DispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            DispatcherTimer.Tick += DispatcherTimer_Tick;
            DispatcherTimer.Start();
            CboxNameStraxovaiya.ItemsSource = App.Context.InsuranceCompany.ToList();

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

        

        private void BtnAddPacient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ErrorMessage = CheckErrors();
                if (ErrorMessage.Length > 0)
                {
                    MessageBox.Show(ErrorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else { 
                Entities.Pacient Pacient = new Entities.Pacient
                {
                    Name = TBoxName.Text,
                    Sername = TBoxSername.Text,
                    Patronomic = TBoxOtchestvo.Text,
                    DateOfBirthday = DateOfBirthday.SelectedDate,
                    SeriaPassport = Convert.ToInt32(TBoxsSeriaPasport.Text),
                    NumberPasport = Convert.ToInt32(TBoxNumberPasport.Text),
                    Login = null,
                    Password=null,
                    Phone=TBoxPhone.Text,
                    Pochta=TBoxPochta.Text

                };
                App.Context.Pacient.Add(Pacient);
                App.Context.SaveChanges();
                Entities.PacientInsuranse PacientInsuranse = new Entities.PacientInsuranse
                {
                    s5_InsuranceCompany=(Entities.InsuranceCompany)CboxNameStraxovaiya.SelectedItem,
                    CodPacient =Convert.ToInt32(App.Context.Pacient.Max(p=>p.Id).ToString()),
                    TypePolic=CboxTypePolic.Text,
                    NumberPolic=Convert.ToInt32(TBoxNumberPolic.Text)
                    };
                App.Context.PacientInsuranse.Add(PacientInsuranse);
                App.Context.SaveChanges();
                MessageBox.Show("Пользователь успешно добавлен");
                NavigationService.Navigate(new Pages.Zakaz(Convert.ToInt32(Time.TotalSeconds), CurrentLaborant));
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
            if (TBoxName.Text == "" || !Regex.IsMatch(TBoxName.Text, @"^[А-Яа-я]|\s+$")) ErrorBuilder.AppendLine("Имя обязательно для заполнения и содержит только буквы;");
            if (TBoxSername.Text == "" || !Regex.IsMatch(TBoxSername.Text, @"^[А-Яа-я]|\s+$")) ErrorBuilder.AppendLine("Фамилия обязательна для заполнения и содержит только буквы;");
            if (TBoxOtchestvo.Text == "" || !Regex.IsMatch(TBoxOtchestvo.Text, @"^[А-Яа-я]|\s+$")) ErrorBuilder.AppendLine("Отчество обязательно для заполнения и содержит только буквы;");
            if(DateOfBirthday.SelectedDate==null&& DateOfBirthday.SelectedDate<DateTime.Now.Date) ErrorBuilder.AppendLine("Дата обязательна для выбора и не может быть больше текущей;");
            if (TBoxPhone.Text == "" || !Regex.IsMatch(TBoxPhone.Text, @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{6,7}$")) ErrorBuilder.AppendLine("Телефон должен быть заполнен в нужном формате и не может быть пустым;");
            if (TBoxPochta.Text == "" || !Regex.IsMatch(TBoxPochta.Text, @"^[a-z0-9] [- a-z0-9._] +@([- a-z0-9]+.) +[a-z] {2,5}$")) ErrorBuilder.AppendLine("Почта должна быть заполнена в нужном формате и не может быть пустой;");
            if (TBoxsSeriaPasport.Text == "" || !Regex.IsMatch(TBoxsSeriaPasport.Text, @"^[0-9]+$")) ErrorBuilder.AppendLine("Поле паспорт серия должена быть заполнена в нужном формате и не может быть пустым;");
            if (TBoxNumberPasport.Text == "" || !Regex.IsMatch(TBoxNumberPasport.Text, @"^[0-9]+$")) ErrorBuilder.AppendLine("Поле паспорт номер должен быть заполнен в нужном формате и не может быть пустым;");
            if (TBoxNumberPolic.Text == "" || !Regex.IsMatch(TBoxNumberPolic.Text, @"^[0-9]+$")) ErrorBuilder.AppendLine("Поле номер страхового полиса должен быть заполнен в нужном формате и не может быть пустым;");
            if (CboxNameStraxovaiya.SelectedValue == null) ErrorBuilder.AppendLine("Имя страховой обязателно для выбора;");
            if (CboxTypePolic.SelectedValue==null) ErrorBuilder.AppendLine("Тип страхового полиса обязателен для выбора;");
            return ErrorBuilder.ToString();
        }
    }
}
