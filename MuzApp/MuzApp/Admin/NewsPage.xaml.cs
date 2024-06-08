using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MuzApp.DbTables;

namespace MuzApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        public New SelectedNew { get; set; }
        FirebaseClient firebaseClient;

        public NewsPage()
        {
            InitializeComponent();
            firebaseClient = new FirebaseClient("https://muzicschool-f7f69-default-rtdb.firebaseio.com/");
            LoadNewsAsync();
        }

        private async void LoadNewsAsync()
        {
            try
            {
                var newsItems = await GetAllNewsAsync();
                ItemColl.ItemsSource = newsItems;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Ошибка загрузки новостей: {ex.Message}", "Ок");
            }
        }

        private async Task<List<New>> GetAllNewsAsync()
        {
            return (await firebaseClient
                .Child("News")
                .OnceAsync<New>()).Select(item =>
                {
                    var newsItem = item.Object;
                    newsItem.Id = item.Key;
                    return newsItem;
                }).ToList();
        }

        private async void ItemColl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is New selectedNews)
            {
                await Navigation.PushAsync(new AboutNew(selectedNews));
            }
        }
        private async void AddNew_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddNew());
        }
    }
}