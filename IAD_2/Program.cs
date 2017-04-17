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
            TestingMode test = null;
            TeachingMode teach = null;

            Perceptron perceptron = new Perceptron(3);

            perceptron.initLayer(1, 4, 4, new DuplicateFunction(), false);
            perceptron.initLayer(2, 2, 4, new SigmoidalFunction(), true);
            perceptron.initLayer(3, 4, 2, new SigmoidalFunction(), true);

            bool learnMode = false; // <== Tu można przestawić tryb pracy perceptronu

            if (learnMode == true)
            {
                teach = new TeachingMode(perceptron, 200, 1, false, 0.9, 0.6);
                perceptron.saveWeightsToFile();
            }
            else
                test = new TestingMode(perceptron);

            ErrorLineChart elc = new ErrorLineChart();
            System.Windows.Forms.Application.Run(elc);

            Console.ReadLine();
        }
    }
}
