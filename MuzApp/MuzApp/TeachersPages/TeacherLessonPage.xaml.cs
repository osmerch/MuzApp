using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MuzApp.DbTables;

namespace MuzApp.TeachersPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherLessonPage : ContentPage
    {
        private DateTime _selectedDate;
        private FirebaseClient firebaseClient;
        private List<Course> courses;
        private List<Teacher> teachers;
        private List<Lesson> lessons;
        public int teacherId;
        public Lesson SelectedLesson { get; set; }

        public TeacherLessonPage(int id)
        {
            InitializeComponent();
            teacherId = id;
            firebaseClient = new FirebaseClient("https://muzicschool-f7f69-default-rtdb.firebaseio.com/");
            this.BindingContext = this;
            CreateCalendar();
            LoadDataAsync(teacherId);
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

        private void CreateCalendar()
        {
            DateTime startdate = DateTime.Today.AddDays(-1);
            DateTime enddate = DateTime.Today.AddDays(30);

            foreach (var date in Enumerable.Range(0, (enddate - startdate).Days + 1).Select(offset => startdate.AddDays(offset)))
            {
                Button dateBtn = new Button
                {
                    CornerRadius = 10,
                    Margin = 5,
                    WidthRequest = 70,
                    FontSize = 11,
                    Text = date.ToString("MMM dd"),
                    BindingContext = date,
                    BackgroundColor = date == DateTime.Today ? Color.LightBlue : Color.Transparent,
                    TextColor = Color.White,
                    BorderColor = Color.White,
                    BorderWidth = 1
                };
                dateBtn.Clicked += DateBtn_Clicked;
                HorizontalClendar.Children.Add(dateBtn);

                if (date == DateTime.Today)
                {
                    _selectedDate = date;
                    Device.BeginInvokeOnMainThread(() => dateBtn.Focus());
                }
                foreach (Button btn in HorizontalClendar.Children)
                {
                    DateTime btnDate = (DateTime)btn.BindingContext;
                    if (btnDate == DateTime.Today)
                    {
                        btn.BackgroundColor = Color.LightBlue;
                    }
                    else if (btnDate == _selectedDate)
                    {
                        btn.BackgroundColor = Color.FromHex("#C9B0A1");
                    }
                    else
                    {
                        btn.BackgroundColor = Color.Transparent;
                    }
                }
            }
        }

        private void DateBtn_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            DateTime selectedDate = (DateTime)button.BindingContext;
            _selectedDate = selectedDate;

            foreach (Button btn in HorizontalClendar.Children)
            {
                DateTime btnDate = (DateTime)btn.BindingContext;
                if (btnDate == DateTime.Today)
                {
                    btn.BackgroundColor = Color.LightBlue;
                }
                else if (btnDate == _selectedDate)
                {
                    btn.BackgroundColor = Color.FromHex("#C9B0A1");
                }
                else
                {
                    btn.BackgroundColor = Color.Transparent;
                }
            }
            testLabel.Text = selectedDate.ToString("dddd");
            LoadLessonsForDate(selectedDate);
        }

        private async void LoadDataAsync(int teacherId)
        {
            try
            {
                courses = await GetAllAsync<Course>("Course");
                teachers = await GetAllAsync<Teacher>("Teacher");
                lessons = await GetAllAsync<Lesson>("Lesson");

                var teacherCourses = await GetAllAsync<Teacher_Course>("Teacher_Course");
                var teacherCourseIds = teacherCourses.Where(tc => tc.TeacherId == teacherId).Select(tc => tc.CourseId).ToList();

                lessons = lessons.Where(lesson => teacherCourseIds.Contains(lesson.CourseId)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }

        private void LoadLessonsForDate(DateTime date)
        {
            var selectedDateLessons = lessons
                .Where(lesson => lesson.Date.Date == date.Date)
                .Select(lesson => new LessonViewModel
                {
                    LessonID = lesson.LessonId,
                    CourseName = courses.FirstOrDefault(course => course.CourseId == lesson.CourseId)?.Name,
                    TeacherName = teachers.FirstOrDefault(teacher => teacher.UserId == lesson.TeacherId)?.Name + " " + teachers.FirstOrDefault(teacher => teacher.UserId == lesson.TeacherId)?.Surname,
                    StartTime = $"{lesson.StartTime:hh\\:mm}",
                    EndTime = $"{lesson.EndTime:hh\\:mm}",
                    Room = lesson.Room,
                    Date = $"{lesson.Date:D}",
                    Status = (lesson.Status == "отменено" || DateTime.Now > lesson.Date.Add(lesson.StartTime).AddMinutes(10)) ? "окончено" : "",
                    TeacherDesc = teachers.FirstOrDefault(teacher => teacher.UserId == lesson.TeacherId)?.Desc,
                    CourseDesc = courses.FirstOrDefault(course => course.CourseId == lesson.CourseId)?.Desc,
                    BackgroundColor = (lesson.Status == "отменено" || DateTime.Now > lesson.Date.Add(lesson.StartTime).AddMinutes(10)) ? Color.LightGray : Color.FromHex("#C9B0A1")
                })
                .ToList();

            ItemColl.ItemsSource = selectedDateLessons;
        }

        public class LessonViewModel
        {
            public int LessonID { get; set; }
            public string CourseName { get; set; }
            public string TeacherName { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public string Date { get; set; }
            public string Room { get; set; }
            public string Status { get; set; }
            public string TeacherDesc { get; set; }
            public string CourseDesc { get; set; }
            public Color BackgroundColor { get; set; }
        }

        private async void OnLessonSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is LessonViewModel selectedLesson)
            {
                await Navigation.PushAsync(new AboutLesson(
                    selectedLesson.CourseName,
                    selectedLesson.TeacherName,
                    selectedLesson.StartTime,
                    selectedLesson.EndTime,
                    selectedLesson.Date,
                    selectedLesson.TeacherDesc,
                    selectedLesson.CourseDesc,
                    selectedLesson.LessonID));
            }
            else
            {
                await DisplayAlert("Ошибка", "Проверьте данные", "Ок");
            }
        }
    }
}