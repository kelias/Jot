/*
    Jarloo
    http://jarloo.com
 
    This work is licensed under a Creative Commons Attribution-ShareAlike 3.0 Unported License  
    http://creativecommons.org/licenses/by-sa/3.0/ 

*/

using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Jarloo.Jot.Models
{
    [Serializable]
    public class Note : INotifyPropertyChanged, IWidget
    {
        private int angle;
        private string body;
        private string color;
        private DateTime timestamp;
        private double x;
        private double y;

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime Timestamp
        {
            get => timestamp;
            set
            {
                timestamp = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Timestamp"));
            }
        }

        public int Angle
        {
            get => angle;
            set
            {
                angle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Angle"));
            }
        }

        public double Y
        {
            get => y;
            set
            {
                y = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Y"));
            }
        }


        public double X
        {
            get => x;
            set
            {
                x = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("X"));
            }
        }

        public string Color
        {
            get => color;
            set
            {
                color = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Color"));
            }
        }

        public string Body
        {
            get => body;
            set
            {
                body = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Body"));
                Timestamp = DateTime.Now;
            }
        }

        public bool IsMatch(string searchText)
        {
            return !string.IsNullOrEmpty(body) && Regex.IsMatch(body, searchText);
        }
    }
}