using NationalGrid.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NationalGrid.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Welcome : ContentPage
    {
        public Welcome()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Redirecting to Login activity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHeader_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }

        /// <summary>
        /// Redirect to Polling activity after connection succeded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Poll());
        }

        /// <summary>
        /// When Activity is loaded then this event will be trigger.
        /// Checking API is working and getting response or not to enable user for poll.
        /// </summary>
        /// 
        protected override void OnAppearing()
        {

            btnConnect.IsEnabled = false;
            btnConnect.BackgroundColor = Color.FromHex("#bbbdbf");
           
            if (Application.Current.Properties.Count > 0)
            {
                string API = Convert.ToString(Application.Current.Properties["API"]);
                string DeviceID = Convert.ToString(Application.Current.Properties["DeviceID"]);
                if ((!string.IsNullOrEmpty(DeviceID) && !string.IsNullOrEmpty(API)) == true)
                {
                    StartUPAPI();
                }
            }
        }

        /// <summary>
        ///  API call to get user name by device ID and API URL
        ///  If any exception occured then it will try again until get positive response.
        /// </summary>
        public async void StartUPAPI()
        {

            try
            {
                string API = Convert.ToString(Application.Current.Properties["API"]);
                string DeviceID = Convert.ToString(Application.Current.Properties["DeviceID"]);

                if ((!string.IsNullOrEmpty(DeviceID) && !string.IsNullOrEmpty(API)) == true)
                {
                    Startup startup = new Startup();
                    startup = await APIClass.GetDeviceIDAsync(API, DeviceID, false);

                    UserName.Text = startup.AssignedName;
                    if (!string.IsNullOrWhiteSpace(UserName.Text))
                    {
                        btnConnect.IsEnabled = true;
                        btnConnect.BackgroundColor = Color.FromHex("#0C7CBA");
                        Application.Current.Properties["UserName"] = startup.AssignedName;
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Contact Admin to resotre API and DeviceID", "OK");
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", "Please Contact Admin", "OK");
                StartUPAPI();
            }
        }
    }
}