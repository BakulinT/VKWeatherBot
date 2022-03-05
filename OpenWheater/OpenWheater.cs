using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace VkChatBot.OpenWheater
{
    class OpenWheater
    {
        public coord coord;

        public weather[] weather;

        [JsonProperty ("base")]
        public string Base;

        public main main;

        public double visibility;

        public wind wind;

        public snow snow;

        public clouds clouds;

        public double dt;

        public sys sys;

        public int timezne;

        public int id;

        public string name;

        public int cod;
    }
}
