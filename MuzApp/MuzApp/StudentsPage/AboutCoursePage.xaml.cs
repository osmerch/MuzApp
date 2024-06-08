using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MuzApp.DbTables;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MuzApp.StudentsPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutCoursePage : ContentPage
    {
        private Course selectedCourse;
        public AboutCoursePage(Course course)
        {
            InitializeComponent();
            selectedCourse = course;
            CourseNameTxt.Text = course.Name;
            DescCourseTxt.Text = course.Desc;
            SetBackgroundImage(course.ImageUrl);
        }
        private void SetBackgroundImage(string imageUrl)
        {
            BackgroundImage.Source = ImageSource.FromUri(new Uri(imageUrl));
        }
        private void GetBtn_Clicked(object sender, EventArgs e)
        {

        }
    }
}