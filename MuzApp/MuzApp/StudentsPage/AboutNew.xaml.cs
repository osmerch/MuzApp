using Firebase.Database;
using Firebase.Database.Query;
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
    public partial class AboutNew : ContentPage
    {
        public bool edited = true;
        public string id_news;
        private FirebaseClient firebaseClient;
        public AboutNew(New n)
        {
            InitializeComponent();
            TitleTxt.Text = n.Title;
            DateLabel.Text = n.Date.ToString("f");
            DescLabel.Text = n.Description;
            NewsImage.Source = n.Image; firebaseClient = new FirebaseClient("https://muzicschool-f7f69-default-rtdb.firebaseio.com/");
            id_news = n.Id;
        }
        private async Task DeleteNew(string newId)
        {
            var lessonToDelete = (await firebaseClient
                .Child("News")
                .OnceAsync<New>()).FirstOrDefault(a => a.Object.Id == newId);

            if (lessonToDelete != null)
            {
                await firebaseClient.Child("Lesson").Child(lessonToDelete.Key).DeleteAsync();
            }
        }
        private async void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            var confirm = await DisplayAlert("Подтверждение", "Вы уверены, что хотите удалить это занятие?", "Да", "Нет");
            if (confirm)
            {
                await DeleteNew(id_news);
                await DisplayAlert("Успех", "Занятие удалено", "Ок");
                await Navigation.PopAsync(); // Возвращаемся на предыдущую страницу
            }
        }
    }
}