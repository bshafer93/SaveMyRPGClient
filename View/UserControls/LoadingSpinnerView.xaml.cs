using System;
using System.Windows;
using System.Windows.Controls;

namespace SaveMyRPGClient.View.UserControls
{
    /// <summary>
    /// Interaction logic for LoadingSpinnerView.xaml
    /// </summary>
    public partial class LoadingSpinnerView : UserControl
    {
        public LoadingSpinnerView()
        {
            InitializeComponent();
        }

        private void gif_MediaEnded(object sender, RoutedEventArgs e)
        {
            gif.Position = new TimeSpan(0, 0, 1);
            gif.Play();
        }
    }
}
