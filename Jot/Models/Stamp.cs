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
    public class Stamp : INotifyPropertyChanged, IWidget
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;


        private int angle;
        private double x;
        private double y;
        private string text;

        public string Text
        {
            get => text;
            set
            {
                text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Text"));
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

        public bool IsMatch(string searchText)
        {
            return !string.IsNullOrEmpty(text) && Regex.IsMatch(text, searchText);
        }
    }
}