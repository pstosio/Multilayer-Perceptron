using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_2
{
    public class ClassificationTestingMode
    {
        Perceptron perceptron;
        ClassificationTeachingSet cts;
        double[] input, expected;

        public ClassificationTestingMode(Perceptron _perceptron)
        {
            cts = new ClassificationTeachingSet();
            perceptron = _perceptron;

            perceptron.uploadWeights();

            for (int i = 1; i <= 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    input = cts.getTestingPattern(i, j);
                    expected = cts.getOutput(i);

                    perceptron.forwardPropagation(input);
                    perceptron.addSumSquaredError(expected);

                    string strInput = "";
                    for (int k = 0; k < perceptron.layers[0].input.Length; k++)
                    {
                        strInput += Convert.ToString(perceptron.layers[0].input[k]) + " ";
                    }

                    string strExpected = "";
                    for (int k = 0; k < expected.Length; k++)
                    {
                        strExpected += Convert.ToString(expected[k]);
                    }

                    string strOutput = "";
                    for (int k = 0; k < perceptron.layers.Last().output.Length; k++)
                    {
                        strOutput += Convert.ToString(Math.Round(perceptron.layers.Last().output[k], 4)) + " ";
                    }


                    Console.WriteLine(String.Format("input: {0} expected {1} output: {2}", strInput, strExpected, strOutput));
                }

                perceptron.estimateSumSquaredError();
            }


            Console.WriteLine("błąd średniokwadratowy: {0}\n", perceptron.getTotalSumSquaredError());
        }
    }
}
