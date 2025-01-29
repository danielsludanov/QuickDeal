using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace QuickDeal.Authentication
{
    public partial class Auth : Window
    {
        private bool isNav = false;
        public Auth()
        {
            InitializeComponent();
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
