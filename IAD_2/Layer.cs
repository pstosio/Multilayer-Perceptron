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
        /// Konstruktor warstwy sieci neuronowe.
        /// </summary>
        /// <param name="_neuronAmount"> Ilośc neuronów w warstwie. </param>
        /// <param name="_inputAmount"> Ilość wejść - długość wektora wejściowego.</param>
        /// <param name="_activateFunction"> Funkcja aktywacji neuronów w warstwie. </param>
        public Layer(int _neuronAmount, int _inputAmount, IActivateFunction _activateFunction)
        {
            neurons = new List<Neuron>(_neuronAmount);

            for(int i=0; i < _neuronAmount; i++)
            {
                neurons.Add(new Neuron(_activateFunction, _inputAmount));
            }
        }
    }
}
