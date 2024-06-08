using Android.App;
using Android.Content;
using Android.OS;
using MuzApp;
using MuzApp.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomNavigationPageRenderer))]
namespace MuzApp.Droid
{
    public class CustomNavigationPageRenderer : NavigationPageRenderer
    {
        public CustomNavigationPageRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                SetBarBackground();
            }
        }

        private void SetBarBackground()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                // Используйте Xamarin.Forms.Color для установки фона и текста
                Element.BarBackgroundColor = Xamarin.Forms.Color.Transparent;
                Element.BarTextColor = Xamarin.Forms.Color.White;

                // Используйте Android.Graphics.Color для работы с Android ActionBar
                var actionBar = ((Activity)Context).ActionBar;
                if (actionBar != null)
                {
                    actionBar.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));
                    actionBar.SetDisplayShowTitleEnabled(false);
                    actionBar.SetDisplayShowTitleEnabled(true);
                }
            }
        }
    }
}
