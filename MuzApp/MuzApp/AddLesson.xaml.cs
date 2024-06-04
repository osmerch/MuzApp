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
using Newtonsoft.Json;

namespace MuzApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddLesson : ContentPage
    {
        string timefrombtn = "";
        string datepic = "";
        List<Course> courses;
        List<Teacher> teachers;
        List<Teacher_Course> teacher_Courses;
        FirebaseClient firebaseClient;
        public AddLesson()
        {
            InitializeComponent();
            firebaseClient = new FirebaseClient("https://muzicschool-f7f69-default-rtdb.firebaseio.com/");
            LoadDataAsync();
            datePic.MinimumDate = DateTime.Today;
            datePic.MaximumDate = DateTime.Today.AddDays(30);
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
                courses = await GetAllAsync<Course>("Course");
                teachers = await GetAllAsync<Teacher>("Teacher");
                teacher_Courses = await GetAllAsync<Teacher_Course>("Teacher_Course");
                               
                coursePicker.ItemsSource = courses;
                coursePicker.SelectedIndex = -1;
                               
                coursePicker.SelectedIndexChanged += OnCourseSelected;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "Ок");
            }
        }
        private void OnCourseSelected(object sender, EventArgs e)
        {
            try
            {
                if (coursePicker.SelectedIndex != -1)
                {
                    Course selectedCourse = (Course)coursePicker.SelectedItem;

                    var selectedCourseTeachers = teacher_Courses
                        .Where(ct => ct.CourseId == selectedCourse.CourseId)
                        .Select(ct => teachers.FirstOrDefault(t => t.UserId == ct.TeacherId))
                        .Where(t => t != null)
                        .ToList();

                    teacherPicker.ItemsSource = selectedCourseTeachers;
                    teacherPicker.SelectedIndex = -1; 

                }
            }
            catch (Exception ex)
            {
                 DisplayAlert("Ошибка", ex.Message, "Ок");
            }
        }
        private void AddBtn_Clicked(object sender, EventArgs e)
        {
            //string subject = title1.Text.Trim();
            //string teacher = teacherText.Text.Trim();
            //string date = datepic;
            //if (timefrombtn != "")
            //{
            //    Lesson lesson = new Lesson()
            //    {
            //        Subject = subject,
            //        TeacherName = teacher,
            //        Date = date,
            //        Time = timefrombtn
            //    };
            //    App.Db.SaveLesson(lesson);
            //    title1.Text = "";
            //    teacherText.Text = "";
            //}
        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            
            timefrombtn = "8:30";
            timeText.Text = timefrombtn;
        }

        private void Btn1_Clicked(object sender, EventArgs e)
        {
            
            timefrombtn = "9:30";
            timeText.Text = timefrombtn;


        }

        private void Btn2_Clicked(object sender, EventArgs e)
        {
            
            timefrombtn = "10:30";
            timeText.Text = timefrombtn;


        }

        private void Btn3_Clicked(object sender, EventArgs e)
        {
            
            timefrombtn = "11:30";
            timeText.Text = timefrombtn;

        }

        private void datePic_DateSelected(object sender, DateChangedEventArgs e)
        {
            datepic = e.NewDate.ToString("dd/MM/yyyy");
            dText.Text = datepic;
        }
    }
}