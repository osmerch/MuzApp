using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MuzApp.DbTables;
using MuzApp.Reg;
using Firebase.Database;
using Xamarin.Essentials;

namespace MuzApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegPage : ContentPage
    {
        private readonly FirebaseClient firebaseClient;
        public RegPage()
        {
            InitializeComponent();
            firebaseClient = new FirebaseClient("https://muzicschool-f7f69-default-rtdb.firebaseio.com/");
            Task.Run(AnimateBack);
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

        private async void Reg2_Clicked(object sender, EventArgs e)
        {
            try
            {
                Random rnd = new Random();
                int RandId = rnd.Next(1000, 9999);
                string name = NameText.Text.Trim();
                string surname = SurnameText.Text.Trim();
                if (!string.IsNullOrWhiteSpace(NameText.Text) && !string.IsNullOrWhiteSpace(SurnameText.Text))
                {
                    var users = await GetAllUsersAsync();
                    if (users.Any(user => user.UserId == RandId))
                    {
                        RandId = rnd.Next(1000, 9999);
                    }
                    else
                    {
                        await Navigation.PushAsync(new RegPage2(RandId, name, surname));
                    } 
                }
                else
                {
                    await DisplayAlert("Ошибка", "Проверьте данные", "Ок");
                }                
            }
            catch
            {
                await DisplayAlert("Ошибка", "Проверьте данные", "Ок");
            }
        }
        private async Task<List<UserDataAuth>> GetAllUsersAsync()
        {
            return (await firebaseClient
                .Child("UsersData")
                .OnceAsync<UserDataAuth>())
                .Select(item => new UserDataAuth
                {
                    UserId = item.Object.UserId,
                    // другие поля, если необходимо
                })
                .ToList();
        }
        private async void EnterBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Auth());
        }
    }
}