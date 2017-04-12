using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_2
{

    public class TestingMode
    {
        public TestingMode()
        {
            Perceptron perceptron = new Perceptron(3);
            RandomTeachingPattern rtp;

            perceptron.initLayer(1, 4, 4, new DuplicateFunction(), false);
            perceptron.initLayer(2, 2, 4, new SigmoidalFunction(), false);
            perceptron.initLayer(3, 4, 2, new SigmoidalFunction(), false);

            rtp = new RandomTeachingPattern();
            double[] input = rtp.getTeachingPattern();
            double[] expected = input; // W naszym przypdku wartości wyjściowe to wejściowe

            perceptron.randomWeights();

            for (int i = 0; i < 100; i++)
            {
                perceptron.backPropagation(0.9, 0.0, input, expected);

                if (i % 1 == 0)
                    Console.WriteLine(perceptron.sumSquaredError(expected));
            }


        }
    }
}
