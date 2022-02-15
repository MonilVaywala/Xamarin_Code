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
    public partial class Poll : ContentPage
    {
        bool checkFirst = true;
        int AlertMsg = 0;
        public Poll()
        {
            InitializeComponent();
            // Every 2.5 seconds API will trigger to get response if new Question is available or not.
            Device.StartTimer(TimeSpan.FromMilliseconds(2500), TimerElapsed);
        }

        /// <summary>
        /// When Activity is loaded then this event will be trigger.
        /// If New Question is available then all buttons are enabled else all buttons are disabled.
        /// </summary>
        /// 
        protected override void OnAppearing()
        {
            AlertMsg = 0;
            checkFirst = true;
            if (Application.Current.Properties.Count > 0)
            {
                string userName = Convert.ToString(Application.Current.Properties["UserName"]);
                //UserName.Text = "Welcome " + userName;
                UserName.Text = userName;
                //System.Threading.Thread.Sleep(3000);
                layoutconnectd.IsVisible = true;
                layoutbtn.IsVisible = false;

                btnA.IsEnabled = false;
                btnB.IsEnabled = false;
                btnC.IsEnabled = false;
                btnD.IsEnabled = false;
                btnE.IsEnabled = false;
                btnF.IsEnabled = false;

                btnA.BackgroundColor = Color.FromHex("#bbbdbf");
                btnB.BackgroundColor = Color.FromHex("#bbbdbf");
                btnC.BackgroundColor = Color.FromHex("#bbbdbf");
                btnD.BackgroundColor = Color.FromHex("#bbbdbf");
                btnE.BackgroundColor = Color.FromHex("#bbbdbf");
                btnF.BackgroundColor = Color.FromHex("#bbbdbf");
                lblquestion.Text = "Waiting for question...";
            }
            //DisplayAlert("", "Seguro que desea salir del App?", "Si", "No");
        }

        /// <summary>
        /// Timer to call API at every 2.5 seconds.
        /// </summary>
        /// <returns></returns>
        private bool TimerElapsed()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (checkFirst)
                {
                    layoutbtn.IsVisible = true;
                    layoutconnectd.IsVisible = false;
                    checkFirst = false;
                }

                PollAPI();

            });

            //return true to keep timer reccurring
            //return false to stop timer

            return true;
        }

        /// <summary>
        /// Calling API to check new Question is available or not and Enabled/Disabled buttons accordingly.
        /// </summary>
        public async void PollAPI()
        {
            try
            {
                string API = Convert.ToString(Application.Current.Properties["API"]);
                
                if (!(string.IsNullOrEmpty(API)))
                {
                    PollStatus poll = new PollStatus();
                    poll = await APIClass.GetPollStartStatus(API, false);

                    //if (poll.ReadyToAcceptAnswers || test == 5)
                    if (poll.ReadyToAcceptAnswers)
                    {
                        btnA.IsEnabled = true;
                        btnB.IsEnabled = true;
                        btnC.IsEnabled = true;
                        btnD.IsEnabled = true;
                        btnE.IsEnabled = true;
                        btnF.IsEnabled = true;

                        btnA.BackgroundColor = Color.FromHex("#0C7CBA");
                        btnB.BackgroundColor = Color.FromHex("#0C7CBA");
                        btnC.BackgroundColor = Color.FromHex("#0C7CBA");
                        btnD.BackgroundColor = Color.FromHex("#0C7CBA");
                        btnE.BackgroundColor = Color.FromHex("#0C7CBA");
                        btnF.BackgroundColor = Color.FromHex("#0C7CBA");
                        lblquestion.Text = "Please submit your answer(s) now!";
                    }
                    else
                    {
                        btnA.IsEnabled = false;
                        btnB.IsEnabled = false;
                        btnC.IsEnabled = false;
                        btnD.IsEnabled = false;
                        btnE.IsEnabled = false;
                        btnF.IsEnabled = false;

                        btnA.BackgroundColor = Color.FromHex("#bbbdbf");
                        btnB.BackgroundColor = Color.FromHex("#bbbdbf");
                        btnC.BackgroundColor = Color.FromHex("#bbbdbf");
                        btnD.BackgroundColor = Color.FromHex("#bbbdbf");
                        btnE.BackgroundColor = Color.FromHex("#bbbdbf");
                        btnF.BackgroundColor = Color.FromHex("#bbbdbf");
                        lblquestion.Text = "Waiting for question...";
                    }
                }
                else
                {
                    if (AlertMsg == 0){await DisplayAlert("Error", "Contact Admin to resotre API and DeviceID", "OK");}
                    AlertMsg = AlertMsg + 1;
                    if (AlertMsg == 10) AlertMsg = 0;
                }
            }

            catch
            {
                if (AlertMsg == 0) {await DisplayAlert("Error", "Please Contact Admin", "OK");}
                AlertMsg = AlertMsg + 1;
                if (AlertMsg == 10) AlertMsg = 0;
            }

        }

        /// <summary>
        /// Calling API to submit answer
        /// </summary>
        /// <param name="Answer"></param>
        public async void SubmitPollAPI(string Answer)
        {

            try
            {
                string API = Convert.ToString(Application.Current.Properties["API"]);
                string DeviceID = Convert.ToString(Application.Current.Properties["DeviceID"]);

                if ((!string.IsNullOrEmpty(DeviceID) && !string.IsNullOrEmpty(API)) == true)
                {
                    PollStatus poll = new PollStatus();
                    poll = await APIClass.SubmitPollStatus(API, DeviceID, Answer, false);
                }
                else
                {
                    await DisplayAlert("Error", "Contact Admin to resotre API and DeviceID", "OK");
                    await Navigation.PopAsync();
                }
            }
            catch
            {
                await DisplayAlert("Error", "Please Contact Admin", "OK");
            }

        }

        /// <summary>
        /// Redirect to previously opended activity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHeader_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        /// <summary>
        /// Calling API to submit answer which are clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            SubmitPollAPI(btn.Text);
        }
    }
}