using System;
using System.Windows.Forms;
using System.Windows.Media;
using System.IO;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;

namespace IAD_2
{
    public partial class ErrorLineChart : Form
    {
        ChartValues<double> values;

        public ErrorLineChart()
        {
            InitializeComponent();

            values = new ChartValues<double>();

            string path = @"C:\Users\Peter\Desktop\IAD\Zad 2\errorLog.txt";
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;

                    // Read and display lines from the file until 
                    // the end of the file is reached. 
                    while ((line = sr.ReadLine()) != null)
                    {
                        values.Add(Convert.ToDouble(line));
                    }
                }
            }
            catch (Exception e)
            {

                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            cartesianChart1.Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Błąd średniokwadratowy",
                        Values = values
                    },

                };

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Epoki"
                //Labels = values
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Błąd średniokwadratowy",
                LabelFormatter = value => value.ToString()
            });


        }

    }
}