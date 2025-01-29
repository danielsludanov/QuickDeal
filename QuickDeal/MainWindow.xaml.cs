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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuickDeal
{
    public partial class MainWindow : Window
    {
        private readonly QuickDealEntities db;
        public MainWindow()
        {
            InitializeComponent();
            db = new QuickDealEntities();
            ListAds.ItemsSource = db.ads.ToList();
            LoadCities();
            LoadCategories();
            LoadTypes();
            LoadStatuses();
        }

        private void LoadCities()
        {
            var cities = db.cities.ToList();
            City.ItemsSource = cities;
            City.DisplayMemberPath = "city_name";
            City.SelectedValuePath = "city_id";
        }

        private void LoadCategories()
        {
            var categories = db.ad_categories.ToList();
            Category.ItemsSource = categories;
            Category.DisplayMemberPath = "ad_category_name";
            Category.SelectedValuePath = "ad_category_id";
        }

        private void LoadTypes()
        {
            var types = db.ad_types.ToList();
            Type.ItemsSource = types;
            Type.DisplayMemberPath = "ad_type_name";
            Type.SelectedValuePath = "ad_type_id";
        }
        
        private void LoadStatuses()
        {
            var statuses = db.ad_statuses.ToList();
            Status.ItemsSource = statuses;
            Status.DisplayMemberPath = "ad_status_name";
            Status.SelectedValuePath = "ad_status_id";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(MessageBox.Show("Вы действительно хотите выйти из приложения?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
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
