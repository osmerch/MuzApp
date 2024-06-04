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

        public async void TeacherReg()
        {
            try
            {
                var db = new DB();
                Random rnd = new Random();
                int RandId = rnd.Next(1000, 9999);
                int a = 0;

                List<UserDataAuth> MyItems = new List<UserDataAuth>();

                foreach (var item in MyItems)
                {
                    while (item.UserId == RandId)
                    {
                        RandId = rnd.Next(1000, 9999);
                    }
                }
                string login = "log1";
                string password = "123";
                string email = "ellie_greys@mail.ru";
                string name = "Эльгина";
                string surname = "Мирзаянова";
                string desc = "Лучший учитель музыки!!";

                string cursname = "Вокал";
                string desccours = "Здесь вы научитесь пению..";

                UserDataAuth user = new UserDataAuth()
                {
                    UserId = RandId,
                    Login = login,
                    Password = password,
                    RoleId = 3,
                };
                Teacher teacher = new Teacher()
                {
                    UserId = RandId,
                    Email = email,
                    Name = name,
                    Surname = surname,
                    Desc = desc
                };
                Course course = new Course()
                {
                    Name = cursname,
                    Desc = desccours
                };
                Teacher_Course tc = new Teacher_Course()
                {
                    CourseId = 0,
                    TeacherId = RandId
                };
                await db.AddCourse(course);
                await db.AddUserData(user);
                await db.AddTeacher(teacher);
                await db.AddTeacher_Course(tc);
            }
            catch
            {
                await DisplayAlert("Ошибка", "Ошибка firebase", "Ок");
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
                    if (u.Login == Log.Text && u.Password == Pas.Text && u.RoleId == 1)
                    {
                        await Navigation.PushAsync(new MainPage(u.UserId));
                        return;
                    }
                    else if (u.Login == Log.Text && u.Password == Pas.Text && u.RoleId == 2)
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