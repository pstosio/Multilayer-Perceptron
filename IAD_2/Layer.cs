using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_2
{
    /// <summary>
    /// Pojedyńcza warstwa sieci neuronowej
    /// </summary>
    public class Layer
    {
        /// <summary>
        /// Lista przechowująca neurony warstwy
        /// </summary>
        public List<Neuron> neurons;

        /// <summary>
        /// Konstruktor parametrowy
        /// </summary>
        /// <param name="_neuronAmount">Ilość neuronów warstwy</param>
        public Layer(int _neuronAmount)
        {
            for(int i=0; i<_neuronAmount; i++)
            {
                neurons.Add(new Neuron(NetworkProperty.));
            }
        }
    }
}
