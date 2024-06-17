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
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace MuzApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddLesson : ContentPage
    {
        int _corseId = -1;
        DateTime dateLesson;
        string roomLessaon = "";
        TimeSpan starttimelesson;
        TimeSpan endtimelesson;
        int _TeachId = 0; 
        string timefrombtn = "";
        string datepic = "";
        List<Course> courses;
        List<Teacher> teachers;
        List<Teacher_Course> teacher_Courses;
        FirebaseClient firebaseClient;
        List<WorkSchedule> workSchedules;
        public AddLesson()
        {
            InitializeComponent();
            firebaseClient = new FirebaseClient("https://muzicschool-f7f69-default-rtdb.firebaseio.com/");
            LoadDataAsync();

            datePic.MinimumDate = DateTime.Today;
            datePic.MaximumDate = DateTime.Today.AddDays(30);
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
                workSchedules = await GetAllAsync<WorkSchedule>("WorkSchedule");

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
                    _corseId = selectedCourse.CourseId;
                    var selectedCourseTeachers = teacher_Courses
                        .Where(ct => ct.CourseId == selectedCourse.CourseId)
                        .Select(ct => teachers.FirstOrDefault(t => t.UserId == ct.TeacherId))
                        .Where(t => t != null)
                        .ToList();

                    teacherPicker.ItemsSource = selectedCourseTeachers;
                    teacherPicker.SelectedIndex = -1;

                    teacherPicker.SelectedIndexChanged += OnTeacherSelected;

                }
            }
            catch (Exception ex)
            {
                 DisplayAlert("Ошибка", ex.Message, "Ок");
            }
        }

        private void OnTeacherSelected(object sender, EventArgs e)
        {
            UpdateAvailableTimeSlots();
        }

        private async void UpdateAvailableTimeSlots()
        {
            if (teacherPicker.SelectedIndex != -1 && datePic.Date != null)
            {
                dateLesson = datePic.Date;
                var selectedTeacher = (Teacher)teacherPicker.SelectedItem;
                var selectedDate = datePic.Date;
                var dayOfWeek = selectedDate.DayOfWeek;

                var teacherSchedule = workSchedules
                    .Where(ws => ws.TeacherId == selectedTeacher.UserId && ws.Day == dayOfWeek)
                    .ToList();
                _TeachId = selectedTeacher.UserId;

                var occupiedSlots = await GetOccupiedTimeSlots(selectedTeacher.UserId, selectedDate);

                timeSlotsStack.Children.Clear();

                foreach (var schedule in teacherSchedule)
                {
                    var currentTime = schedule.StartTime;
                   
                    while (currentTime < schedule.EndTime)
                    {
                        var endTime = currentTime.Add(TimeSpan.FromMinutes(50));
                        
                        if (!occupiedSlots.Contains(currentTime))
                        {
                            Button timeSlotButton = new Button
                            {
                                CornerRadius = 10,
                                Margin = 5,
                                WidthRequest = 67,
                                FontSize = 12,
                                Text = $"{currentTime:hh\\:mm} - {endTime:hh\\:mm}",
                                BindingContext = currentTime
                            };

                            timeSlotButton.Clicked += TimeSlotButton_Clicked;
                            timeSlotsStack.Children.Add(timeSlotButton);
                        }

                        currentTime = currentTime.Add(TimeSpan.FromMinutes(60)); 
                    }
                }
            }
        }

        private async Task<List<TimeSpan>> GetOccupiedTimeSlots(int teacherId, DateTime date)
        {
            var lessons = await GetAllAsync<Lesson>("Lesson");
            return lessons
                .Where(l => l.TeacherId == teacherId && l.Date.Date == date.Date)
                .Select(l => l.StartTime)
                .ToList();
        }

        
        private void TimeSlotButton_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var selectedTime = (TimeSpan)button.BindingContext;
            timeText.Text = selectedTime.ToString();
            starttimelesson = selectedTime;
            endtimelesson = selectedTime.Add(TimeSpan.FromMinutes(50));
        }

        private async void AddBtn_Clicked(object sender, EventArgs e)
        {
            var db = new DB();
            Random rnd = new Random();
            int RandId = rnd.Next(1000, 9999);

            if (_TeachId != 0  && starttimelesson != null && roomLessaon != null && dateLesson != null && endtimelesson != null && _corseId > -1)
            {

                int a = 0;

                List<Lesson> MyItems = new List<Lesson>();

                foreach (var item in MyItems)
                {
                    while (item.LessonId == RandId)
                    {
                        RandId = rnd.Next(10000, 99999);
                    }
                }
               
                if (a != 1)
                {                   
                    Lesson lesson = new Lesson()
                    {
                        LessonId = RandId,
                        TeacherId = _TeachId,
                        CourseId = _corseId,
                        Status = "будет",
                        StartTime = starttimelesson,
                        EndTime = endtimelesson,
                        Room = roomLessaon,
                        Date = dateLesson
                    };

                    try
                    {
                        await db.AddLesson(lesson);
                        await DisplayAlert("Успешно", "Вы зарегистрированы", "Ок");
                        await Navigation.PopAsync();
                    }
                    catch
                    {
                        await DisplayAlert("Ошибка", "Что-то с добавлением", "Ок");
                    }
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Проверьте данные", "Ок");
            }
        }
    
        private void datePic_DateSelected(object sender, DateChangedEventArgs e)
        {
            datepic = e.NewDate.ToString("dd/MM/yyyy");
            if (teacherPicker.SelectedIndex != -1)
            {
                Teacher selectedTeacher = (Teacher)teacherPicker.SelectedItem;
            }
            UpdateAvailableTimeSlots();
        }

        public async void AddTestWorkSchedules()
        {
            var db = new DB();

            var testSchedules = new List<WorkSchedule>
            {
                new WorkSchedule
                {
                    TeacherId = 3034, 
                    Day = DayOfWeek.Tuesday,
                    StartTime = new TimeSpan(10, 0, 0),
                    EndTime = new TimeSpan(14, 0, 0)
                },
                new WorkSchedule
                {
                    TeacherId = 3034, 
                    Day = DayOfWeek.Wednesday,
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(12, 0, 0)
                },
            };

            foreach (var schedule in testSchedules)
            {
                await db.AddWorkSchedule(schedule);
            }
        }
        private void AudPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            roomLessaon = AudPicker.SelectedItem.ToString();
        }
    }
}