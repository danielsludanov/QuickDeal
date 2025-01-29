using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public partial class Auth : Window
    {
        private readonly QuickDealEntities db;

        private static readonly Regex LoginRegex = new Regex(@"^[a-zA-Z0-9]{6,}$");
        private static readonly Regex PasswordRegex = new Regex(@"^[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{}|;:'"",.<>?/]{6,}$");

        private bool isNav = false;
        public Auth()
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

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string Login = UserLogin.Text;
            string Password = UserPassword.Password;

            // Валидация данных, проверка записи на существование

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

            var CheckUser = db.users.AsNoTracking().FirstOrDefault(u => u.login == Login);

            if (CheckUser == null)
            {
                MessageBox.Show("Пользователя с таким логином не существует в системе",
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
             ((App)Application.Current).CurrentUserID = CheckUser.user_id;
            MessageBox.Show("Вы авторизовались!", 
                "Информация", 
                MessageBoxButton.OK, MessageBoxImage.Information);

            isNav = true;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();

        }

        private void LinkReg_Click(object sender, RoutedEventArgs e)
        {
            isNav = true;
            Reg reg = new Reg();
            reg.Show();
            this.Close();
        }
    }
}
