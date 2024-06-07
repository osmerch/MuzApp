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
    public partial class Auth : ContentPage
    {
        public Auth()
        {
            InitializeComponent();
           
        }
        private async void Ren2_Clicked(object sender, EventArgs e)
        {
            List<UserDataAuth> Users = new List<UserDataAuth>();
            var db = new DB();
            try
            {
                Users = await db.GetAllUsersData();
                foreach (var u in Users)
                {
                    if (u.Email == Log.Text && u.Password == Pas.Text && u.RoleId == 1)
                    {
                        await Navigation.PushAsync(new MainPage(u.UserId));
                        return;
                    }
                    else if (u.Email == Log.Text && u.Password == Pas.Text && u.RoleId == 2)
                    {
                        await Navigation.PushAsync(new AdminPage());
                        return;
                    }
                }
            }
            catch
            {
                await DisplayAlert("Ошибка", "Ошибка firebase", "Ок");
            }
        }

        private async void Ren3_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegPage());
        }

        private async void Ren4_Clicked(object sender, EventArgs e)
        {
            using (var fb = new FirebaseClient("https://muzicschool-f7f69-default-rtdb.firebaseio.com/"))
            {
                var result = await fb.Child("test").OnceSingleAsync<string>();
                await DisplayAlert("title", result, "OK");
            }
        }
    }
}