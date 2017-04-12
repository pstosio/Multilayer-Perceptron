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
    public sealed class SigmoidalFunction : IActivateFunction
    {
        #region
        /// <summary>
        /// Wartości wejściowe
        /// </summary>
        public double[] inputValues;

        /// <summary>
        /// Wagi
        /// </summary>
        public double[] weights;

        /// <summary>
        /// Wartośc sumatora
        /// </summary>
        public double adderValue;

        /// <summary>
        /// Wartość funkcji aktywacji
        /// </summary>
        public double outputValue;
        #endregion

        public void initFunction(double[] _inputValues, double[] _inputWeights)
        {
            inputValues = _inputValues;
            weights = _inputWeights;

            adderValue = this.getNeuronAdder();
            outputValue = this.getNeuronOutputValue();
        }

        public double getNeuronAdder()
        {
            double sum = 0d;

            if (weights.Length != inputValues.Length)
                throw new Exception("Niepoprawne wektory wag..");

            for (int i = 0; i < inputValues.Length; i++)
                sum += inputValues[i] * weights[i];

            return sum;
        }

        public double getNeuronOutputValue()
        {
            return (double)(1.0d / (1.0d + Math.Exp( - adderValue)));
        }

        public double getNeuronDeriativeOutput()
        {
            return outputValue * (1.0d - outputValue);
        }
    }
}
