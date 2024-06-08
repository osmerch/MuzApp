using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MuzApp.DbTables;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Database;
using Firebase.Database.Query;

namespace MuzApp.StudentsPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutCoursePage : ContentPage
    {
        private Course selectedCourse;
        public int idStudent;
        FirebaseClient firebaseClient;
        public AboutCoursePage(Course course, int id)
        {
            InitializeComponent();
            idStudent = id;
            selectedCourse = course;
            CourseNameTxt.Text = course.Name;
            DescCourseTxt.Text = course.Desc;
            SetBackgroundImage(course.ImageUrl);
            firebaseClient = new FirebaseClient("https://muzicschool-f7f69-default-rtdb.firebaseio.com/");
        }
        private void SetBackgroundImage(string imageUrl)
        {
            BackgroundImage.Source = ImageSource.FromUri(new Uri(imageUrl));
        }
        private async void GetBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                var studentCourse = new StudentCourse
                {
                    StudentId = idStudent,
                    CourseId = selectedCourse.CourseId
                };

                await firebaseClient
                    .Child("Student_Course")
                    .PostAsync(studentCourse);

                await DisplayAlert("Успех", "Вы успешно записались на курс", "Ок");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось записаться на курс: {ex.Message}", "Ок");
            }
        }
    }
}