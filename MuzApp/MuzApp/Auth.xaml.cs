using Firebase.Database;
using MuzApp.TeachersPages;
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
            var testSchedules = new List<WorkSchedule>
            {
               
                new WorkSchedule
                {
                    TeacherId = 891019304,
                    Day = DayOfWeek.Monday,
                    StartTime = TimeSpan.FromHours(10),
                    EndTime = TimeSpan.FromHours(15),
                },
                new WorkSchedule
                {
                    TeacherId = 891019304,
                    Day = DayOfWeek.Tuesday,
                    StartTime = TimeSpan.FromHours(9),
                    EndTime = TimeSpan.FromHours(17),
                },
                new WorkSchedule
                {
                    TeacherId = 891019304,
                    Day = DayOfWeek.Thursday,
                    StartTime = TimeSpan.FromHours(10),
                    EndTime = TimeSpan.FromHours(15),
                },
                new WorkSchedule
                {
                    TeacherId = 891019304,
                    Day = DayOfWeek.Saturday,
                    StartTime = TimeSpan.FromHours(10),
                    EndTime = TimeSpan.FromHours(15),
                },
                new WorkSchedule
                {
                    TeacherId = 891019304,
                    Day = DayOfWeek.Sunday,
                    StartTime = TimeSpan.FromHours(11),
                    EndTime = TimeSpan.FromHours(13),
                },
                new WorkSchedule
                {
                    TeacherId = 891019304,
                    Day = DayOfWeek.Wednesday,
                    StartTime = TimeSpan.FromHours(10),
                    EndTime = TimeSpan.FromHours(16),
                },
                 new WorkSchedule
                {
                    TeacherId = 757495946,
                    Day = DayOfWeek.Friday,
                    StartTime = TimeSpan.FromHours(10),
                    EndTime = TimeSpan.FromHours(15),
                },
                new WorkSchedule
                {
                    TeacherId = 757495946,
                    Day = DayOfWeek.Monday,
                    StartTime = TimeSpan.FromHours(10),
                    EndTime = TimeSpan.FromHours(15),
                },
                new WorkSchedule
                {
                    TeacherId = 757495946,
                    Day = DayOfWeek.Tuesday,
                    StartTime = TimeSpan.FromHours(9),
                    EndTime = TimeSpan.FromHours(17),
                },
                new WorkSchedule
                {
                    TeacherId = 757495946,
                    Day = DayOfWeek.Thursday,
                    StartTime = TimeSpan.FromHours(10),
                    EndTime = TimeSpan.FromHours(15),
                },
                new WorkSchedule
                {
                    TeacherId = 757495946,
                    Day = DayOfWeek.Saturday,
                    StartTime = TimeSpan.FromHours(9),
                    EndTime = TimeSpan.FromHours(15),
                },
                new WorkSchedule
                {
                    TeacherId = 757495946,
                    Day = DayOfWeek.Sunday,
                    StartTime = TimeSpan.FromHours(9),
                    EndTime = TimeSpan.FromHours(13),
                },
                new WorkSchedule
                {
                    TeacherId = 757495946,
                    Day = DayOfWeek.Wednesday,
                    StartTime = TimeSpan.FromHours(9),
                    EndTime = TimeSpan.FromHours(16),
                },
                new WorkSchedule
                {
                    TeacherId = 891019304,
                    Day = DayOfWeek.Friday,
                    StartTime = TimeSpan.FromHours(10),
                    EndTime = TimeSpan.FromHours(15),
                },
                new WorkSchedule
                {
                    TeacherId = 617990759,
                    Day = DayOfWeek.Friday,
                    StartTime = TimeSpan.FromHours(10),
                    EndTime = TimeSpan.FromHours(15),
                },
                new WorkSchedule
                {
                    TeacherId = 617990759,
                    Day = DayOfWeek.Monday,
                    StartTime = TimeSpan.FromHours(10),
                    EndTime = TimeSpan.FromHours(15),
                },
                new WorkSchedule
                {
                    TeacherId = 617990759,
                    Day = DayOfWeek.Tuesday,
                    StartTime = TimeSpan.FromHours(9),
                    EndTime = TimeSpan.FromHours(17),
                },
                new WorkSchedule
                {
                    TeacherId = 617990759,
                    Day = DayOfWeek.Thursday,
                    StartTime = TimeSpan.FromHours(10),
                    EndTime = TimeSpan.FromHours(15),
                },
                new WorkSchedule
                {
                    TeacherId = 617990759,
                    Day = DayOfWeek.Saturday,
                    StartTime = TimeSpan.FromHours(9),
                    EndTime = TimeSpan.FromHours(15),
                },
                new WorkSchedule
                {
                    TeacherId = 617990759,
                    Day = DayOfWeek.Sunday,
                    StartTime = TimeSpan.FromHours(9),
                    EndTime = TimeSpan.FromHours(13),
                },
                new WorkSchedule
                {
                    TeacherId = 617990759,
                    Day = DayOfWeek.Wednesday,
                    StartTime = TimeSpan.FromHours(9),
                    EndTime = TimeSpan.FromHours(16),
                },


            };

            foreach (var schedule in testSchedules)
            {
                await db.AddWorkSchedule(schedule);
            }
        }
        private async void Ren2_Clicked(object sender, EventArgs e)
        {

           
            if (string.IsNullOrWhiteSpace(Log.Text) || string.IsNullOrWhiteSpace(Pas.Text))
            {
                await DisplayAlert("Ошибка", "Введите логин и пароль", "Ок");
                return;
            }

            List<UserDataAuth> Users = new List<UserDataAuth>();
            var db = new DB();
            try
            {
                Users = await db.GetAllUsersData();
                foreach (var u in Users)
                {
                    if (u.Email == Log.Text && u.Password == Pas.Text)
                    {
                        if (u.RoleId == 1)
                        {
                            await Navigation.PushAsync(new MainPage(u.UserId));
                        }
                        else if (u.RoleId == 2)
                        {
                            await Navigation.PushAsync(new AdminPage());
                        }
                        else if (u.RoleId == 3)
                        {
                            await Navigation.PushAsync(new TeacherPage(u.UserId));
                        }
                        return;
                    }
                }
                await DisplayAlert("Ошибка", "Неверный логин или пароль", "Ок");
            }
            catch
            {
                await DisplayAlert("Ошибка", "Ошибка при получении данных", "Ок");
            }
        }

        private async void Ren3_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegPage());
        }
    }
}