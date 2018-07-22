/*
    Jarloo
    http://jarloo.com
 
    This work is licensed under a Creative Commons Attribution-ShareAlike 3.0 Unported License  
    http://creativecommons.org/licenses/by-sa/3.0/ 

*/
using System.Windows;
using System.Windows.Input;
using Jarloo.Jot.ViewModels;

namespace Jarloo.Jot.Views
{
    public partial class CorkBoardView : Window
    {
        private CorkBoardViewModel vm;

        public CorkBoardView()
        {
            InitializeComponent();
            vm = new CorkBoardViewModel(this);
            DataContext = vm;
        }

        private void MainView_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            vm.Zoom(e.Delta);
        }

        private void MainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            vm.StartPan(e.GetPosition(MainGrid));
        }

        private void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {
            vm.ContinuePan(e.GetPosition(MainGrid));
        }

        private void MainGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            vm.StopPan(e.GetPosition(MainGrid));
        }
    }
}