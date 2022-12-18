using bank_system;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using System.Windows.Shapes;
using Point = System.Windows.Point;

namespace Bank
{
    /// <summary>
    /// Логика взаимодействия для AnalizeVklad.xaml
    /// </summary>
    public partial class AnalizeVklad : Window
    {
        
        public AnalizeVklad(int sum,int time)
        {
            InitializeComponent();
           
        }

        public AnalizeVklad()
        {
        }

        private void btn_openStab_Click(object sender, RoutedEventArgs e)
        {
           authoriz authoriz = new authoriz();
            authoriz.Show();
            this.Close();
        }

        private void btn_OpenOpt_Click(object sender, RoutedEventArgs e)
        {
            authoriz authoriz = new authoriz();
            authoriz.Show();
            this.Close();
        }

        private void btn_openStandart_Click(object sender, RoutedEventArgs e)
        {
            authoriz authoriz = new authoriz();
            authoriz.Show();
            this.Close();
        }

        private void btn_Create_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = Screen as UIElement;
            Uri path = new Uri(@"C:\Users\msi gf65\source\repos\bank_system\screenshot.png");
            CaptureScreen(element, path);
        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            Calculator calculator = new Calculator();
            calculator.Show();
            this.Close();
        }

        public void CaptureScreen(UIElement source, Uri destination)
        {
            try
            {
                double Height, renderHeight, Width, renderWidth;

                Height = renderHeight = source.RenderSize.Height;
                Width = renderWidth = source.RenderSize.Width;


                RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)renderWidth, (int)renderHeight, 96, 96, PixelFormats.Pbgra32);

                VisualBrush visualBrush = new VisualBrush(source);
                DrawingVisual drawingVisual = new DrawingVisual();
                using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                {
                    drawingContext.DrawRectangle(visualBrush, null, new Rect(new Point(0, 0), new Point(Width, Height)));
                }

                renderTarget.Render(drawingVisual);
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderTarget));
                using (FileStream stream = new FileStream(destination.LocalPath, FileMode.Create, FileAccess.Write))
                {
                    encoder.Save(stream);
                }
                PdfDocument doc = new PdfDocument();
                PdfPage page = doc.Pages.Add();
                PdfGraphics pdf = page.Graphics;
                PdfBitmap image = new PdfBitmap(@"C:\Users\msi gf65\source\repos\bank_system\screenshot.png");
                pdf.DrawImage(image, 0, 0);
                doc.Save(@"C:\Users\msi gf65\source\repos\bank_system\выписка.pdf");
                doc.Close(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
