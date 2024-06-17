using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;


namespace MuzApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Device.SetFlags(new[] { "Brush_Experimental" });
            MainPage = new Auth();

            MainPage = new CustomNavigationPage(new Auth());

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
