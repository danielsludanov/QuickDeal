using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace QuickDeal.Authentication
{
    public partial class Reg : Window
    {
        private readonly QuickDealEntities db;
        public bool isNav = false;
        private static readonly Regex LoginRegex = new Regex(@"^[a-zA-Z0-9]{6,}$");
        private static readonly Regex PasswordRegex = new Regex(@"^[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{}|;:'"",.<>?/]{6,}$");


        public Reg()
        {
            InitializeComponent();
            db = new QuickDealEntities();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isNav)
            {
                return;
            }
            
            if (MessageBox.Show("Вы действительно хотите выйти из приложения?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void LinkAuth_Click(object sender, RoutedEventArgs e)
        {
            isNav = true;
            Auth auth = new Auth();
            auth.Show();
            this.Close();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string Login = UserLogin.Text;
            string Password = UserPassword.Password;

            try
            {
                if (!LoginRegex.IsMatch(Login))
                {
                    MessageBox.Show("Вы неправильно ввели логин",
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    MessageBox.Show("Логин может содержать: английские символы\n" +
                        "Цифры" +
                        "Минимальное количество символов в логине: 6",
                        "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (db.users.Any(u => u.login == Login))
                {
                    MessageBox.Show("Пользователь с таким логином уже существует в системе",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!PasswordRegex.IsMatch(Password))
                {
                    MessageBox.Show("Вы неправильно ввели пароль",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    MessageBox.Show("Пароль может содержать: английские символы\n" +
                        "Цифры\n" +
                        "Специальные символы" +
                        "Минимальное колчество символов в пароле: 6",
                        "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;

                }

                var newUser = new user
                {
                    login = Login,
                    password = Password,
                };
                db.users.Add(newUser);
                db.SaveChanges();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
