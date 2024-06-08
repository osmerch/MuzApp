using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MuzApp.DbTables;

namespace MuzApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutLesson : ContentPage
    {
        private FirebaseClient firebaseClient;
        public bool edited = true;
        public int id_lesson;
        public AboutLesson(string courseName, string teacherName, string startTime, string endTime, string date, string teacherDesc, string courseDesc, int LessonId)
        {
            InitializeComponent();
            CourseNameLabel.Text = courseName;
            TeacherNameLabel.Text = teacherName;
            StartTimeLabel.Text = startTime;
            EndTimeLabel.Text = endTime;
            DateLabel.Text = date;
            TeacherDescLabel.Text = teacherDesc;


            id_lesson = LessonId;
            firebaseClient = new FirebaseClient("https://muzicschool-f7f69-default-rtdb.firebaseio.com/");
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
    }
}