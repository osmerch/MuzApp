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
            List<User> Users = new List<User>();

            Users = (List<User>)App.Db.GetUsers();
            foreach (var u in Users)
            {
                if (u.Login == Log.Text && u.Password == Pas.Text && u.RoleId == 1)
                {
                    await Navigation.PushAsync(new MainPage(u.UserId));
                    return;
                }
                else if(u.Login == Log.Text && u.Password == Pas.Text && u.RoleId == 2)
                {
                    await Navigation.PushAsync(new AdminPage());
                    return;
                }
            }
            await DisplayAlert("Ошибка", "Ошибка данных", "Ок");
        }

        private async void Ren3_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegPage());
        }
    }
}