/*
    Jarloo
    http://jarloo.com
 
    This work is licensed under a Creative Commons Attribution-ShareAlike 3.0 Unported License  
    http://creativecommons.org/licenses/by-sa/3.0/ 

*/

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Jarloo.Jot.Helpers;
using Jarloo.Jot.Models;

namespace Jarloo.Jot.ViewModels
{
    public class NoteViewModel : DependencyObject
    {
        public ICommand NewNoteCommand { get; set; }

        public NoteViewModel()
        {
            BindCommands();
        }

        private void BindCommands()
        {
            NewNoteCommand = new RelayCommand(x =>
            {
                WidgetManager.GetInstance()
                    .Widgets.Add(new Note
                    {
                        Body = x.ToString(),
                        Angle = GetRandomAngle(),
                        X = 10,
                        Y = 10,
                        Timestamp = DateTime.Now,
                        Color = GetRandomColor()
                    });
            });
        }

        private static int GetRandomAngle()
        {
            var rand = new Random();
            return rand.Next(-5, 5);
        }

        public static string GetRandomColor()
        {
            var colors = new List<string> {"FFFFCB", "E5CBE4", "CBE4DE", "D5C69D"};

            var rnd = new Random();
            return colors[rnd.Next(0, colors.Count - 1)];
        }
    }
}