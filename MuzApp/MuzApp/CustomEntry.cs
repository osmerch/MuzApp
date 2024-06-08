using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MuzApp
{
    public class CustomEntry : Entry
    {
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create("CornerRadius", typeof(int), typeof(CustomEntry), 10);
        public int EntryCornerRadius
        {
            get {  return (int)GetValue(CornerRadiusProperty);}
            set { SetValue(CornerRadiusProperty, value);}

        }

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create("BorderThickness", typeof(Color), typeof(CustomEntry), Color.White);
        public Color EntryBorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }
    }
}
