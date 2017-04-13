using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
// <--

namespace IAD_2
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Perceptron perceptron = new Perceptron(3);

            perceptron.initLayer(1, 4, 4, new DuplicateFunction(), false);
            perceptron.initLayer(2, 2, 4, new SigmoidalFunction(), false);
            perceptron.initLayer(3, 4, 2, new SigmoidalFunction(), false);

            TeachingMode teach = new TeachingMode(perceptron, 1000, 1, 0.01, false, 0.6, 0.0);

            Console.ReadLine();
        }
    }
}
