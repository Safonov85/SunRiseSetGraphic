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

namespace SunRiseSetGraphic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BitmapImage bitmap;

        public MainWindow()
        {
            InitializeComponent();

            bitmap = new BitmapImage();
            //bitmap.UriSource = new Uri(dialog.FileName);
            var i = BitmapSource.Create(
    2,
    2,
    96,
    96,
    PixelFormats.Indexed1,
    new BitmapPalette(new List<Color> { Colors.Red }),
    new byte[] { 0, 0, 0, 0 },
    1);
            
            MainGraphImage.Source = i;

            DrawOnCertainPixel();
        }

        private void MainWindow1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //DrawOnCertainPixel();
        }

        void DrawOnCertainPixel()
        {

            DrawingVisual drawVis = new DrawingVisual();
            using (DrawingContext dc = drawVis.RenderOpen())
            {
                dc.DrawImage(MainGraphImage.Source, new Rect(0, 0, MainGraphImage.Width, MainGraphImage.Height));
                dc.DrawLine(new Pen(Brushes.Blue, 2), new Point(0, 0), new Point(MainGraphImage.Width, MainGraphImage.Height));
                dc.DrawRectangle(Brushes.Green, null, new Rect(20, 20, 150, 100));
            }

            RenderTargetBitmap targetBitmap = new RenderTargetBitmap((int)MainGraphImage.Width, (int)MainGraphImage.Height, 96, 96, PixelFormats.Pbgra32);
            targetBitmap.Render(drawVis);

            MainGraphImage.Source = targetBitmap;
        }

        private void MainWindow1_MouseMove(object sender, MouseEventArgs e)
        {
            this.MainWindow1.Title = "Baah";

            // to clear the image
            MainGraphImage.Source = null;

            var point = e.GetPosition(this.MainGraphImage);
            Point pointCircle;
            pointCircle.X = MainGraphImage.Width / 2;
            pointCircle.Y = MainGraphImage.Height / 2;

            // draw the new frame
            DrawingVisual drawVis = new DrawingVisual();
            using (DrawingContext dc = drawVis.RenderOpen())
            {
                dc.DrawImage(MainGraphImage.Source, new Rect(0, 0, MainGraphImage.Width, MainGraphImage.Height));
                dc.DrawLine(new Pen(Brushes.Blue, 2), new Point(0, 0), new Point(MainGraphImage.Width, MainGraphImage.Height));
                if(point.X > 0 && point.Y > 0)
                {
                    dc.DrawRectangle(Brushes.Green, null, new Rect(0, 0, point.X, point.Y));
                }
                dc.DrawEllipse(Brushes.Pink, null, pointCircle, point.X, point.X);
            }

            RenderTargetBitmap targetBitmap = new RenderTargetBitmap((int)MainGraphImage.Width, (int)MainGraphImage.Height, 96, 96, PixelFormats.Pbgra32);
            targetBitmap.Render(drawVis);

            MainGraphImage.Source = targetBitmap;
        }
    }

    
}
