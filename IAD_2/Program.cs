using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace IAD_2
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Perceptron perceptron = new Perceptron(3);

            perceptron.initLayer(1, 4, 4, new DuplicateFunction(), false);
            perceptron.initLayer(2, 2, 4, new SigmoidalFunction(), true);
            perceptron.initLayer(3, 4, 2, new SigmoidalFunction(), true);

            TeachingMode teach = new TeachingMode(perceptron, 100000, 1, 0.001, false, 0.6, 0.0);

            /*
            Chart chart = new Chart();
            chart.Palette = ChartColorPalette.SeaGreen;
            chart.Titles.Add("Błąd średniokwadratowy");
            double[] dataContext = new double[1001];
            string tmpLine;
            int idx = 0;
            string path = @"C:\Users\Peter\Desktop\IAD\Zad 2\errorLog.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                while ((tmpLine = reader.ReadLine()) != null)
                {
                    dataContext[idx] = Convert.ToDouble(tmpLine);
                    idx++;
                }
            }

            Series series = chart.Series.Add("błąd");
            series.Points.Add(dataContext);
            chart.SaveImage(@"C:\Users\Peter\Desktop\IAD\Zad 2\error.png", ChartImageFormat.Png);
    */

            Console.ReadLine();
        }
    }
}
