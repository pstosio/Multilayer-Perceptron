using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_2
{
    public abstract class ActivateFunction
    {
        /// <summary>
        /// Wartości wejściowe
        /// </summary>
        public double[] inputValues;

        /// <summary>
        /// Wagi wejściowe
        /// </summary>
        public double[] inputWeights;

        /// <summary>
        /// Wartośc sumatora
        /// </summary>
        public double adderValue;

        /// <summary>
        /// Wartość funkcji aktywacji
        /// </summary>
        public double outputValue;

        /// <summary>
        /// Funkcja inicjuje liczenie sumatora i wartości wyjściowej neuronu
        /// </summary>
        /// <param name="_input"></param>
        /// <param name="_weights"></param>
        /// <returns></returns>
        public abstract void initFunction(double[] _input, double[] _weights);

        /// <summary>
        /// Funkcja zwraca sumę iloczynu wektora wejściowego z wagami neuronu - sumator
        /// </summary>
        public double getNeuronOutputAdder()
        {
            double sum = 0d;

            if (inputWeights.Length != inputValues.Length)
                throw new Exception("Niepoprawne wektory wag..");

            for (int i = 0; i < inputValues.Length; i++)
                sum += inputValues[i] * inputWeights[i];

            return sum;
        }
        /// <summary>
        /// Funkcja liczy wyjście danego neuronu obliczone z funkcji aktywacji
        /// </summary>
        /// <returns></returns>
        public abstract double getNeuronOutputValue();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract double getNeuronDeriativeOutput();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_neuronOutput"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public abstract double computeError(double _neuronOutput, int target);
    }
}
