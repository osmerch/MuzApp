using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.PlatformConfiguration.macOSSpecific;
using Xamarin.Forms;

namespace MuzApp
{
    public class CustomNavigationPage : Xamarin.Forms.NavigationPage
    {
        public CustomNavigationPage(Xamarin.Forms.Page root) : base(root)
        {
            Init();
        }

        private void Init()
        {
            // Установите основные свойства
            BarBackgroundColor = Color.Transparent;
            BarTextColor = Color.White;
        }
    }
}
