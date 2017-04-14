using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAD_2
{
    public class TeachingMode
    {
        Perceptron perceptron;
        TeachingPatterns teachingPatterns;
        double[] input, expected;

        public TeachingMode(Perceptron _perceptron, int _epoch, int _logJump, double _errorLevel, bool _isRandomOrderPatterns, double _learnFactor, double _momentum)
        {
            teachingPatterns = new TeachingPatterns();
            perceptron = _perceptron;
            perceptron.randomWeights();

            for (int i = 0; i <= _epoch; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    if (_isRandomOrderPatterns == true)
                        input = teachingPatterns.getRandomTeachingPattern();
                    else
                        input = teachingPatterns.getTeachingPattern(j);

                    expected = input; // W naszym przypdku wartości wyjściowe to wejściowe
                    perceptron.backPropagation(_learnFactor, _momentum, input, expected);
                    perceptron.addSumSquaredError(expected);
                }
                perceptron.estimateSumSquaredError();
                if (i % _logJump == 0)
                {
                    Console.WriteLine(perceptron.getSumSquaredError());
                    perceptron.saveErrorToFile();
                }

                if(perceptron.getSumSquaredError() <= _errorLevel)
                {
                    Console.WriteLine("Aby osiągnąć błąd średniokwadratowy mniejszy niż {0}, potrzeba było {1} epok.",
                                     _errorLevel, i);
                    Console.WriteLine(perceptron.ToString());
                    break;
                }

                perceptron.resetSumSquaredError();
            }

            

        }
    }
}
