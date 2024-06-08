using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;


namespace MuzApp
{
    public partial class App : Application
    {
        //private static DB db;
        //public static DB Db
        //{
        //    get
        //    {
        //        if (db == null)
        //            db = new DB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "db.sqlite3"));
        //        return db;
        //    }
        //}
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
