using QuickDeal.Authentication;
using QuickDeal.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            LoadCities();
            LoadCategories();
            LoadTypes();
            LoadStatuses();
            FilterAds(); // Загружаем объявления с учетом фильтрации

            int currentUserID = ((App)Application.Current).CurrentUserID;

            if (currentUserID != 0)
            {
                BtnLogin.Visibility = Visibility.Collapsed;
                BtnProfile.Visibility = Visibility.Visible;
            }

            // Добавляем обработчики для ComboBox и TextBox
            City.SelectionChanged += (s, e) => FilterAds();
            Category.SelectionChanged += (s, e) => FilterAds();
            Type.SelectionChanged += (s, e) => FilterAds();
            Status.SelectionChanged += (s, e) => FilterAds();
            Search.TextChanged += (s, e) => FilterAds();
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

        private void FilterAds()
        {
            var ads = db.ads.AsQueryable();

            // Фильтр по городу
            if (City.SelectedValue != null)
            {
                int cityId = (int)City.SelectedValue;
                ads = ads.Where(a => a.city_id == cityId);
            }

            // Фильтр по категории
            if (Category.SelectedValue != null)
            {
                int categoryId = (int)Category.SelectedValue;
                ads = ads.Where(a => a.ad_category_id == categoryId);
            }

            // Фильтр по типу
            if (Type.SelectedValue != null)
            {
                int typeId = (int)Type.SelectedValue;
                ads = ads.Where(a => a.ad_type_id == typeId);
            }

            // Фильтр по статусу
            if (Status.SelectedValue != null)
            {
                int statusId = (int)Status.SelectedValue;
                ads = ads.Where(a => a.ad_status_id == statusId);
            }

            // Фильтр по названию или описанию объявления
            string searchText = Search.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                ads = ads.Where(a => a.title.ToLower().Contains(searchText) || a.description.ToLower().Contains(searchText));
            }

            // Обновление списка объявлений
            ListAds.ItemsSource = ads.ToList();
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

        private void ClearFilters_Click(object sender, RoutedEventArgs e)
        {
            // Сброс всех фильтров
            City.SelectedValue = null;
            Category.SelectedValue = null;
            Type.SelectedValue = null;
            Status.SelectedValue = null;
            Search.Text = string.Empty;

            // Обновление списка без фильтров
            FilterAds();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            isNav = true;
            Auth auth = new Auth(); 
            auth.Show();
            this.Close();
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new Profile());
        }
    }
}