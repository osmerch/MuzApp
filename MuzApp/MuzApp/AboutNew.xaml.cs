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
    public partial class AboutNew : ContentPage
    {
        public bool edited = true;
        public int id_news;
        public AboutNew(New n)
        {
            InitializeComponent();
            this.BindingContext = n;
            id_news = n.NewId;
        }

        private async void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            //var n = App.Db.GetNew(id_news);
            //if (n != null)
            //{
            //    App.Db.DeleteNew(id_news);
            //    await Navigation.PopAsync();
            //}
            //else
            //    await DisplayAlert("Ошибка", "Проблемы с удалением", "Хорошо");
        }
    }
}