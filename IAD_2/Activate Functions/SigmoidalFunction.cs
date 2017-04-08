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
        public double getNeuronOutput(double[] _input, double[] _weights)
        {
            return (double)(1.0d / 1.0d + Math.Exp(-1 * this.output(_input, _weights)));
        }

        /// <summary>
        /// Funkcja zwraca sumę iloczynu wektora wejściowego z wagami neuronu
        /// </summary>
        /// <returns></returns>
        private double output(double[] _input, double[] _weights)
        {
            double sum = 0d;

            if (_weights.Length != _input.Length)
                throw new Exception("Niepoprawne wektory wag..");

            for (int i = 0; i < _input.Length; i++)
                sum += _input[i] * _weights[i];

            return sum;
        }
    }
}
