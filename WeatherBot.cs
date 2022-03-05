using System;
using System.IO;
using System.Net;

namespace VkChatBot
{
    class WeatherBot
    {
        public string dw;
        private string apiKey;
        private string url;
        public WeatherBot()
        {
            apiKey = "";
        }
        public void Load(string sity)
        {
            url = $"https://api.openweathermap.org/data/2.5/weather?q={sity}&appid={apiKey}&lang=ru";
            WebRequest request = WebRequest.Create(url);

            request.Method = "POST";
            request.ContentType = "application/x-www-urlencoded";

            WebResponse response = request.GetResponse();

            string answer = "";

            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(s))
                {
                    answer = reader.ReadToEnd();
                }
            }
            response.Close();
            dw = answer;
        }
    }
}
