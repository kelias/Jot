/*
    Jarloo
    http://jarloo.com
 
    This work is licensed under a Creative Commons Attribution-ShareAlike 3.0 Unported License  
    http://creativecommons.org/licenses/by-sa/3.0/ 

*/

using System;
using System.Windows;
using System.Windows.Input;
using Jarloo.Jot.Helpers;
using Jarloo.Jot.Models;

namespace Jarloo.Jot.ViewModels
{
    public class StampViewModel : DependencyObject
    {
        public ICommand NewStampCommand { get; set; }

        public StampViewModel()
        {
            BindCommands();
        }

        public Action<object> NewStamp { get; private set; }

        private void BindCommands()
        {
            NewStampCommand = new RelayCommand(x =>
            {
                WidgetManager.GetInstance()
                    .Widgets.Add(new Stamp
                    {
                        Text = x.ToString(),
                        Angle = GetRandomAngle(),
                        X = 10,
                        Y = 10
                    });
            });
        }

        private static int GetRandomAngle()
        {
            var rand = new Random();
            return rand.Next(-5, 5);
        }
    }
}