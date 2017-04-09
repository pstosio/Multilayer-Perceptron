using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Perceptron perceptron = new Perceptron(3);

            perceptron.initLayer(1, 4, 4, new DuplicateFunction());
            perceptron.initLayer(2, 2, 4, new SigmoidalFunction());
            perceptron.initLayer(3, 4, 2,  new SigmoidalFunction());


            perceptron.randomWeights();

            Console.WriteLine(perceptron.ToString());

            perceptron.backPropagation(0.9, 0.0);

            Console.WriteLine(perceptron.ToString());

            Console.ReadLine();

        }
    }
}
