using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MuzApp.DbTables;

namespace MuzApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegPage : ContentPage
    {
        public RegPage()
        {
            InitializeComponent();

        }

        private async void Reg2_Clicked(object sender, EventArgs e)
        {
            var db = new DB();
            Random rnd = new Random();
            int RandId = rnd.Next(1000, 9999);

            if (Pas.Text == PasRep.Text && Log.Text != null && Pas.Text != null && EmailText.Text != null && NameText.Text != null && SurnameText.Text != null)
            {
                int a = 0;

                List<UserDataAuth> MyItems = new List<UserDataAuth>();

                foreach (var item in MyItems)
                {
                    while (item.UserId == RandId)
                    {
                        RandId = rnd.Next(1000, 9999);
                    }
                }
                foreach (var item in MyItems)
                {
                    if ((item.Login == Log.Text))
                    {
                        await DisplayAlert("Ошибка", "Такой логин уже используется", "Ок");
                        a = 1;
                        break;
                    }
                }
                if (a != 1)
                {
                    string login = Log.Text.Trim();
                    string password = Pas.Text.Trim();
                    string email = EmailText.Text.Trim();
                    string name = NameText.Text.Trim();
                    string surname = SurnameText.Text.Trim();

                    UserDataAuth user = new UserDataAuth()
                    {
                        UserId = RandId,
                        Login = login,
                        Password = password,
                        RoleId = 2,
                    };
                    Admin admin = new Admin()
                    {
                        UserId = RandId,
                        Email = email,
                        Name = name,
                        Surname = surname
                    };

                    await db.AddUserData(user);
                    await db.AddAdmin(admin);
                    Log.Text = "";
                    Pas.Text = "";
                    NameText.Text = "";
                    SurnameText.Text = "";
                    EmailText.Text = "";
                    await DisplayAlert("Успешно", "Вы зарегистрированы", "Ок");
                }

            }
            else if (Pas.Text != PasRep.Text | Log.Text == null | Pas.Text == null | SurnameText.Text == null | NameText.Text == null | EmailText.Text == null)
            {
                await DisplayAlert("Ошибка", "Проверьте данные", "Ок");
            }
        }

        private async void EnterBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Auth());
        }
    }
}