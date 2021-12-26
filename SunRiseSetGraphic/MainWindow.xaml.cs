using System;
using System.Collections.Generic;
using System.Globalization;
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

        Point pointCircle;

        public MainWindow()
        {
            InitializeComponent();

            bitmap = new BitmapImage();
            pointCircle = ranPlace();
            
            // some thing to get it started
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

        Point ranPlace()
        {
            Random rand = new Random();
            int x, y;
            x = rand.Next(1, (int)MainGraphImage.Width);
            y = rand.Next(1, (int)MainGraphImage.Height);

            Point point = new Point((double)x, (double)y);

            return point;
        }

        private void MainWindow1_MouseMove(object sender, MouseEventArgs e)
        {
            this.MainWindow1.Title = "Graphics";

            // to clear the image
            MainGraphImage.Source = null;

            var point = e.GetPosition(this.MainGraphImage);
            
            //pointCircle.X = MainGraphImage.Width / 2;
            //pointCircle.Y = MainGraphImage.Height / 2;

            float colorChange = (float)point.X / (float)MainGraphImage.Width;
            colorChange = colorChange * 200f;

            float transparentChange = 1f - colorChange;

            // draw the new frame
            DrawingVisual drawVis = new DrawingVisual();
            using (DrawingContext dc = drawVis.RenderOpen())
            {
                dc.DrawImage(MainGraphImage.Source, new Rect(0, 0, MainGraphImage.Width, MainGraphImage.Height));
                dc.DrawLine(new Pen(Brushes.Blue, 2), new Point(0, 0), new Point(MainGraphImage.Width, MainGraphImage.Height));
                if(point.X > 0 && point.Y > 0)
                {
                    dc.DrawRectangle(new SolidColorBrush(Color.FromArgb(127, 50, (byte)colorChange, 50)), null, new Rect(0, 0, point.X, point.Y));
                }
                // Transparent Circle
                dc.DrawEllipse(new SolidColorBrush(Color.FromArgb((byte)transparentChange, 50,50,50)), null, pointCircle, point.X * 0.5, point.X * 0.5);

                //
                dc.DrawRectangle(new SolidColorBrush(Color.FromArgb(255, 220, 220, 40)), null, new Rect(0, 0, 20, 200));

                dc.DrawRectangle(new SolidColorBrush(Color.FromArgb(255, 220, 220, 40)), null, new Rect(30, 0, 20, 200));

                dc.DrawRectangle(new SolidColorBrush(Color.FromArgb(255, 220, 220, 40)), null, new Rect(60, 0, 20, 200));

                FormattedText formattedText = new FormattedText(
        "Safonov",
        CultureInfo.GetCultureInfo("en-us"),
        FlowDirection.LeftToRight,
        new Typeface("Arial"),
        32,
        Brushes.White, VisualTreeHelper.GetDpi(this).PixelsPerDip);

                dc.DrawText(formattedText, new Point(MainGraphImage.Width - 150, 10));

                
            }

            RenderTargetBitmap targetBitmap = new RenderTargetBitmap((int)MainGraphImage.Width, (int)MainGraphImage.Height, 96, 96, PixelFormats.Pbgra32);
            targetBitmap.Render(drawVis);

            MainGraphImage.Source = targetBitmap;
        }
    }

    
}
