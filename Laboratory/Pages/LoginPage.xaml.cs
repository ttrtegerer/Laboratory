using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            CBPasswordActive.IsChecked = true;
        }

        private async void Vhod_Click(object sender, RoutedEventArgs e)
        { if (TBoxCaptcha.Text != TBoxResaultCaptcha.Text)
            {
                MessageBox.Show("Капча не верна");
                Captcha();
                Vhod.IsEnabled = false;
                for (int i = 10; i >= 0; i--)
                {
                    Vhod.Content = String.Format("Заблокировано {0} секунд", i);
                    await Task.Delay(1000);
                }
                Vhod.Content = "Войти";
                Vhod.IsEnabled = true;

            }
            else
            {
                CBPasswordNoActive.IsChecked = true;
                CBPasswordActive.IsChecked = true;
                CBPasswordNoActive.IsChecked = false;
                try
                {
                    if (TBoxLogin.Text != "" && TBoxPassword.Text != "" && TBoxCaptcha.Text == TBoxResaultCaptcha.Text)
                    {
                        var CurrentAdmin = App.Context.Admin.FirstOrDefault(p => p.Login == TBoxLogin.Text && p.Password == TBoxPassword.Text);
                        if (CurrentAdmin != null)
                        {
                            MessageBox.Show("Вход за администратора успешен");
                            NavigationService.Navigate(new AdministratorVhod(CurrentAdmin));
                        }
                        else
                        {
                            var CurrentBexgalter = App.Context.Byxgalter.FirstOrDefault(p => p.Login == TBoxLogin.Text && p.Password == TBoxPassword.Text);
                            if (CurrentBexgalter != null)
                            {
                                MessageBox.Show(CurrentBexgalter.Sername + " " + CurrentBexgalter.Name + " " + CurrentBexgalter.Patronomic + ":" + "Вход за бухгалтера успешен");
                                NavigationService.Navigate(new ByxgalterVhod(CurrentBexgalter));
                            }
                            else
                            {
                                var CurrentLaborant = App.Context.Laborant.FirstOrDefault(p => p.Login == TBoxLogin.Text && p.Password == TBoxPassword.Text);
                                if (CurrentLaborant != null)
                                {
                                    MessageBox.Show(CurrentLaborant.Sername + " " + CurrentLaborant.Name + " " + CurrentLaborant.Patronomic + ":" + "Вход за Лаборанта успешен");
                                    NavigationService.Navigate(new LaborantVhod(CurrentLaborant));
                                }
                                else
                                {
                                    var CurrentUser = App.Context.Pacient.FirstOrDefault(p => p.Login == TBoxLogin.Text && p.Password == TBoxPassword.Text);
                                    if (CurrentUser != null)
                                    {
                                        MessageBox.Show(CurrentUser.Sername + " " + CurrentUser.Name + " " + CurrentUser.Patronomic + ":" + "Вход за Пациента успешен");
                                        NavigationService.Navigate(new PacientVhod(CurrentUser));
                                    }
                                    else
                                    {
                                        MessageBox.Show("Авторизация неуспешна");
                                        Captcha();
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Логин или пароль не могут быть пустыми");
                        Captcha();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка");

                }
            }
        }
       private void Captcha()
        {
            ReloadCaptcha.Visibility = Visibility.Visible;
            TBoxCaptcha.Text = null;
            TBoxCaptcha.IsEnabled = false;
            TBlockCaptcha.Visibility = Visibility.Visible;
            TBlockResaultCaptcha.Visibility = Visibility.Visible;
            TBlockCaptcha.Visibility = Visibility.Visible;
            TBoxCaptcha.Visibility = Visibility.Visible;
            TBoxResaultCaptcha.Visibility=Visibility.Visible;
            String allowchar = " ";

            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";

            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";

            allowchar += "1,2,3,4,5,6,7,8,9,0";

            char[] A = { ',' };

            String[] Ar = allowchar.Split(A);

  

            string Temp = " ";

            Random r = new Random();


            for (int i = 0; i < 4; i++)
            {
                TBoxCaptcha.FontSize = r.Next(12, 25);
                Temp = Ar[(r.Next(0, Ar.Length))];
                TBoxCaptcha.AppendText(Temp);
                
            }
            
        }
    

        private void RBPassword_Checked(object sender, RoutedEventArgs e)
        {

            if (CBPasswordNoActive.IsChecked.Value)
            {
                CBPasswordActive.IsChecked = false;
                TBoxPassword.Text = PBPassword.Password;
                TBoxPassword.Visibility = Visibility.Visible;
                PBPassword.Visibility = Visibility.Collapsed;

            }
           
            
        }

        private void CBPasswordActive_Checked(object sender, RoutedEventArgs e)
        {
            if (CBPasswordActive.IsChecked.Value)
            {
                CBPasswordNoActive.IsChecked = false;
                PBPassword.Password = TBoxPassword.Text;
                TBoxPassword.Visibility = Visibility.Collapsed;
                PBPassword.Visibility = Visibility.Visible;
            }
        }

        private void ReloadCaptcha_Click(object sender, RoutedEventArgs e)
        {
            Captcha();
        }
    }
}
