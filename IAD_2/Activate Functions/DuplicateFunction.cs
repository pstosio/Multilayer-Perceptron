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
        public int getNeuronOutput(int[] _input, double[] _weights, int _id)
        {
            return _input[_id];
        }

        public double computeError(int _input, int target)
        {
            throw new NotImplementedException();
        }

        public double getNeuronDeriativeOutput()
        {
            throw new NotImplementedException();
        }
    }
}
