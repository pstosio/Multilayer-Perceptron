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
        /// <summary>
        /// Funkcja sigmoidalna unipolarna
        /// </summary>
        /// <param name="_input"></param>
        /// <param name="_weights"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public int getNeuronOutput(int[] _input, double[] _weights, int _id)
        {
            double ret = (double)(1.0d / (1.0d + Math.Exp(-1.0d * this.outputSum(_input, _weights))));

            // Próg aktywacji ustawiony na wartoś 0.5
            return ret >= 0.5 ? 1 : 0;
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
    }
}
