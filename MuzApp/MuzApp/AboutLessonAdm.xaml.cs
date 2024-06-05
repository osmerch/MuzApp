using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MuzApp.DbTables;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Database;
using static MuzApp.AdminLessonPage;
using Firebase.Database.Query;

namespace MuzApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutLessonAdm : ContentPage
    {
        private FirebaseClient firebaseClient;
        public bool edited = true;
        public int id_lesson;
        public AboutLessonAdm(string courseName, string teacherName, string startTime, string endTime, string date, string teacherDesc, string courseDesc, int LessonId)
        {
            InitializeComponent();
            CourseNameLabel.Text = courseName;
            TeacherNameLabel.Text = teacherName;
            StartTimeLabel.Text = startTime;
            EndTimeLabel.Text = endTime;
            DateLabel.Text = date;
            TeacherDescLabel.Text = teacherDesc;
            CourseDescLabel.Text = courseDesc;

            id_lesson = LessonId;
            firebaseClient = new FirebaseClient("https://muzicschool-f7f69-default-rtdb.firebaseio.com/");

        }
        private async void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            var confirm = await DisplayAlert("Подтверждение", "Вы уверены, что хотите удалить это занятие?", "Да", "Нет");
            if (confirm)
            {
                await DeleteLesson(id_lesson);
                await DisplayAlert("Успех", "Занятие удалено", "Ок");
                await Navigation.PopAsync(); // Возвращаемся на предыдущую страницу
            }
        }

        private async void CancelBtn_Clicked(object sender, EventArgs e)
        {
            var confirm = await DisplayAlert("Подтверждение", "Вы уверены, что хотите отменить это занятие?", "Да", "Нет");
            if (confirm)
            {
                await UpdateLessonStatus(id_lesson, "отменено");
                await DisplayAlert("Успех", "Статус занятия обновлен на 'отменено'", "Ок");
                await Navigation.PopAsync(); // Возвращаемся на предыдущую страницу
            }
        }
        private async Task DeleteLesson(int lessonId)
        {
            var lessonToDelete = (await firebaseClient
                .Child("Lesson")
                .OnceAsync<Lesson>()).FirstOrDefault(a => a.Object.LessonId == lessonId);

            if (lessonToDelete != null)
            {
                await firebaseClient.Child("Lesson").Child(lessonToDelete.Key).DeleteAsync();
            }
        }

        private async Task UpdateLessonStatus(int lessonId, string newStatus)
        {
            var lessonToUpdate = (await firebaseClient
                .Child("Lesson")
                .OnceAsync<Lesson>()).FirstOrDefault(a => a.Object.LessonId == lessonId);

            if (lessonToUpdate != null)
            {
                var lesson = lessonToUpdate.Object;
                lesson.Status = newStatus;
                await firebaseClient.Child("Lesson").Child(lessonToUpdate.Key).PutAsync(lesson);
            }
        }
    }
}