using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MuzApp.DbTables;

namespace MuzApp.TeachersPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherPage : TabbedPage
    {
        public int idteacher;
        public TeacherPage (int id)
        {
            InitializeComponent();
             idteacher = id;
            var lessonpage = new TeacherLessonPage(idteacher);
            //var profilepage = new Profile(idteacher);
            Children.Add(new NavigationPage(lessonpage) { Title = "Расписание" });
            //Children.Add(new NavigationPage(profilepage) { Title = "Профиль" });
        }
    }
}