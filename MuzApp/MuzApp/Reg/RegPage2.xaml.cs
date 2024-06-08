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
            Task.Run(AnimateBack);
        }
        private async void AnimateBack()
        {
            Action<double> forward = input => GradienBack.AnchorY = input;
            Action<double> backward = input => GradienBack.AnchorY = input;

            while (true)
            {
                GradienBack.Animate(name: "forward", callback: forward, start: 0, end: 1, length: 5000, easing: Easing.SinIn);
                await Task.Delay(5000);
                GradienBack.Animate(name: "backward", callback: backward, start: 1, end: 0, length: 5000, easing: Easing.SinIn);
                await Task.Delay(5000);

            }

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