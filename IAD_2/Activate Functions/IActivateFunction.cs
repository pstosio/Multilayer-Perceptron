using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_2
{
    public interface IActivateFunction
    {
        double getNeuronOutput(double[] _input, double[] _weights);
    }
}
