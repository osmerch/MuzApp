using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MuzApp.DbTables;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MuzApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddLesson : ContentPage
    {
        public AddLesson()
        {
            InitializeComponent();
        }
        private void AddBtn_Clicked(object sender, EventArgs e)
        {
            string subject = title1.Text.Trim();
            string teacher = teacherText.Text.Trim();
            DateTime date = DateTime.Now;
            DateTime time = DateTime.Now;

            Lesson lesson = new Lesson()
            {
                Subject = subject,
                TeacherName = teacher,
                Date = date.ToShortDateString(),
                Time = time.ToShortTimeString()
            };
            App.Db.SaveLesson(lesson);
            title1.Text = "";
            teacherText.Text = "";
        }
    }
}