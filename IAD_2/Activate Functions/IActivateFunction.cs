using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_2
{
    public interface IActivateFunction
    {
        int getNeuronOutput(int[] _input, double[] _weights, int id);

        double getNeuronDeriativeOutput();

        double computeError(int _neuronOutput, int target);
    }
}
