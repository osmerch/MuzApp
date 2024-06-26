using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MuzApp.DbTables;

namespace MuzApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        private int userId;
        private UserDataAuth userData;
        private Teacher teacherData;
        private Student studentData;

        public Profile(int Id)
        {
            InitializeComponent();
            userId = Id;
            LoadUserData(Id);
        }

        private async void LoadUserData(int Id)
        {
            try
            {
                var db = new DB();
                userData = await db.GetUserDataById(Id);

                if (userData == null)
                {
                    await DisplayAlert("Ошибка", "Пользователь не найден", "Ок");
                    return;
                }

                if (userData.RoleId == 1)
                {
                    studentData = await db.GetStudentById(Id);
                    if (studentData != null)
                    {
                        NameLabel.Text = studentData.Name;
                        SurnameLabel.Text = studentData.Surname;
                        RoleLabel.Text = "Student";
                    }
                    else
                    {
                        await DisplayAlert("Ошибка", "Данные студента не найдены", "Ок");
                    }
                }
                else if (userData.RoleId == 3)
                {
                    teacherData = await db.GetTeacherById(Id);
                    if (teacherData != null)
                    {
                        NameLabel.Text = teacherData.Name;
                        SurnameLabel.Text = teacherData.Surname;
                        RoleLabel.Text = "Teacher";
                    }
                    else
                    {
                        await DisplayAlert("Ошибка", "Данные учителя не найдены", "Ок");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось загрузить данные пользователя: {ex.Message}", "Ок");
            }
        }

        private async void OnEditProfileClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new EditProfilePage(userId));
        }

        private async void OnSchoolInfoClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new SchoolInfoPage());
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Успех", "Вы успешно вышли из системы", "Ок");
            await Navigation.PushAsync(new Auth());
        }
    }
}