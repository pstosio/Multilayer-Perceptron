using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_2
{
    /// <summary>
    /// Pojedyncza warstwa sieci neuronowej
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
                neurons.Add(new Neuron(_activateFunction, _inputAmount, i));
            }
        }

        /// <summary>
        /// Metoda procesuje warstwę. Przekazywany jest wektor wejściowy, a zwaracany jest wektor wyjściowy warstwy.
        /// </summary>
        /// <param name="_inputValues"> Wektor wejściowy warstwy. </param>
        /// <returns></returns>
        public int[] process(int[] _inputValues)
        {
            int[] output = new int[neurons.Count]; // Wektor wyjściowy o wielkości neuronów w warstwie

            for(int i=0; i<neurons.Count; i++)
            {
                output[i] = neurons[i].neuronOutput(_inputValues);
            }

            return output;
        }
    }
}
