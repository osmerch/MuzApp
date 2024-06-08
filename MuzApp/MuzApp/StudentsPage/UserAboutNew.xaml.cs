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
	public partial class UserAboutNew : ContentPage
	{
        public bool edited = true;
        public int id_news;
        public UserAboutNew (New n)
		{
			InitializeComponent ();
			TitleTxt.Text = n.Title;
			DateLabel.Text = n.Date.ToString("f");
			DescLabel.Text = n.Description;
			NewsImage.Source = n.Image;
        }
	}
}