using System;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using QuickDeal;

namespace QuickDeal.Pages
{
    public partial class Profile : Page
    {
        private readonly QuickDealEntities db;
        private readonly int userID;

        public Profile()
        {
            InitializeComponent();
            db = new QuickDealEntities();
            userID = ((App)Application.Current).CurrentUserID;
            LoadActiveAds();
            LoadCompletedAds();
        }

        private void LoadActiveAds()
        {
            try
            {
                var activeAds = db.ads
                    .Where(a => a.user_id == userID && a.ad_statuses.ad_status_name == "Активное")
                    .Select(a => new
                    {
                        a.ad_id,
                        a.title,
                        a.description,
                        a.ad_price,
                        a.publish_date,
                        CityName = a.city.city_name,
                        CategoryName = a.ad_categories.ad_category_name,
                        TypeName = a.ad_types.ad_type_name,
                        StatusName = a.ad_statuses.ad_status_name,
                        a.ad_image
                    }).ToList();

                AListAds.ItemsSource = activeAds;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки активных объявлений: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadCompletedAds()
        {
            try
            {
                var completedAds = db.ads
                    .Where(a => a.user_id == userID && a.ad_statuses.ad_status_name == "Завершено")
                    .Select(a => new
                    {
                        a.ad_id,
                        a.title,
                        a.description,
                        a.ad_price,
                        a.publish_date,
                        CityName = a.city.city_name,
                        CategoryName = a.ad_categories.ad_category_name,
                        TypeName = a.ad_types.ad_type_name,
                        StatusName = a.ad_statuses.ad_status_name,
                        a.ad_image
                    }).ToList();

                DListAds.ItemsSource = completedAds;

                decimal totalProfit = completedAds.Sum(a => a.ad_price);
                TotalProfitTextBlock.Text = $"Общая прибыль: {totalProfit:C}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки завершенных объявлений: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new AdvAdd());
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            dynamic selectedAd = null;

            if (AListAds.SelectedItem != null)
            {
                selectedAd = AListAds.SelectedItem;
            }

            else if (DListAds.SelectedItem != null)
            {
                selectedAd = DListAds.SelectedItem;
            }

            if (selectedAd != null)
            {
                int adId = selectedAd.ad_id;
                FrameManager.MainFrame.Navigate(new AdvAdd(adId));
            }
            else
            {
                MessageBox.Show("Выберите объявление для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            dynamic selectedAd = null;

            if (AListAds.SelectedItem != null)
            {
                selectedAd = AListAds.SelectedItem;
            }
            
            else if (DListAds.SelectedItem != null)
            {
                selectedAd = DListAds.SelectedItem;
            }

            if (selectedAd != null)
            {
                int advID = selectedAd.ad_id;

                if (MessageBox.Show("Вы точно хотите удалить выбранное объявление?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        var adToDelete = db.ads.FirstOrDefault(a => a.ad_id == advID);
                        if (adToDelete != null)
                        {
                            db.ads.Remove(adToDelete);
                            db.SaveChanges();
                            LoadActiveAds();
                            LoadCompletedAds();
                        }
                        else
                        {
                            MessageBox.Show("Не удалось найти объявление для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка удаления объявления: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите объявление для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnDone_Click(object sender, RoutedEventArgs e)
        {
            dynamic selectedAd = null;

            if (AListAds.SelectedItem != null)
            {
                selectedAd = AListAds.SelectedItem;
            }
            else if (DListAds.SelectedItem != null)
            {
                selectedAd = DListAds.SelectedItem;
            }

            if (selectedAd != null)
            {
                int adId = selectedAd.ad_id;

                var adToFinish = db.ads.FirstOrDefault(a => a.ad_id == adId);
                if (adToFinish != null)
                {
                    var completedStatus = db.ad_statuses.FirstOrDefault(s => s.ad_status_name == "Завершено");
                    if (adToFinish.ad_status_id == completedStatus?.ad_status_id)
                    {
                        MessageBox.Show("Это объявление уже завершено.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (MessageBox.Show("Вы точно хотите завершить выбранное объявление?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            if (completedStatus != null)
                            {
                                adToFinish.ad_status_id = completedStatus.ad_status_id;
                                db.SaveChanges();

                                LoadActiveAds();
                                LoadCompletedAds();

                                MessageBox.Show("Объявление успешно завершено.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show("Не удалось найти статус 'Завершено' в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при завершении объявления: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось найти выбранное объявление.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите объявление для завершения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AListAds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var selectedAd = AListAds.SelectedItem as dynamic;

            if (selectedAd != null)
            {
                int adId = selectedAd.ad_id;
                FrameManager.MainFrame.Navigate(new AdvAdd(adId));
            }
            else
            {
                MessageBox.Show("Выберите объявление для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MainPage());
        }

        private void AListAds_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            dynamic selectedAd = ((ListView)sender).SelectedItem;

            if (selectedAd != null)
            {
                int adId = selectedAd.ad_id;
                FrameManager.MainFrame.Navigate(new AdvAdd(adId));
            }
        }
    }
}