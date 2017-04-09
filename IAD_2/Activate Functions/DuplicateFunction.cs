﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_2
{
    /// <summary>
    /// Powielająca funkcja aktywacji
    /// </summary>
    public sealed class DuplicateFunction : ActivateFunction
    {
        public override void initFunction(double[] _inputValues, double[] _inputWeights)
        {
            inputValues = _inputValues;
            weights = _inputWeights;

            adderValue = this.getNeuronOutputAdder();
            outputValue = this.getNeuronOutputValue();
        }

        public override double getNeuronOutputValue()
        {
            return adderValue;
        }

        public override double getNeuronDeriativeOutput()
        {
            throw new NotImplementedException();
        }
    }
}
