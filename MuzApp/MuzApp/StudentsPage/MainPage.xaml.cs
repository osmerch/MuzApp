using MuzApp.AdminPages;
using MuzApp.StudentsPage;
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
    public partial class MainPage : TabbedPage
    {
        public int idStudent;
        public MainPage(int id)
        {
            idStudent = id;
            InitializeComponent();

            var lessonpage = new LessonsPage(idStudent);
            var coursespage = new AllCoursesPage(idStudent);
            //var profilepage = new Profile(idStudent);

            Children.Add(new NavigationPage(lessonpage) { Title = "Расписание"});
            Children.Add(new NavigationPage(coursespage) { Title = "Курсы"}); 
            //Children.Add(new NavigationPage(profilepage) { Title = "Профиль" });
        }
    }
}