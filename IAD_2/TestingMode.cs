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
            RandomTeachingPattern rtp = new RandomTeachingPattern();
            int[] input = rtp.getTeachingPattern();
            int[] expected = input; // W naszym przypdku wartości wyjściowe to wejściowe

            perceptron.initLayer(1, 4, 4, new DuplicateFunction());
            perceptron.initLayer(2, 2, 4, new SigmoidalFunction());
            perceptron.initLayer(3, 4, 2, new SigmoidalFunction());

            perceptron.randomWeights();

            for (int i = 0; i < 500; i++)
            {
                perceptron.randomWeights();
                perceptron.forwardPropagation(input);
                perceptron.countErrors(expected);

                Console.WriteLine(perceptron.sumSquaredError(expected));
            }


        }
    }
}
