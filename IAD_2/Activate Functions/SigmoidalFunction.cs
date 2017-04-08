using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_2
{
    /// <summary>
    /// Sigmoidalna funkcja aktywacji
    /// </summary>
    public class SigmoidalFunction : IActivateFunction
    {
        double sigmoidalValue;

        /// <summary>
        /// Funkcja sigmoidalna unipolarna
        /// </summary>
        /// <param name="_input"></param>
        /// <param name="_weights"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public int getNeuronOutput(int[] _input, double[] _weights, int _id)
        {
            sigmoidalValue = (double)(1.0d / (1.0d + Math.Exp(-1.0d * this.outputSum(_input, _weights))));

            // Próg aktywacji ustawiony na wartość 0.5
            return sigmoidalValue >= 0.5 ? 1 : 0;
        }

        /// <summary>
        /// Pochodna
        /// </summary>
        /// <returns></returns>
        public double getNeuronDeriativeOutput()
        {
            return 1.0d * sigmoidalValue * (1.0d * sigmoidalValue);
        }

        /// <summary>
        /// Funkcja zwraca sumę iloczynu wektora wejściowego z wagami neuronu
        /// </summary>
        /// <returns></returns>
        private double outputSum(int[] _input, double[] _weights)
        {
            double sum = 0d;

            if (_weights.Length != _input.Length)
                throw new Exception("Niepoprawne wektory wag..");

            for (int i = 0; i < _input.Length; i++)
                sum += _input[i] * _weights[i];

            return sum;
        }

        public double computeError(int _input, int _target)
        {
            return (_target - _input) * this.getNeuronDeriativeOutput();
        }
    }
}
