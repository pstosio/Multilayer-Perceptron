using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_2
{
    /// <summary>
    /// Powielająca funkcja aktywacji 
    /// </summary>
    public sealed class DuplicateFunction : IActivateFunction
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

        public void initFunction(double[] _inputValues, double[] _inputWeights, int _id = 0)
        {
            inputValues = _inputValues;
            weights = _inputWeights;

            adderValue = this.getNeuronAdder();
            outputValue = _inputValues[_id];//  this.getNeuronOutputValue();
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
            return adderValue;
        }

        public double getNeuronDeriativeOutput()
        {
            return 0;
        }
    }
}
