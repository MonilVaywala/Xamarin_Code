using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NationalGrid.API;

namespace NationalGrid.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAPIDevice : ContentPage
    {
        public AddAPIDevice()
        {
            InitializeComponent();
        }
        /// <summary>
        /// To Add DeviceID and API and it will be Accessible throught out application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Savebtn_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DeviceID.Text) || string.IsNullOrEmpty(API.Text))
            {
                DisplayAlert("Warning", "Please enter API Url / Device ID", "OK");
            }
            else
            {
                Application.Current.Properties["API"] = API.Text;
                Application.Current.Properties["DeviceID"] = DeviceID.Text;

                Navigation.PushAsync(new Welcome());
            }
        }

        /// <summary>
        /// Back to previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHeader_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        /// <summary>
        /// When Activity is loaded then this event will be trigger
        /// </summary>
        protected override void OnAppearing()
        {
            if (Application.Current.Properties.Count > 0)
            {
                string api = Convert.ToString(Application.Current.Properties["API"]);
                string deviceID = Convert.ToString(Application.Current.Properties["DeviceID"]);
                if (!(string.IsNullOrEmpty(deviceID) && string.IsNullOrEmpty(api)))
                {
                    DeviceID.Text = deviceID;
                    API.Text = api;
                }
            }
        }
    }
}