/*
    Jarloo
    http://jarloo.com
 
    This work is licensed under a Creative Commons Attribution-ShareAlike 3.0 Unported License  
    http://creativecommons.org/licenses/by-sa/3.0/ 

*/

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using Jarloo.Jot.Helpers;
using Jarloo.Jot.Models;
using Jarloo.Jot.Views;

namespace Jarloo.Jot.ViewModels
{
    public class CorkBoardViewModel : DependencyObject
    {
        public NoteViewModel NoteViewModel { get; set; }
        public StampViewModel StampViewModel { get; set; }

        private readonly CorkBoardView window;
        private Point origin;
        private Point start;

        public Action<int> Zoom { get; private set; }
        public Action<Point> StartPan { get; private set; }
        public Action<object> StopPan { get; private set; }
        public Action<Point> ContinuePan { get; private set; }

        public static readonly DependencyProperty SearchTextProperty = DependencyProperty.Register("SearchText",
            typeof(string), typeof(CorkBoardViewModel), new PropertyMetadata(null));

        private readonly CollectionViewSource viewSource = new CollectionViewSource();

        public CorkBoardViewModel(CorkBoardView win)
        {
            window = win;

            NoteViewModel = new NoteViewModel();
            StampViewModel = new StampViewModel();

            viewSource.Source = WidgetManager.GetInstance().Widgets;
            viewSource.Filter += viewSource_Filter;
            WidgetView = viewSource.View;

            SetupEvents();
        }

        public ICollectionView WidgetView { get; set; }
        public Action<object> SaveWidgets { get; private set; }

        public string SearchText
        {
            get => (string) GetValue(SearchTextProperty);
            set => SetValue(SearchTextProperty, value);
        }

        private void SetupEvents()
        {
            var dpd = DependencyPropertyDescriptor.FromProperty(SearchTextProperty, typeof(CorkBoardViewModel));
            dpd?.AddValueChanged(this, delegate { viewSource.View.Refresh(); });

            SaveWidgets = x => WidgetManager.GetInstance().Save();

            Zoom = x =>
            {
                var transform = window.ZoomTransform;
                var zoom = x > 0 ? .08 : -.08;
                if (transform.ScaleX + zoom < .05 || transform.ScaleX + zoom > 1) return;
                transform.ScaleX += zoom;
                transform.ScaleY += zoom;
            };

            StartPan = x =>
            {
                start = x;
                origin = new Point(window.PanTransform.X, window.PanTransform.Y);
                window.MainGrid.CaptureMouse();
            };

            StopPan = x => window.MainGrid.ReleaseMouseCapture();

            ContinuePan = x =>
            {
                if (!window.MainGrid.IsMouseCaptured) return;

                var v = start - x;
                window.PanTransform.X = origin.X - v.X;
                window.PanTransform.Y = origin.Y - v.Y;
            };
        }

        private void viewSource_Filter(object sender, FilterEventArgs e)
        {
            if (SearchText == null)
            {
                e.Accepted = true;
                return;
            }

            var w = (IWidget) e.Item;

            e.Accepted = w.IsMatch(SearchText);
        }
    }
}