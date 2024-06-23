using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Tools.Sms.Model
{
    public class Sms
    {
        const String ApiKey = "47657152613466776F353273357145637249456F3665475964446B75756345527449466A644879592F6F4D3D";
        const String SenderLine = "10006000330033";
        public static async void SendSms(string phoneNumber, string text)
        {
            Message msg = new Message();
            msg.Sender = SenderLine;
            msg.Receptor = phoneNumber;
            msg.Text = text;

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.kavenegar.com/v1/47657152613466776F353273357145637249456F3665475964446B75756345527449466A644879592F6F4D3D/");

            var data = new {};
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");

           
            var result = await client.PostAsync($"sms/send.json?sender={SenderLine}&receptor={phoneNumber}&message={text}", content);
            if (result == null) return;
           
            Console.WriteLine();
        }  
    }
}
