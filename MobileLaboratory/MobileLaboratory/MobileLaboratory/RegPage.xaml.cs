using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileLaboratory
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegPage : ContentPage
    {
        string connectionString = @"Data Source=stud-mssql.sttec.yar.ru,38325;Database=user79_db;" + "User ID=user79_db;Password=user79;Connect Timeout=30;" + "Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;" + "MultiSubnetFailover=False";
        SqlConnection connection;
        public RegPage()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
        }

        private void BtnReg_Clicked(object sender, EventArgs e)
        {
            try
            {   var ErrorMessege = CheckErrors();
                if (ErrorMessege.Length > 1)
                {
                    _ = DisplayAlert("Ошибка", ErrorMessege, "ок");
                }
                else
                     {
                            
                            connection.Open();
                            string command = String.Format("INSERT INTO [s5_Pacient] (Login, Password, Name,Sername,Patronomic,DateOfBirthday,SeriaPassport,NumberPasport,Phone,Pochta) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')",TboxLogin.Text,TboxPassword.Text,TboxName.Text,TboxSername.Text,TboxOtchectvo.Text,TboxDayOfBirthday.Text,TboxSPassport.Text,TboxNPassport.Text,TboxPhone.Text,TboxPhocta.Text); 
                            SqlCommand myCommand = new SqlCommand(command, connection);
                            myCommand.ExecuteNonQuery();
                            connection.Close();
                    _ = DisplayAlert("Успех", "Вы зарегестрированы", "ок");
                }
            }
            catch
            {
                _ = DisplayAlert("Ошибка", " Ошибка", "ок");
            }
        }
        private string CheckErrors()
        {
            var ErrorBuilder = new StringBuilder();
            if (TboxName.Text == "" || !Regex.IsMatch(TboxName.Text, @"^[А-Яа-я]|\s+$")) ErrorBuilder.AppendLine("Имя обязательно для заполнения и содержит только буквы;");
            if (TboxSername.Text == "" || !Regex.IsMatch(TboxSername.Text, @"^[А-Яа-я]|\s+$")) ErrorBuilder.AppendLine("Фамилия обязательна для заполнения и содержит только буквы;");
            if (TboxOtchectvo.Text == "" || !Regex.IsMatch(TboxOtchectvo.Text, @"^[А-Яа-я]|\s+$")) ErrorBuilder.AppendLine("Отчество обязательно для заполнения и содержит только буквы;");
            if (TboxDayOfBirthday.Text == null) ErrorBuilder.AppendLine("Дата обязательна для выбора и не может быть больше текущей;");
            if (TboxPhone.Text == "" || !Regex.IsMatch(TboxPhone.Text, @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{6,7}$")) ErrorBuilder.AppendLine("Телефон должен быть заполнен в нужном формате и не может быть пустым;");
            if (TboxPhocta.Text == "" || !Regex.IsMatch(TboxPhocta.Text, @"^[a-z0-9] [- a-z0-9._] +@([- a-z0-9]+.) +[a-z] {2,5}$")) ErrorBuilder.AppendLine("Почта должна быть заполнена в нужном формате и не может быть пустой;");
            if (TboxSPassport.Text == "" || !Regex.IsMatch(TboxSPassport.Text, @"^[0-9]+$")) ErrorBuilder.AppendLine("Поле паспорт серия должена быть заполнена в нужном формате и не может быть пустым;");
            if (TboxNPassport.Text == "" || !Regex.IsMatch(TboxNPassport.Text, @"^[0-9]+$")) ErrorBuilder.AppendLine("Поле паспорт номер должен быть заполнен в нужном формате и не может быть пустым;");
            if (TboxStraxovaya.Text == "" || !Regex.IsMatch(TboxStraxovaya.Text, @"^[0-9]+$")) ErrorBuilder.AppendLine("Поле номер страхового полиса должен быть заполнен в нужном формате и не может быть пустым;");
            if (TboxPassword.Text == "" || TboxPassword.Text.Length < 8) ErrorBuilder.AppendLine("Пароль не может быть пустым и должен содержать от 8 символов;");
            if (TboxLogin.Text == "" || TboxLogin.Text.Length < 8) ErrorBuilder.AppendLine("Логин не может быть пустым и должен содержать от 8 символов;");

            return ErrorBuilder.ToString();
        }
    }
}