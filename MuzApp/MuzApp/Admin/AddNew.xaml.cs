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
    public partial class AddNew : ContentPage
    {
        public AddNew()
        {
            InitializeComponent();
        }

        private void AddBtn_Clicked(object sender, EventArgs e)
        {
            //string title = title1.Text.Trim();
            //string desk = descTExt.Text.Trim();
            //string img = imgsrc.Text.Trim();
            //DateTime date = DateTime.Now;
            //DateTime time = DateTime.Now;

            //New n = new New()
            //{
            //    Title = title,
            //    Description = desk,
            //    Date = date.ToShortDateString(),
            //    Time = time.ToShortTimeString(),
            //    Image = img
            //};
            //App.Db.SaveNew(n);
            //title1.Text = "";
            //descTExt.Text = "";
            //imgsrc.Text = "";
        }
    }
}