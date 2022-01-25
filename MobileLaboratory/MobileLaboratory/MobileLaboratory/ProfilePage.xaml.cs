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
    public partial class ProfilePage : ContentPage
    {
        string connectionString = @"Data Source=stud-mssql.sttec.yar.ru,38325;Database=user79_db;" + "User ID=user79_db;Password=user79;Connect Timeout=30;" + "Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;" + "MultiSubnetFailover=False";
        SqlConnection connection;
        int Id = 0;
        public ProfilePage(int PacientId)
        {
            Id = PacientId;
            InitializeComponent();
            TboxName.IsEnabled = false;
            TboxSername.IsEnabled = false;
            TboxOtchectvo.IsEnabled = false;
            TboxDayOfBirthday.IsEnabled = false;
            TboxSPassport.IsEnabled = false;
            TboxNPassport.IsEnabled = false;
            TboxStraxovaya.IsEnabled = false;
            TboxLogin.IsEnabled = false;
            
            connection = new SqlConnection(connectionString);
            string command = "SELECT * FROM [s5_Pacient] where Id='" +PacientId+ "'";
            SqlCommand myCommand = new SqlCommand(command, connection);
            connection.Open();
            SqlDataReader a = myCommand.ExecuteReader();
            while (a.Read())
            {
                TboxLogin.Text = a.GetString(1);
                TboxPassword.Text = a.GetString(2);
                TboxName.Text= a.GetString(3);
                TboxSername.Text = a.GetString(4);
                TboxOtchectvo.Text = a.GetString(5);
                TboxDayOfBirthday.Text = Convert.ToString( a.GetDateTime(7));
                TboxSPassport.Text = Convert.ToString(a.GetInt32(8));
                TboxNPassport.Text = Convert.ToString(a.GetInt32(9));
                TboxPhone.Text = a.GetString(10);
                TboxPhocta.Text = a.GetString(11);
            }
            connection.Close();
             command = "SELECT * FROM [s5_PacientInsuranse] where CodPacient='" + PacientId + "'";
             myCommand = new SqlCommand(command, connection);
            connection.Open();
            a = myCommand.ExecuteReader();
            while (a.Read())
            {
                TboxStraxovaya.Text = Convert.ToString(a.GetInt32(4));
            }
            connection.Close();
        }

        private void BtnEdit_Clicked(object sender, EventArgs e)
        {
            try
            {

            
            var ErrorMessage = ChekErrors();
            if (ErrorMessage.Length > 0)
            {
                _ = DisplayAlert("Ошибка", ErrorMessage, "ок");
            }
            else
            {
                
                
                connection.Open();
                string command = String.Format("UPDATE [s5_Pacient] SET Password='{0}',Phone='{1}',Pochta='{2}' WHERE Id={3}", TboxPassword.Text,TboxPhone.Text,TboxPhocta.Text,Id );
                SqlCommand myCommand = new SqlCommand(command, connection);
                myCommand.ExecuteNonQuery();
                connection.Close();
                    _ = DisplayAlert("Успех","Данные успешно изменены", "ок");
                }
            }
            catch
            {
                _ = DisplayAlert("Ошибка", " Ошибка", "ок");
            }

        }
        private string ChekErrors()
        {
            var ErrorBuilder = new StringBuilder();
           
           
            if (TboxPhone.Text == "" || !Regex.IsMatch(TboxPhone.Text, @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{6,7}$")) ErrorBuilder.AppendLine("Телефон должен быть заполнен в нужном формате и не может быть пустым;");
            if (TboxPhocta.Text == "" || !Regex.IsMatch(TboxPhocta.Text, @"^[a-z0-9] [- a-z0-9._] +@([- a-z0-9]+.) +[a-z] {2,5}$")) ErrorBuilder.AppendLine("Почта должна быть заполнена в нужном формате и не может быть пустой;");
            if (TboxPassword.Text =="" || TboxPassword.Text.Length<8 ) ErrorBuilder.AppendLine("Пароль не может быть пустым и должен содержать от 8 символов;");

            return ErrorBuilder.ToString();
        }
    }
}