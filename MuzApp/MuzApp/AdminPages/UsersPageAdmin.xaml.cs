using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using static MuzApp.DbTables;

namespace MuzApp.AdminPages
{
    public partial class UsersPageAdmin : ContentPage
    {
        FirebaseClient firebaseClient;
        List<Student> students;
        List<UserDataAuth> userDataAuths;
        List<Teacher> teachers;

        public UsersPageAdmin()
        {
            InitializeComponent();
            firebaseClient = new FirebaseClient("https://muzicschool-f7f69-default-rtdb.firebaseio.com/");
            LoadDataAsync();
        }

        private async Task<List<T>> GetAllAsync<T>(string childPath)
        {
            return (await firebaseClient
                .Child(childPath)
                .OnceAsync<T>()).Select(item =>
                {
                    var obj = item.Object;
                    var prop = obj.GetType().GetProperty("Key");
                    if (prop != null)
                    {
                        prop.SetValue(obj, item.Key);
                    }
                    return obj;
                }).ToList();
        }

        private async void LoadDataAsync()
        {
            try
            {
                students = await GetAllAsync<Student>("Student");
                userDataAuths = await GetAllAsync<UserDataAuth>("UsersData");
                teachers = await GetAllAsync<Teacher>("Teacher");

                var userViewModels = new List<UserViewModel>();

                foreach (var student in students)
                {
                    var authData = userDataAuths.FirstOrDefault(u => u.UserId == student.UserId);
                    if (authData != null)
                    {
                        userViewModels.Add(new UserViewModel
                        {
                            Name = student.Name + " " + student.Surname,
                            Surname = student.Surname,
                            Email = authData.Email,
                            Role = "Студент"
                        });
                    }
                }

                foreach (var teacher in teachers)
                {
                    userViewModels.Add(new UserViewModel
                    {
                        Name = teacher.Name + " " + teacher.Surname,
                        Surname = teacher.Surname,
                        Email = teacher.Email,
                        Role = "Учитель"
                    });
                }

                ItemColl.ItemsSource = userViewModels;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }

        public class UserViewModel
        {
            public int UserId { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        }
        private async void OnLessonSelected(object sender, SelectionChangedEventArgs e)
        {
            //if (e.CurrentSelection.FirstOrDefault() is UserViewModel selectedUser)
            //{
            //    var userData = await firebaseClient
            //        .Child("UsersData")
            //        .Child(selectedUser.UserId.ToString())
            //        .OnceSingleAsync<UserDataAuth>();

            //    bool isTeacher = selectedUser.Role == "Учитель";

            //    await Navigation.PushAsync(new EditUserPage(userData, isTeacher));
            //}
        }

        private async void AddLesson_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTeacher());
        }
    }
}