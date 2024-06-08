using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Database;
using Firebase.Database.Query;
using static MuzApp.DbTables;

namespace MuzApp.Reg
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegPage3 : ContentPage
    {
        private int IdStudent;
        private string NameStudent;
        private string SurameStudent;
        DateTime dateStudent;

        private readonly FirebaseClient firebaseClient;

        public RegPage3(int id, string Name, string Surname, DateTime date)
        {
            InitializeComponent();
            IdStudent = id;
            NameStudent = Name;
            SurameStudent = Surname;
            dateStudent = date;
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
                if (!string.IsNullOrWhiteSpace(EmailText.Text))
                {
                    string email = EmailText.Text.Trim();
                    var users = await GetAllUsersAsync();

                    if (users.Any(user => user.Email == email))
                    {
                        await DisplayAlert("Ошибка", "Такая почта уже зарегистрирована", "Ок");
                    }
                    else
                    {
                        await Navigation.PushAsync(new RegPage4(IdStudent, NameStudent, SurameStudent, dateStudent, email));
                    }
                }
                else
                {
                    await DisplayAlert("Ошибка", "Проверьте данные", "Ок");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Ошибка подключения Firebase. Проверьте связь с сервером: {ex.Message}", "Ок");
            }
        }

        private async Task<List<UserDataAuth>> GetAllUsersAsync()
        { 
            var users = await firebaseClient
                .Child("UsersData")
                .OnceAsync<UserDataAuth>();

            return users.Select(item => new UserDataAuth
            {
                Email = item.Object.Email
                // другие поля, если необходимо
            }).ToList();
        }
    }
}