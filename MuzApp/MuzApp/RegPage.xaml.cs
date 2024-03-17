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
            if (Pas.Text == PasRep.Text && Log.Text != null && Pas.Text != null && EmailText.Text != null && NameText.Text != null && SurnameText.Text != null)
            {
                int a = 0;


                List<User> MyItems = new List<User>();

                MyItems = (List<User>)App.Db.GetUsers();
                foreach (var item in MyItems)
                {
                    if ((item.Login == Log.Text))
                    {

                        await DisplayAlert("Ошибка", "Такой аккаунт уже существует", "Ок");
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

                    User user = new User()
                    {
                        Login = login,
                        Password = password,
                        Email = email,
                        Name = name,
                        RoleId = 1,
                        Surname = surname
                    };

                    App.Db.SaveUser(user);

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