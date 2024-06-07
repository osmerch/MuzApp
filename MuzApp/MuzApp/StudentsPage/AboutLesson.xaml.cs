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
        public bool edited = true;
        public int id_lesson;
        public AboutLesson (Lesson lesson)
        {
			InitializeComponent (); 
			this.BindingContext = lesson;
            id_lesson = lesson.LessonId;
        }
	}
}