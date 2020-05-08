using OnLine.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OnLine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainModel viewModel = null;
        private NotifyIcon notifyIcon;

        public MainWindow()
        {
            InitializeComponent();

            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = ConvertFromBitmapFrame(Resources["mouse"] as BitmapFrame);
            notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(NotifyIcon_MouseDoubleClick);

            viewModel = new MainModel();
            viewModel.ToTrayHandler += ViewModel_ToTrayHandler;
            DataContext = viewModel;
        }

        private void NotifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            WindowState = WindowState.Normal;
            notifyIcon.Visible = false;
            ShowInTaskbar = true;
            Visibility = Visibility.Visible;
        }

        private void ViewModel_ToTrayHandler(object sender, EventArgs e)
        {
            WindowState = WindowState.Minimized;
            ShowInTaskbar = false;
            Visibility = Visibility.Hidden;
            notifyIcon.BalloonTipTitle = "Minimize Sucessful";
            notifyIcon.BalloonTipText = "Minimized the app ";
            notifyIcon.ShowBalloonTip(400);
            notifyIcon.Visible = true;
        }

        private static Icon ConvertFromBitmapFrame(BitmapFrame bitmapFrame)
        {
            var ms = new MemoryStream();
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(bitmapFrame);
            encoder.Save(ms);
            ms.Seek(0, SeekOrigin.Begin);
            var bmp = new Bitmap(ms);
            return System.Drawing.Icon.FromHandle(bmp.GetHicon());
        }
    }
}
