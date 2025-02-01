using QuickDeal.Authentication;
using QuickDeal.Pages;
using System.Linq;
using System.Windows;

namespace QuickDeal
{
    public partial class MainWindow : Window
    {
        private readonly QuickDealEntities db;
        public bool isNav = false;
        public MainWindow()
        {
            InitializeComponent();
            db = new QuickDealEntities();
            FrameManager.MainFrame = MainFrame;
            FrameManager.MainFrame.Navigate(new MainPage());
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
    }
}