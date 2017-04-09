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
    public sealed class SigmoidalFunction : ActivateFunction
    {

        public override void initFunction(double[] _inputValues, double[] _inputWeights)
        {
            inputValues = _inputValues;
            inputWeights = _inputWeights;

            adderValue = this.getNeuronOutputAdder();
            outputValue = this.getNeuronOutputValue();
        }

        /// <summary>
        /// Funkcja sigmoidalna unipolarna
        /// </summary>
        /// <returns></returns>
        public override double getNeuronOutputValue()
        {
            return (double)(1.0d / (1.0d + Math.Exp(-1.0d * adderValue)));
        }

        /// <summary>
        /// Pochodna
        /// </summary>
        /// <returns></returns>
        public override double getNeuronDeriativeOutput()
        {
            return 1.0d * outputValue * (1.0d * outputValue);
        }

        public override double computeError(double _input, int _target)
        {
            return (_target - _input) * this.getNeuronDeriativeOutput();
        }

    }
}
