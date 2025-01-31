using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace QuickDeal.Pages
{
    public partial class AdvAdd : Page
    {
        private readonly int currentUserID = ((App)Application.Current).CurrentUserID;
        private readonly QuickDealEntities db;
        private int? adIdToEdit;

        public AdvAdd(int? adId = null)
        {
            InitializeComponent();
            db = new QuickDealEntities();
            LoadCities();
            LoadCategories();
            LoadTypes();
            LoadStatuses();

            if(adId.HasValue)
        {
                adIdToEdit = adId.Value;
                LoadAdData(adIdToEdit.Value);
                AddAdButton.Content = "Сохранить изменения";
            }
        else
            {
                AddAdButton.Content = "Добавить объявление";
            }
        }

        private bool HasFullName()
        {
            var user = db.users.FirstOrDefault(u => u.user_id == currentUserID);
            return user != null && !string.IsNullOrWhiteSpace(user.first_name) && !string.IsNullOrWhiteSpace(user.last_name);
        }

        private void LoadAdData(int adId)
        {
            try
            {

                if (!HasFullName())
                {
                    MessageBox.Show("Для добавления или редактирования объявления необходимо указать ФИО.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var ad = db.ads.FirstOrDefault(a => a.ad_id == adId);
                if (ad != null)
                {
                    
                    TitleTextBox.Text = ad.title;
                    DescriptionTextBox.Text = ad.description;
                    PriceTextBox.Text = ad.ad_price.ToString();
                    ImagePathTextBox.Text = ad.ad_image;

                    CityComboBox.SelectedValue = ad.city_id;
                    CategoryComboBox.SelectedValue = ad.ad_category_id;
                    AdTypeComboBox.SelectedValue = ad.ad_type_id;
                    StatusComboBox.SelectedValue = ad.ad_status_id;
                }
                else
                {
                    MessageBox.Show("Не удалось найти объявление для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных объявления: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadCities()
        {
            var cities = db.cities.ToList();
            CityComboBox.ItemsSource = cities;
            CityComboBox.DisplayMemberPath = "city_name";
            CityComboBox.SelectedValuePath = "city_id";
        }

        private void LoadCategories()
        {
            var categories = db.ad_categories.ToList();
            CategoryComboBox.ItemsSource = categories;
            CategoryComboBox.DisplayMemberPath = "ad_category_name";
            CategoryComboBox.SelectedValuePath = "ad_category_id";
        }

        private void LoadTypes()
        {
            var types = db.ad_types.ToList();
            AdTypeComboBox.ItemsSource = types;
            AdTypeComboBox.DisplayMemberPath = "ad_type_name";
            AdTypeComboBox.SelectedValuePath = "ad_type_id";
        }

        private void LoadStatuses()
        {
            var statuses = db.ad_statuses.ToList();
            StatusComboBox.ItemsSource = statuses;
            StatusComboBox.DisplayMemberPath = "ad_status_name";
            StatusComboBox.SelectedValuePath = "ad_status_id";
        }

        private void AddAdButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(TitleTextBox.Text) ||
                    string.IsNullOrWhiteSpace(DescriptionTextBox.Text) ||
                    string.IsNullOrWhiteSpace(PriceTextBox.Text) ||
                    CityComboBox.SelectedItem == null ||
                    CategoryComboBox.SelectedItem == null ||
                    AdTypeComboBox.SelectedItem == null ||
                    StatusComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                
                string titlePattern = @"^[a-zA-Zа-яА-ЯёЁ0-9\s,.'-]+$";
                if (!Regex.IsMatch(TitleTextBox.Text, titlePattern))
                {
                    MessageBox.Show("Название объявления должно содержать только русские и английские буквы, цифры и пробелы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string descriptionPattern = @"^[a-zA-Zа-яА-ЯёЁ0-9\s,.'-]+$";
                if (!Regex.IsMatch(DescriptionTextBox.Text, descriptionPattern))
                {
                    MessageBox.Show("Описание объявления должно содержать только русские и английские буквы, цифры и пробелы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

              
                string pricePattern = @"^\d+(\.\d{1,2})?$";
                if (!Regex.IsMatch(PriceTextBox.Text, pricePattern))
                {
                    MessageBox.Show("Цена должна содержать только цифры и может иметь до 2 знаков после запятой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                
                decimal price = decimal.Parse(PriceTextBox.Text);


                if (adIdToEdit.HasValue)
                {
                    var existingAd = db.ads.FirstOrDefault(a => a.ad_id == adIdToEdit.Value);
                    if (existingAd != null)
                    {
                        
                        existingAd.title = TitleTextBox.Text;
                        existingAd.description = DescriptionTextBox.Text;
                        existingAd.ad_price = price;
                        existingAd.ad_image = ImagePathTextBox.Text;
                        existingAd.city_id = (int)CityComboBox.SelectedValue;
                        existingAd.ad_category_id = (int)CategoryComboBox.SelectedValue;
                        existingAd.ad_type_id = (int)AdTypeComboBox.SelectedValue;
                        existingAd.ad_status_id = (int)StatusComboBox.SelectedValue;

                        
                        db.SaveChanges();

                        MessageBox.Show("Объявление обновлено успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Не удалось найти объявление для обновления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    
                    var newAd = new ad
                    {
                        user_id = currentUserID,
                        ad_category_id = (int)CategoryComboBox.SelectedValue,
                        ad_type_id = (int)AdTypeComboBox.SelectedValue,
                        ad_status_id = (int)StatusComboBox.SelectedValue,
                        city_id = (int)CityComboBox.SelectedValue,
                        title = TitleTextBox.Text,
                        description = DescriptionTextBox.Text,
                        ad_price = price,
                        ad_image = ImagePathTextBox.Text,
                        publish_date = DateTime.Now
                    };

                    db.ads.Add(newAd);
                    db.SaveChanges();

                    MessageBox.Show("Объявление добавлено успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении/редактировании объявления: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearForm()
        {
            TitleTextBox.Clear();
            DescriptionTextBox.Clear();
            PriceTextBox.Clear();
            ImagePathTextBox.Clear();
            CityComboBox.SelectedIndex = -1;
            CategoryComboBox.SelectedIndex = -1;
            AdTypeComboBox.SelectedIndex = -1;
            StatusComboBox.SelectedIndex = -1;
        }
    }
}
