using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MuzApp.DbTables;
using static MuzApp.DB;
using Xamarin.Essentials;

namespace MuzApp.Reg
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegPage4 : ContentPage
    {
        private int IdStudent;
        private string NameStudent;
        private string SurameStudent;
        DateTime dateStudent;
        string emailStudent;
        public RegPage4(int id, string Name, string Surname, DateTime date, string email)
        {
            InitializeComponent();
            IdStudent = id;
            NameStudent = Name;
            SurameStudent = Surname;
            dateStudent = date;
            emailStudent = email;
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
        private async void Reg2_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Pas.Text != "" && PasRep.Text != "")
                {
                    if (Pas.Text == PasRep.Text)
                    {
                        var db = new DB();

                        string password = PasRep.Text;
                        UserDataAuth data = new UserDataAuth
                        {
                            UserId = IdStudent,
                            Email = emailStudent,
                            Password = password,
                            RoleId = 1
                        };
                        Student student = new Student
                        {
                            UserId = IdStudent,
                            Name = NameStudent,
                            Surname = SurameStudent,
                            DateOfBirth =dateStudent,
                        };
                        try
                        {
                            await db.AddUserData(data);
                            await db.AddStudent(student);
                            await DisplayAlert("Ошибка", "Регистрация прошла успешно!", "Ок");
                            await Navigation.PushAsync(new Auth());
                        }
                        catch
                        {
                            await DisplayAlert("Ошибка", "Ошибка подключения Firebase. Проверьте связь с сервером", "Ок");
                        }
                        

                    }
                    else
                    {
                        await DisplayAlert("Ошибка", "Пароли не совпадают", "Ок");
                    }
                    
                }
                else
                {
                    await DisplayAlert("Ошибка", "Проверьте данные", "Ок");
                }
            }
            catch
            {
                await DisplayAlert("Ошибка", "Ошибка подключения Firebase. Проверьте связь с сервером", "Ок");
            }
        }
    }
}