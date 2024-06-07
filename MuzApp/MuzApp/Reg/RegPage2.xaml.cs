using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MuzApp.Reg
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegPage2 : ContentPage
    {
        private int IdStudent;
        private string NameStudent;
        private string SurameStudent;
        public RegPage2(int id, string Name, string Surname)
        {
            InitializeComponent();
            IdStudent = id;
            NameStudent = Name; 
            SurameStudent = Surname;
        }

        private void Reg2_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (DateOfBirthPic.Date != null) 
                {
                    DateTime date = DateOfBirthPic.Date;
                    Navigation.PushAsync(new RegPage3(IdStudent, NameStudent, SurameStudent, date));
                }
                else
                {
                    DisplayAlert("Ошибка", "Выберите дату", "Ок");
                }
            }
            catch
            {
                DisplayAlert("Ошибка", "Ошибка подключения Firebase. Проверьте связь с сервером", "Ок");
            }
        }
    }
}