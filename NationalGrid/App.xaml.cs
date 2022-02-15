using NationalGrid.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NationalGrid
{
    public partial class App : Application
    {
        /// <summary>
        /// If user has already entered API and Device ID then from next time it will check and redirect main page accordingly.
        /// </summary>
        public App()
        {
            InitializeComponent();
            if (Application.Current.Properties.Count > 0)
            {
                string API = Convert.ToString(Application.Current.Properties["API"]);
                string DeviceID = Convert.ToString(Application.Current.Properties["DeviceID"]);
                if (!(string.IsNullOrEmpty(DeviceID) && string.IsNullOrEmpty(API)))
                {
                    MainPage = new NavigationPage(new Welcome());
                }
            }
            else
                MainPage = new NavigationPage(new Login());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
