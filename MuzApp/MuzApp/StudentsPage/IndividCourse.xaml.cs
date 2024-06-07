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

        public IndividCourse()
        {
            InitializeComponent();
            firebaseClient = new FirebaseClient("https://muzicschool-f7f69-default-rtdb.firebaseio.com/");
            LoadIndividualCoursesAsync();
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
                        course.CourseId = -1;
                    }
                    return course;
                }).ToList();
        }

        private async void OnCourseSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Course selectedCourse)
            {
                await Navigation.PushAsync(new AboutCoursePage(selectedCourse));
            }
        }
    }
}