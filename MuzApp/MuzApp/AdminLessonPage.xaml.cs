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
            ItemColl.ItemsSource = App.Db.GetLessons();
        }

        private async void AddLesson_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddLesson());
        }

        private async void ItemColl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Navigation.PushAsync(new AboutLessonAdm(SelectedLesson));
        }
    }
}