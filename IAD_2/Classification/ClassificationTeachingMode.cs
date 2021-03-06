﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_2
{
    public class ClassificationTeachingMode
    {
        Perceptron perceptron;
        ClassificationTeachingSet cts;
        double[] input, expected;

        public ClassificationTeachingMode(Perceptron _perceptron, int _epoch, int _logJump, bool _isRandomOrderPatterns, double _learnFactor, double _momentum, double _errorLevel = 0.0)
        {
            perceptron = _perceptron;
            perceptron.randomWeights();

            for (int i = 0; i < _epoch; i++)
            {
                cts = new ClassificationTeachingSet();

                for (int j = 0; j < 50; j++)
                {
                    for (int k = 1; k <= 3; k++)
                    {
                        input = _isRandomOrderPatterns ? cts.getRandomTeachingPattern(k, j) : cts.getTeachingPattern(k, j);
                        expected = cts.getOutput(k);

                        /**** BACK PROPAGATION ALGORITHM ****/
                        perceptron.backPropagation(_learnFactor, _momentum, input, expected);
                        /************************************/

                        perceptron.addSumSquaredError(expected);
                    }
                }

                perceptron.estimateSumSquaredError();

                if (i % _logJump == 0)
                {
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

                    perceptron.saveErrorToFile();
                }

                if (perceptron.getTotalSumSquaredError() <= _errorLevel || i == _epoch)
                {
                    Console.WriteLine("błąd średniokwadratowy: {0}\n założony poziom błędu: {1}\n epoki: {2}",
                                     perceptron.getTotalSumSquaredError(), _errorLevel, i);
                    Console.WriteLine(perceptron.ToString());
                    break;
                }

                perceptron.resetSumSquaredError();
            }
        }
    }
}
