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
    public partial class AdminLessonPage : ContentPage
    {
        string date = "18.03.2024";
        public Lesson SelectedLesson { get; set; }
        public AdminLessonPage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }
        protected override void OnAppearing()
        {
            ShowItems();
        }
        void ShowItems()
        {
            List<Lesson> allLessons = App.Db.GetLessons();
            List<Lesson> lessonsOnTargetDate = allLessons.Where(lesson => lesson.Date == date).ToList(); 
            ItemColl.ItemsSource = lessonsOnTargetDate;
        }

        private async void AddLesson_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddLesson());
        }

        private async void ItemColl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Navigation.PushAsync(new AboutLessonAdm(SelectedLesson));
        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            date = "18.03.2024";
            ShowItems();


        }

        private void Btn1_Clicked(object sender, EventArgs e)
        {
            date = "19.03.2024";
            ShowItems();

        }

        private void Btn2_Clicked(object sender, EventArgs e)
        {
            date = "20.03.2024";
            ShowItems();


        }

        private void Btn3_Clicked(object sender, EventArgs e)
        {
            date = "21.03.2024";
            ShowItems();


        }

        private void Btn4_Clicked(object sender, EventArgs e)
        {
            date = "22.03.2024";
            List<Lesson> allLessons = App.Db.GetLessons();
            List<Lesson> lessonsOnTargetDate = allLessons.Where(lesson => lesson.Date == date).ToList();
            ItemColl.ItemsSource = lessonsOnTargetDate;

        }

        private void Btn5_Clicked(object sender, EventArgs e)
        {
            date = "23.03.2024";
            List<Lesson> allLessons = App.Db.GetLessons();
            List<Lesson> lessonsOnTargetDate = allLessons.Where(lesson => lesson.Date == date).ToList();
            ItemColl.ItemsSource = lessonsOnTargetDate;

        }
    }
}