using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace NationalGrid.API
{
    /// <summary>
    /// Functions are created to call APIs.
    /// </summary>
    public static class APIClass
    {
        public static async System.Threading.Tasks.Task<Startup> GetDeviceIDAsync(string API, string DeviceID,bool isDemo)
        {
            Startup startup = new Startup();

            try
            {
                if (isDemo)
                {
                    startup.AssignedName = "User Name";
                    return startup;
                }

                using (var cl = new HttpClient())
                {
                    var formcontent = new FormUrlEncodedContent(new[]
                    { new KeyValuePair<string,string>("ControllerId",DeviceID)

                });

                    //var request = await cl.PostAsync("http://ng.snapwire-portal.co.uk/api/Startup", formcontent);
                    var request = await cl.PostAsync(API + "/Startup", formcontent);

                    request.EnsureSuccessStatusCode();

                    var response = await request.Content.ReadAsStringAsync();

                    startup = JsonConvert.DeserializeObject<Startup>(response);

                }
            }
            catch(Exception ex)
            {
                
            }

            return startup;
        }

        public static async System.Threading.Tasks.Task<PollStatus> GetPollStartStatus(string API, bool isDemo)
        {
            PollStatus poll = new PollStatus();

            if (isDemo)
            {
                poll.ReadyToAcceptAnswers = false;
                return poll;
            }

            using (var cl = new HttpClient())
            {
                var request = await cl.PostAsync(API + "/Poll", null);
                request.EnsureSuccessStatusCode();
                var response = await request.Content.ReadAsStringAsync();
                poll = JsonConvert.DeserializeObject<PollStatus>(response);
            }

            return poll;
        }

        public static async System.Threading.Tasks.Task<PollStatus> SubmitPollStatus(string API, string DeviceID,string Answer,bool isDemo)
        {
            PollStatus poll = new PollStatus();

            if (isDemo)
            {
                poll.ConfirmReceived = true;
                return poll;
            }

            using (var cl = new HttpClient())
            {
                var formcontent = new FormUrlEncodedContent(new[]
                { new KeyValuePair<string,string>("ControllerId",DeviceID),
                  new KeyValuePair<string,string>("Answer",Answer)
                });

                var request = await cl.PostAsync(API + "/SubmitAnswer", formcontent);
                request.EnsureSuccessStatusCode();
                var response = await request.Content.ReadAsStringAsync();
                poll = JsonConvert.DeserializeObject<PollStatus>(response);
            }

            return poll;
        }

    }
    public class Startup
    {
        public string ControllerId { get; set; }
        public string AssignedName { get; set; }

    }

    public class PollStatus
    {
        public string Timestamp { get; set; }
        public bool ReadyToAcceptAnswers { get; set; }
        public string ControllerId { get; set; }
        public string Answer { get; set; }
        public bool ConfirmReceived { get; set; }
    }
}
