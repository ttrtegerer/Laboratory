using MobileLaboratory.Class;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileLaboratory
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomPage : ContentPage
    {
        string connectionString = @"Data Source=stud-mssql.sttec.yar.ru,38325;Database=user79_db;" + "User ID=user79_db;Password=user79;Connect Timeout=30;" + "Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;" + "MultiSubnetFailover=False";
        SqlConnection connection;
        protected internal ObservableCollection<Yslugi> Yslygs { get; set; }
        protected internal ObservableCollection<News> News { get; set; }
        public WelcomPage()
        {
            InitializeComponent();

            connection = new SqlConnection(connectionString);
            string query = String.Format("SELECT * from [s5_YslugiLaboratorii] ");
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            Yslygs = new ObservableCollection<Yslugi> { };
            while (rd.Read())
            {
                var yslydis = new Yslugi
                {
                    Name = rd.GetString(1),
                    Prise = rd.GetDouble(2)
                   
                };
                Yslygs.Add(yslydis);
            }
            LvYslygi.BindingContext = Yslygs;
            connection.Close();
           
            connection = new SqlConnection(connectionString);
            query = String.Format("SELECT * from [s5_News] ");
            cmd = new SqlCommand(query, connection);
            connection.Open();
             rd = cmd.ExecuteReader();
            News = new ObservableCollection<News> { };
            while (rd.Read())
            {
                var news = new News
                {   Title=rd.GetString(0),
                    Date=rd.GetDateTime(1),
                    Text=rd.GetString(2)

                };
                News.Add(news);
            }
            LvNews.BindingContext = News;
            connection.Close();
        }

      

        private void EventsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void LvYslygi_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void LvNews_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

       

        private async void BtnReg_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegPage());
        }
    }
}