using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RickrollXamarin
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string phone = this.phoneNumber.Text;
            var url = new Uri("http://rickroll-init.azurewebsites.net/api/rickroll?toNumber=" + phone);

            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();

            Message message = JsonConvert.DeserializeObject<Message>(responseString);

            await DisplayAlert("Call Sid", message.message, "Ok");
        }
    }

    public class Message
    {
        public string message { get; set; }
    }
}
