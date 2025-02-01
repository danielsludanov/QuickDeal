using QuickDeal.Authentication;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QuickDeal.Pages
{
    public partial class MainPage : Page
    {
        private readonly QuickDealEntities db;
        public MainPage()
        {
            InitializeComponent();
            InitializeComponent();
            db = new QuickDealEntities();
            LoadFilters();
            FilterAds();

            int currentUserID = ((App)Application.Current).CurrentUserID;

            if (currentUserID != 0)
            {
                BtnLogin.Visibility = Visibility.Collapsed;
                BtnProfile.Visibility = Visibility.Visible;
            }

            City.SelectionChanged += (s, e) => FilterAds();
            Category.SelectionChanged += (s, e) => FilterAds();
            Type.SelectionChanged += (s, e) => FilterAds();
            Status.SelectionChanged += (s, e) => FilterAds();
            Search.TextChanged += (s, e) => FilterAds();
        }

        private void LoadFilters()
        {
            City.ItemsSource = db.cities.ToList();
            City.DisplayMemberPath = "city_name";
            City.SelectedValuePath = "city_id";

            Category.ItemsSource = db.ad_categories.ToList();
            Category.DisplayMemberPath = "ad_category_name";
            Category.SelectedValuePath = "ad_category_id";

            Type.ItemsSource = db.ad_types.ToList();
            Type.DisplayMemberPath = "ad_type_name";
            Type.SelectedValuePath = "ad_type_id";

            Status.ItemsSource = db.ad_statuses.ToList();
            Status.DisplayMemberPath = "ad_status_name";
            Status.SelectedValuePath = "ad_status_id";
        }

        private void FilterAds()
        {
            var ads = db.ads.AsQueryable();

            int completedStatusId = GetCompletedStatusId();
            if (completedStatusId != -1)
            {
                ads = ads.Where(a => a.ad_status_id != completedStatusId);
            }

            if (City.SelectedValue != null)
            {
                int cityId = (int)City.SelectedValue;
                ads = ads.Where(a => a.city_id == cityId);
            }

            if (Category.SelectedValue != null)
            {
                int categoryId = (int)Category.SelectedValue;
                ads = ads.Where(a => a.ad_category_id == categoryId);
            }

            if (Type.SelectedValue != null)
            {
                int typeId = (int)Type.SelectedValue;
                ads = ads.Where(a => a.ad_type_id == typeId);
            }

            if (Status.SelectedValue != null)
            {
                int statusId = (int)Status.SelectedValue;
                ads = ads.Where(a => a.ad_status_id == statusId);
            }

            string searchText = Search.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                ads = ads.Where(a => a.title.ToLower().Contains(searchText) || a.description.ToLower().Contains(searchText));
            }

            ListAds.ItemsSource = ads.ToList();
        }

        private void ClearFilters_Click(object sender, RoutedEventArgs e)
        {
            City.SelectedValue = null;
            Category.SelectedValue = null;
            Type.SelectedValue = null;
            Status.SelectedValue = null;
            Search.Text = string.Empty;

            FilterAds();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).isNav = true;
            Auth authWindow = new Auth();
            authWindow.Show();
            Window.GetWindow(this)?.Close();
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new Profile());
        }

        private int GetCompletedStatusId()
        {
            var completedStatus = db.ad_statuses.FirstOrDefault(s => s.ad_status_name == "Завершено");
            return completedStatus?.ad_status_id ?? -1;
        }
    }
}
