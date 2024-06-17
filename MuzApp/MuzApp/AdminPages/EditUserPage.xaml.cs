using Firebase.Database;
using Firebase.Database.Query;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MuzApp.DbTables;

namespace MuzApp.AdminPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUserPage : ContentPage
    {
        FirebaseClient firebaseClient;
        private UserDataAuth userData;
        private bool isTeacher;

        public EditUserPage(UserDataAuth userData, bool isTeacher)
        {
            InitializeComponent();
            firebaseClient = new FirebaseClient("https://muzicschool-f7f69-default-rtdb.firebaseio.com/");
            this.userData = userData;
            this.isTeacher = isTeacher;

            LoadUserData();
        }

        private async void LoadUserData()
        {
            EmailEntry.Text = userData.Email;
            PasswordEntry.Text = userData.Password;

            if (isTeacher)
            {
                var teacher = await firebaseClient
                    .Child("Teacher")
                    .Child(userData.UserId.ToString())
                    .OnceSingleAsync<Teacher>();

                NameEntry.Text = teacher.Name;
                SurnameEntry.Text = teacher.Surname;
            }
            else
            {
                var student = await firebaseClient
                    .Child("Student")
                    .Child(userData.UserId.ToString())
                    .OnceSingleAsync<Student>();

                NameEntry.Text = student.Name;
                SurnameEntry.Text = student.Surname;
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            userData.Email = EmailEntry.Text;
            userData.Password = PasswordEntry.Text;

            if (isTeacher)
            {
                var teacher = new Teacher
                {
                    UserId = userData.UserId,
                    Name = NameEntry.Text,
                    Surname = SurnameEntry.Text,
                    Email = EmailEntry.Text,
                    Desc = (await firebaseClient
                        .Child("Teacher")
                        .Child(userData.UserId.ToString())
                        .OnceSingleAsync<Teacher>()).Desc
                };

                await firebaseClient
                    .Child("Teacher")
                    .Child(teacher.UserId.ToString())
                    .PutAsync(teacher);
            }
            else
            {
                var student = new Student
                {
                    UserId = userData.UserId,
                    Name = NameEntry.Text,
                    Surname = SurnameEntry.Text,
                    DateOfBirth = (await firebaseClient
                        .Child("Student")
                        .Child(userData.UserId.ToString())
                        .OnceSingleAsync<Student>()).DateOfBirth
                };

                await firebaseClient
                    .Child("Student")
                    .Child(student.UserId.ToString())
                    .PutAsync(student);
            }

            await firebaseClient
                .Child("UsersData")
                .Child(userData.UserId.ToString())
                .PutAsync(userData);

            await DisplayAlert("Успех", "Данные пользователя успешно обновлены", "ОК");
            await Navigation.PopAsync();
        }
    }
}