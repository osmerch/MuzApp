using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MuzApp.DbTables;

namespace MuzApp.StudentsPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllCoursesPage : TabbedPage
    {
        public int idStudent;
        public AllCoursesPage(int id)
        {
            idStudent = id;
            InitializeComponent();

            var indpage = new IndividCourse(idStudent);
            var grouppage = new GroupCourse(idStudent);

            Children.Add(new NavigationPage(indpage) { Title = "Индивидуальные" });
            Children.Add(new NavigationPage(grouppage) { Title = "Групповые" });
        }
    }
}