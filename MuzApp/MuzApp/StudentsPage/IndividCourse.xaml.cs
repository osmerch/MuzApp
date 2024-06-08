using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MuzApp.DbTables;

namespace MuzApp.StudentsPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IndividCourse : ContentPage
    {
        FirebaseClient firebaseClient;
        public int idStudent;
        public IndividCourse(int id)
        {
            InitializeComponent();
            idStudent = id;
            firebaseClient = new FirebaseClient("https://muzicschool-f7f69-default-rtdb.firebaseio.com/");
            LoadIndividualCoursesAsync();
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

        private async void LoadIndividualCoursesAsync()
        {
            try
            {
                var individualCourses = await GetIndividualCoursesAsync();
                ItemColl.ItemsSource = individualCourses;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Ошибка загрузки курсов: {ex.Message}", "Ок");
            }
        }

        private async Task<List<Course>> GetIndividualCoursesAsync()
        {
            var allCourses = await GetAllCoursesAsync();
            return allCourses.Where(course => course.CourseType == "индивидуальные").ToList();
        }

        private async Task<List<Course>> GetAllCoursesAsync()
        {
            return (await firebaseClient
                .Child("Course")
                .OnceAsync<Course>()).Select(item =>
                {
                    var course = item.Object;
                    if (int.TryParse(item.Key, out int courseId))
                    {
                        course.CourseId = courseId;
                    }
                    else
                    {
                        course.CourseId = courseId;
                    }
                    return course;
                }).ToList();
        }

        private async void OnCourseSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Course selectedCourse)
            {
                await Navigation.PushAsync(new AboutCoursePage(selectedCourse, idStudent));
            }
        }
    }
}