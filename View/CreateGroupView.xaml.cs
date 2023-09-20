using System.Windows;
using System.Windows.Input;

namespace SaveMyRPGClient.View
{
    /// <summary>
    /// Interaction logic for CreateGroupView.xaml
    /// </summary>
    public partial class CreateGroupView : Window
    {
        public CreateGroupView()
        {
            InitializeComponent();
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimizeWindow_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
