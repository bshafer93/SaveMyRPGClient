using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
