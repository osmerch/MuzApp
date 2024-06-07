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
    public partial class LessonsPage : ContentPage
    {
        
        public Lesson SelectedLesson { get; set; }
        public LessonsPage()
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
            //List<Lesson> allLessons = App.Db.GetLessons();
            //List<Lesson> lessonsOnTargetDate = allLessons.Where(lesson => lesson.Date == date).ToList();
            //ItemColl.ItemsSource = lessonsOnTargetDate;
        }

        private async void ItemColl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Navigation.PushAsync(new AboutLesson(SelectedLesson));
        }
    }
}