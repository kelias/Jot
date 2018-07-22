/*
    Jarloo
    http://jarloo.com
 
    This work is licensed under a Creative Commons Attribution-ShareAlike 3.0 Unported License  
    http://creativecommons.org/licenses/by-sa/3.0/ 

*/

using System.Windows;
using System.Windows.Controls;
using Jarloo.Jot.Helpers;
using Jarloo.Jot.Models;

namespace Jarloo.Jot.Controls
{
    public class StampControl : MoveableControl
    {
        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register("Angle", typeof(int),
            typeof(StampControl),
            new FrameworkPropertyMetadata(2,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsParentMeasure));

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string),
            typeof(StampControl),
            new FrameworkPropertyMetadata("",
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsParentMeasure));

        static StampControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StampControl),
                new FrameworkPropertyMetadata(typeof(StampControl)));
        }

        public StampControl()
        {
            GotFocus += NoteControl_GotFocus;
        }

        private void NoteControl_GotFocus(object sender, RoutedEventArgs e)
        {
            var w = (IWidget) DataContext;
            WidgetManager.GetInstance().ChangeZOrder(w);
        }

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public int Angle
        {
            get => (int) GetValue(AngleProperty);
            set => SetValue(AngleProperty, value);
        }

        public override void Move(double x, double y)
        {
            var s = (Stamp) DataContext;
            s.X += x;
            s.Y += y;
        }

        //Need to add an event but can't in the generic.xaml so this is the best way I've found to do this.
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var deleteButton = (Button) GetTemplateChild("PART_DELETE");
            deleteButton.Click += deleteButton_Click;
        }

        public void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var s = (Stamp) ((FrameworkElement) sender).DataContext;

            if (
                MessageBox.Show("Are you sure you want to delete this stamp?", "Delete Stamp", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) != MessageBoxResult.Yes) return;

            WidgetManager.GetInstance().Widgets.Remove(s);
        }
    }
}