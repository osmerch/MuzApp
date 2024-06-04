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
        private DateTime _selectedDate;
        string date = "18.03.2024";
        public Lesson SelectedLesson { get; set; }
        public AdminLessonPage()
        {
            InitializeComponent();
            this.BindingContext = this;
            CreateCalendar();
        }
        private void CreateCalendar()
        {
            DateTime startdate = DateTime.Today.AddDays(-1);
            DateTime enddate = DateTime.Today.AddDays(30);
            
            foreach (var date in Enumerable.Range(0, (enddate- startdate).Days + 1).Select(offset => startdate.AddDays(offset))) 
            {
                Button dateBtn = new Button
                {
                    CornerRadius = 10,
                    Margin = 5,
                    WidthRequest = 67,
                    FontSize = 12,
                    Text = date.ToString("MMM dd"),
                    BindingContext = date,
                    BackgroundColor = date == DateTime.Today ? Color.LightBlue : Color.Default,
                    TextColor = Color.White
                };
                dateBtn.Clicked += DateBtn_Clicked;
                HorizontalClendar.Children.Add(dateBtn);

                if (date == DateTime.Today)
                {
                    _selectedDate = date;
                    Device.BeginInvokeOnMainThread(() => dateBtn.Focus());
                }
                foreach (Button btn in HorizontalClendar.Children)
                {
                    DateTime btnDate = (DateTime)btn.BindingContext;
                    if (btnDate == DateTime.Today)
                    {
                        btn.BackgroundColor = Color.LightBlue;
                    }
                    else if (btnDate == _selectedDate)
                    {
                        btn.BackgroundColor = Color.LightGreen;
                    }
                    else
                    {
                        btn.BackgroundColor = Color.DarkKhaki;
                    }
                }

            }
        }

        private void DateBtn_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            DateTime selectedDate = (DateTime)button.BindingContext;
            _selectedDate = selectedDate;

            foreach(Button btn in HorizontalClendar.Children)
            {
                DateTime btnDate = (DateTime)btn.BindingContext;
                if(btnDate == DateTime.Today)
                {
                    btn.BackgroundColor = Color.LightBlue;
                }
                else if(btnDate == _selectedDate) 
                {
                    btn.BackgroundColor = Color.LightGreen;
                }
                else
                {
                    btn.BackgroundColor = Color.DarkKhaki;
                }
            }
            testLabel.Text = selectedDate.ToString("dddd");
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
            //date = "22.03.2024";
            //List<Lesson> allLessons = App.Db.GetLessons();
            //List<Lesson> lessonsOnTargetDate = allLessons.Where(lesson => lesson.Date == date).ToList();
            //ItemColl.ItemsSource = lessonsOnTargetDate;

        }

        private void Btn5_Clicked(object sender, EventArgs e)
        {
            //date = "23.03.2024";
            //List<Lesson> allLessons = App.Db.GetLessons();
            //List<Lesson> lessonsOnTargetDate = allLessons.Where(lesson => lesson.Date == date).ToList();
            //ItemColl.ItemsSource = lessonsOnTargetDate;

        }
    }
}