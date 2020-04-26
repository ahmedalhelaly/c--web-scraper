using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace WebScraper
{
    class Firebase
    {
        private string _url;
        private string _secret;
        public IFirebaseClient client;
        private IFirebaseConfig _config;
        public Firebase(string url, string secret)
        {
            this._url = url;
            this._secret = secret;
        }
        public bool Connect()
        {
            try
            {
                this._config = new FirebaseConfig
                {
                    AuthSecret = this._secret,
                    BasePath = this._url
                };
                this.client = new FirebaseClient(this._config);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public async void CreateRecord(Items data)
        {
            try
            {
                var node = data.country.ToString().ToLower();
                SetResponse response = await client.SetAsync($"{node}", data);
                Items result = response.ResultAs<Items>(); //The response will contain the data written
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
