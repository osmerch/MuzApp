using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MuzApp.DbTables;

namespace MuzApp.AdminPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTeacher : ContentPage
    {
        FirebaseClient firebaseClient;

        public AddTeacher()
        {
            InitializeComponent();
            firebaseClient = new FirebaseClient("https://muzicschool-f7f69-default-rtdb.firebaseio.com/");
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            string name = NameEntry.Text;
            string surname = SurnameEntry.Text;
            string email = EmailEntry.Text;
            string description = DescriptionEntry.Text;
            string password = PasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(description) ||
                string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, заполните все поля", "OK");
                return;
            }

            var existingUser = (await firebaseClient
         .Child("UsersData")
         .OnceAsync<UserDataAuth>())
         .FirstOrDefault(x => x.Object.Email == email);

            if (existingUser != null)
            {
                await DisplayAlert("Ошибка", "Пользователь с такой почтой уже существует", "OK");
                return;
            }

            var teacher = new Teacher
            {
                UserId = new Random().Next(1, int.MaxValue), // Генерация случайного идентификатора пользователя
                Name = name,
                Surname = surname,
                Email = email,
                Desc = description
            };

            var userData = new UserDataAuth
            {
                UserId = teacher.UserId,
                Email = email,
                Password = password,
                RoleId = 3
            };

            await firebaseClient
                .Child("Teacher")
                .Child(teacher.UserId.ToString())
                .PutAsync(teacher);

            await firebaseClient
                .Child("UsersData")
                .Child(userData.UserId.ToString())
                .PutAsync(userData);

            await DisplayAlert("Успех", "Учитель успешно добавлен", "OK");
            await Navigation.PopAsync();
        }
    }
}