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

namespace MuzApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutLessonAdm : ContentPage
    {
        public bool edited = true;
        public int id_lesson;
        public AboutLessonAdm(string courseName, string teacherName, string startTime, string endTime, string date, string teacherDesc, string courseDesc)
        {
            InitializeComponent();
            CourseNameLabel.Text = courseName;
            TeacherNameLabel.Text = teacherName;
            StartTimeLabel.Text = startTime;
            EndTimeLabel.Text = endTime;
            DateLabel.Text = date;
            TeacherDescLabel.Text = teacherDesc;
            CourseDescLabel.Text = courseDesc;
        }
        private  void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            //var lesson = App.Db.GetLesson(id_lesson);
            //if (lesson != null)
            //{
            //    App.Db.DeleteBook(id_lesson);
            //    await Navigation.PopAsync();
                
            //}
            //else
            //    await DisplayAlert("Ошибка", "Проблемы с удалением", "Хорошо");
        }
    }
}