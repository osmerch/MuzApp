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
        public async void AddTestWorkSchedules()
        {
            var db = new DB();
            var testSchedules = new List<Course>
            {
                new Course
                {
                    CourseId = 1,
                    Name = "Вокал",
                    Desc = "",
                    CourseType = "индивидуальные"
                },
                new Course
                {
                    CourseId = 2,
                    Name = "Синтезатор",
                    Desc = "",
                    CourseType = "индивидуальные"
                },
                new Course
                {
                    CourseId = 3,
                    Name = "Гитара",
                    Desc = "",
                    CourseType = "индивидуальные"
                },new Course
                {
                    CourseId = 4,
                    Name = "Электрогитара",
                    Desc = "",
                    CourseType = "индивидуальные"
                },
                new Course
                {
                    CourseId = 5,
                    Name = "Укулеле",
                    Desc = "",
                    CourseType = "индивидуальные"
                },
                new Course
                {
                    CourseId = 6,
                    Name = "Скрипка",
                    Desc = "",
                    CourseType = "индивидуальные"
                },
                new Course
                {
                    CourseId = 7,
                    Name = "Саксафон",
                    Desc = "",
                    CourseType = "индивидуальные"
                },
                new Course
                {
                    CourseId = 8,
                    Name = "Флейта",
                    Desc = "",
                    CourseType = "индивидуальные"
                },
                new Course
                {
                    CourseId = 9,
                    Name = "Фортепиано",
                    Desc = "",
                    CourseType = "индивидуальные"
                },
                new Course
                {
                    CourseId = 10,
                    Name = "Баян",
                    Desc = "",
                    CourseType = "индивидуальные"
                },
                new Course
                {
                    CourseId = 11,
                    Name = "Аккордеон",
                    Desc = "",
                    CourseType = "индивидуальные"
                },
            };

            foreach (var schedule in testSchedules)
            {
                await db.AddCourse(schedule);
            }
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
    }
}