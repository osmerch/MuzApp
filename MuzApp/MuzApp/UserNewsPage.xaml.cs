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
	public partial class UserNewsPage : ContentPage
	{
        public New SelectedNew { get; set; }
        public UserNewsPage ()
		{
			InitializeComponent ();
            this.BindingContext = this;
        }
        protected override void OnAppearing()
        {
            ShowItems();
        }
        void ShowItems()
        {
            //ItemColl.ItemsSource = App.Db.GetNews();
        }

        private async void ItemColl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Navigation.PushAsync(new UserAboutNew(SelectedNew));
        }
    }
}