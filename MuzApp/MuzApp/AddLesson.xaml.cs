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
        string timefrombtn = "";
        string datepic = "";
        public AddLesson()
        {
            InitializeComponent();
        }
        private void AddBtn_Clicked(object sender, EventArgs e)
        {
            string subject = title1.Text.Trim();
            string teacher = teacherText.Text.Trim();
            string date = datepic;
            if (timefrombtn != "")
            {
                Lesson lesson = new Lesson()
                {
                    Subject = subject,
                    TeacherName = teacher,
                    Date = date,
                    Time = timefrombtn
                };
                App.Db.SaveLesson(lesson);
                title1.Text = "";
                teacherText.Text = "";
            }
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