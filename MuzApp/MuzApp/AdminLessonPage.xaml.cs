﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MuzApp.DbTables;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Database;

namespace MuzApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminLessonPage : ContentPage
    {
        private DateTime _selectedDate;
        FirebaseClient firebaseClient;
        List<Course> courses;
        List<Teacher> teachers;
        List<Lesson> lessons;

        public Lesson SelectedLesson { get; set; }
        public AdminLessonPage()
        {
            InitializeComponent();
            firebaseClient = new FirebaseClient("https://muzicschool-f7f69-default-rtdb.firebaseio.com/");
            this.BindingContext = this;
            CreateCalendar();
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
        private void CreateCalendar()
        {
            DateTime startdate = DateTime.Today.AddDays(-1);
            DateTime enddate = DateTime.Today.AddDays(30);
            
            foreach (var date in Enumerable.Range(0, (enddate- startdate).Days + 1).Select(offset => startdate.AddDays(offset))) 
            {
                Button dateBtn = new Button
                {
                    CornerRadius = 10,
                    Margin = 5,
                    WidthRequest = 67,
                    FontSize = 12,
                    Text = date.ToString("MMM dd"),
                    BindingContext = date,
                    BackgroundColor = date == DateTime.Today ? Color.LightBlue : Color.Default,
                    TextColor = Color.White
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
                        btn.BackgroundColor = Color.LightGreen;
                    }
                    else
                    {
                        btn.BackgroundColor = Color.DarkKhaki;
                    }
                }

            }
        }

        private void DateBtn_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            DateTime selectedDate = (DateTime)button.BindingContext;
            _selectedDate = selectedDate;

            foreach(Button btn in HorizontalClendar.Children)
            {
                DateTime btnDate = (DateTime)btn.BindingContext;
                if(btnDate == DateTime.Today)
                {
                    btn.BackgroundColor = Color.LightBlue;
                }
                else if(btnDate == _selectedDate) 
                {
                    btn.BackgroundColor = Color.LightGreen;
                }
                else
                {
                    btn.BackgroundColor = Color.DarkKhaki;
                }
            }
            testLabel.Text = selectedDate.ToString("dddd");
            LoadLessonsForDate(selectedDate);
        }
        private async void LoadDataAsync()
        {
            try
            {
                // Получение данных из Firebase
                courses = await GetAllAsync<Course>("Course");
                teachers = await GetAllAsync<Teacher>("Teacher");
                lessons = await GetAllAsync<Lesson>("Lesson");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }
        private void LoadLessonsForDate(DateTime date)
        {
            LoadDataAsync();
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
                    TeacherDesc = teachers.FirstOrDefault(teacher => teacher.UserId == lesson.TeacherId)?.Desc,
                    CourseDesc = courses.FirstOrDefault(course => course.CourseId == lesson.CourseId)?.Desc

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
            public string TeacherDesc { get; set; }
            public string CourseDesc { get; set; }
        }

        private async void AddLesson_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddLesson());
        }

   
        private async void OnLessonSelected(object sender, SelectionChangedEventArgs e)
        {
            //var selectedLesson = e.CurrentSelection.FirstOrDefault() as LessonViewModel;
            //if (selectedLesson != null)
            //{
            //    await Navigation.PushAsync(new AboutLessonAdm(
            //        Convert.ToInt32(selectedLesson.LessonID)));
            //}
            if (e.CurrentSelection.FirstOrDefault() is LessonViewModel selectedLesson)
            {
                await Navigation.PushAsync(new AboutLessonAdm(
                    selectedLesson.CourseName,
                    selectedLesson.TeacherName,
                    selectedLesson.StartTime,
                    selectedLesson.EndTime,
                    selectedLesson.Date,
                    selectedLesson.TeacherDesc,
                    selectedLesson.CourseDesc));
            }
            else
            {
                await DisplayAlert("Ошибка", "Проверьте данные", "Ок");
            }
        }
    }
}