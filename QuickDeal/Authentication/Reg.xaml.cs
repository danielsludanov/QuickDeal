using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace QuickDeal.Authentication
{
    public partial class Reg : Window
    {
        private readonly QuickDealEntities db;
        public bool isNav = false;
        private static readonly Regex LoginRegex = new Regex(@"^[a-zA-Z0-9]{6,}$");
        private static readonly Regex PasswordRegex = new Regex(@"^[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{}|;:'"",.<>?/]{6,}$");
        private static readonly Regex FioRegex = new Regex(@"^[a-zA-Zа-яА-ЯёЁ]+(\s[a-zA-Zа-яА-ЯёЁ]+){2}$");
        private static readonly Regex PhoneRegex = new Regex(@"^\+7\(\d{3}\)\s?\d{3}-\d{2}-\d{2}$");

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
            string FIO = UserFIO.Text;
            string PhoneNumber = UserPhoneNumber.Text;

            try
            {
                
                if (!LoginRegex.IsMatch(Login))
                {
                    MessageBox.Show("Вы неправильно ввели логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    MessageBox.Show("Логин может содержать: английские символы и цифры, минимальное количество символов: 6", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                
                if (db.users.Any(u => u.login == Login))
                {
                    MessageBox.Show("Пользователь с таким логином уже существует в системе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
                if (!PasswordRegex.IsMatch(Password))
                {
                    MessageBox.Show("Вы неправильно ввели пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    MessageBox.Show("Пароль может содержать: английские символы, цифры, специальные символы, минимальное количество символов: 6", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                
                if (string.IsNullOrWhiteSpace(FIO) || !FioRegex.IsMatch(FIO))
                {
                    MessageBox.Show("Введите полное ФИО (Фамилия Имя Отчество), используя только русские или английские буквы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
                var fioParts = FIO.Split(' ');
                string lastName = fioParts[0];
                string firstName = fioParts[1];
                string middleName = fioParts[2];

               
                if (!PhoneRegex.IsMatch(PhoneNumber))
                {
                    MessageBox.Show("Введите корректный номер телефона в формате +7 (XXX) XXX-XX-XX", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
                var newUser = new user
                {
                    login = Login,
                    password = Password,
                    last_name = lastName,
                    first_name = firstName,
                    second_name = middleName,
                    phone_number = PhoneNumber
                };

                db.users.Add(newUser);
                db.SaveChanges();

                MessageBox.Show("Вы зарегистрированы в систему!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                isNav = true;
                Auth auth = new Auth();
                auth.Show();
                this.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}