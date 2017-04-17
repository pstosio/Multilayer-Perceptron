using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_2
{
    public class TestingMode
    {
        Perceptron perceptron;
        TeachingPatterns teachingPatterns;
        double[] input, expected;

        public TestingMode(Perceptron _perceptron)
        {
            teachingPatterns = new TeachingPatterns();
            perceptron = _perceptron;

            perceptron.uploadWeights();

            for (int j = 1; j <= 4; j++)
            {
                input = teachingPatterns.getTeachingPattern(j);

                expected = input; // W tym przypdku wartości wyjściowe to wejściowe

                perceptron.forwardPropagation(input);
                perceptron.addSumSquaredError(expected);

                string strInput = "";
                for (int k = 0; k < perceptron.layers[0].input.Length; k++)
                {
                    strInput += Convert.ToString(perceptron.layers[0].input[k]) + " ";
                }
                string strOutput = "";
                for (int l = 0; l < perceptron.layers.Last().output.Length; l++)
                {
                    strOutput += Convert.ToString(Math.Round(perceptron.layers.Last().output[l], 4)) + " ";
                }

                Console.WriteLine(String.Format("input: {0} output: {1}", strInput, strOutput));
            }
            perceptron.estimateSumSquaredError();

            Console.WriteLine("błąd średniokwadratowy: {0}\n", perceptron.getTotalSumSquaredError());
        }


    }

}



