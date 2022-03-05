using System;
using System.Collections.Generic;
using System.Text;

namespace VkChatBot
{
    class main
    {
        private double _temp;

        private double _pressure;

        private double _temp_min;

        private double _temp_max;
        public double temp
        {
            get => _temp;
            set => _temp = value - 273.15;
        }
        public double pressure
        {
            get => _pressure;
            set => _pressure = value / 1.3332239;
        }
        public double temp_min
        {
            get => _temp_min;
            set => _temp_min = value - 273.15;
        }
        public double temp_max
        {
            get => _temp_max;
            set => _temp_max = value - 273.15;
        }
        public double feels_like;

        public double humidity;

        public int sea_level;

        public int grnd_level;
    }
}
