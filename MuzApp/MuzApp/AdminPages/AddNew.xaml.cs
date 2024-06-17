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
    public partial class AddNew : ContentPage
    {
        FirebaseClient firebaseClient;
        public AddNew()
        {
            InitializeComponent();
            Task.Run(AnimateBack);
            firebaseClient = new FirebaseClient("https://muzicschool-f7f69-default-rtdb.firebaseio.com/");

        }
        private async void AnimateBack()
        {
            Action<double> forward = input => GradienBack.AnchorY = input;
            Action<double> backward = input => GradienBack.AnchorY = input;

            while (true)
            {
                GradienBack.Animate(name: "forward", callback: forward, start: 0, end: 1, length: 5000, easing: Easing.SinIn);
                await Task.Delay(5000);
                GradienBack.Animate(name: "backward", callback: backward, start: 1, end: 0, length: 5000, easing: Easing.SinIn);
                await Task.Delay(5000);
            }
        }

        private async void AddBtn_Clicked(object sender, EventArgs e)
        {
            string title = title1.Text;
            string imageUrl = imgsrc.Text;
            string description = descTExt.Text;

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(imageUrl) || string.IsNullOrEmpty(description))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, заполните все поля", "Ок");
                return;
            }
            var newsItem = new New
            {
                Title = title,
                Image = imageUrl,
                Description = description,
                Date = DateTime.Today
            };
            try
            {
                await firebaseClient
                    .Child("News")
                    .PostAsync(newsItem);

                await DisplayAlert("Успех", "Новость успешно добавлена", "Ок");
                // Очистка полей ввода
                title1.Text = string.Empty;
                imgsrc.Text = string.Empty;
                descTExt.Text = string.Empty;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Ошибка при добавлении новости: {ex.Message}", "Ок");
            }
        }
    }
}
