/*
    Jarloo
    http://jarloo.com
 
    This work is licensed under a Creative Commons Attribution-ShareAlike 3.0 Unported License  
    http://creativecommons.org/licenses/by-sa/3.0/ 

*/

using System.Windows;
using System.Windows.Controls.Primitives;

namespace Jarloo.Jot.Controls
{
    public abstract class MoveableControl : Thumb
    {
        static MoveableControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MoveableControl),
                new FrameworkPropertyMetadata(typeof(MoveableControl)));
        }

        protected MoveableControl()
        {
            DragDelta += MoveableControl_DragDelta;
        }

        private void MoveableControl_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Move(e.HorizontalChange, e.VerticalChange);
        }

        public abstract void Move(double x, double y);
    }
}