using NationalGrid.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NationalGrid
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }
        /// <summary>
        /// null or empty field validation, check weather email and password is null or empty  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Loginbtn_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Password.Text))
                DisplayAlert("Warning", "Please enter Password", "OK");
            else
            {
                if (Password.Text == "Pa55word")
                {
                    DisplayAlert("Login Success", "", "Ok");
                    //Navigate to Wellcom page after successfully login  
                    Navigation.PushAsync(new AddAPIDevice());
                }
                else
                    DisplayAlert("Login Fail", "Please enter correct Password", "OK");
            }
        }

    }
}