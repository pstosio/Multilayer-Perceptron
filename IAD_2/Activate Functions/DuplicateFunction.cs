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
    public class DuplicateFunction : IActivateFunction
    {
        public double getNeuronOutput(double[] _input, double[] _weights)
        {
            return _input[0];
        }
    }
}
