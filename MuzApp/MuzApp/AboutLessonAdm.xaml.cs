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
    public partial class AboutLessonAdm : ContentPage
    {
        public bool edited = true;
        public int id_lesson;
        public AboutLessonAdm(Lesson lesson)
        {
            InitializeComponent();
            this.BindingContext = lesson;
            id_lesson = lesson.LessonId;
        }

        private async void DeleteBtn_Clicked(object sender, EventArgs e)
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