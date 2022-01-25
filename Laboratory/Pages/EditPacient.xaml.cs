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
    /// Логика взаимодействия для EditPacient.xaml
    /// </summary>
    public partial class EditPacient : Page
    {
        DispatcherTimer DispatcherTimer;
        TimeSpan Time;
        Entities.Laborant CurrentLaborantP = null;
        public EditPacient(int Seconds,Entities.Laborant CurrentLaborant)
        {
            InitializeComponent();
            CboxPacient.ItemsSource = App.Context.Pacient.ToList();
            CurrentLaborantP = CurrentLaborant;
            Time = TimeSpan.FromSeconds(Seconds);
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

     

        private void CboxPacient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CboxPacient.SelectedItem != null)
            {
                var CurrentPacient = (Entities.Pacient)CboxPacient.SelectedItem;
                TboxName.Text = CurrentPacient.Name;
                TboxSername.Text = CurrentPacient.Sername;
                TboxOtchectvo.Text = CurrentPacient.Patronomic;
                TboxSPasport.Text = CurrentPacient.SeriaPassport.ToString();
                TboxNPasport.Text = CurrentPacient.NumberPasport.ToString();
                DateOfBirthday.SelectedDate = CurrentPacient.DateOfBirthday;

            }

        }

        private void BtnEditPacient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ErorMessage = CheckErrors();
                if (ErorMessage.Length > 0)
                {
                    MessageBox.Show(ErorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (CboxPacient.SelectedItem != null)
                    {
                        var CurrentPacient = (Entities.Pacient)CboxPacient.SelectedItem;

                        CurrentPacient.Name = TboxName.Text;
                        CurrentPacient.Sername = TboxSername.Text;
                        CurrentPacient.Patronomic = TboxOtchectvo.Text;
                        CurrentPacient.SeriaPassport = Convert.ToInt32(TboxSPasport.Text);
                        CurrentPacient.NumberPasport = Convert.ToInt32(TboxNPasport.Text);
                        CurrentPacient.DateOfBirthday = DateOfBirthday.SelectedDate;

                        App.Context.SaveChanges();
                        MessageBox.Show("Редактирование успешно");
                    }
                    else
                    {
                        MessageBox.Show("Выберите пациента для редактирования");
                    }

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
            if (TboxName.Text == "" || !Regex.IsMatch(TboxName.Text, @"^[А-Яа-я]|\s+$")) ErrorBuilder.AppendLine("Имя обязательно для заполнения и содержит только буквы;");
            if (TboxSername.Text == "" || !Regex.IsMatch(TboxSername.Text, @"^[А-Яа-я]|\s+$")) ErrorBuilder.AppendLine("Фамилия обязательна для заполнения и содержит только буквы;");
            if (TboxOtchectvo.Text == "" || !Regex.IsMatch(TboxOtchectvo.Text, @"^[А-Яа-я]|\s+$")) ErrorBuilder.AppendLine("Отчество обязательно для заполнения и содержит только буквы;");
            if (DateOfBirthday.SelectedDate == null && DateOfBirthday.SelectedDate < DateTime.Now.Date) ErrorBuilder.AppendLine("Дата обязательна для выбора и не может быть больше текущей;");
            if (TboxSPasport.Text == "" || !Regex.IsMatch(TboxSPasport.Text, @"^[0-9]+$")) ErrorBuilder.AppendLine("Поле паспорт серия должена быть заполнена в нужном формате и не может быть пустым;");
            if (TboxNPasport.Text == "" || !Regex.IsMatch(TboxNPasport.Text, @"^[0-9]+$")) ErrorBuilder.AppendLine("Поле паспорт номер должен быть заполнен в нужном формате и не может быть пустым;");
            return ErrorBuilder.ToString();
        }
    }
}
