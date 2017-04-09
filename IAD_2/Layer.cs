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
        #region Const
        /// <summary>
        /// Numer identyfikacyjny warstwy
        /// </summary>
        int id;
        
        /// <summary>
        /// Lista przechowująca neurony warstwy
        /// </summary>
        public List<Neuron> neurons;

        /// <summary>
        /// Wektor wejściowy warstwy 
        /// </summary>
        public int[] input;

        /// <summary>
        /// Wektor wyjściowy warstwy
        /// </summary>
        public int[] output;
        #endregion

        /// <summary>
        /// Konstruktor warstwy sieci neuronowe.
        /// </summary>
        /// <param name="_id"> Id warstwy </param>
        /// <param name="_neuronAmount"> Ilośc neuronów w warstwie. </param>
        /// <param name="_inputAmount"> Ilość wejść - długość wektora wejściowego.</param>
        /// <param name="_activateFunction"> Funkcja aktywacji neuronów w warstwie. </param>
        public Layer(int _id, int _neuronAmount, int _inputAmount, IActivateFunction _activateFunction)
        {
            id = _id;
            input = new int[_inputAmount];   
            output = new int[_neuronAmount]; // Wektor wyjściowy o wielkości neuronów w warstwie

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
        public void process(int[] _inputValues)
        {
            input = _inputValues;

            for(int i=0; i<neurons.Count; i++)
            {
                output[i] = (neurons[i].neuronOutput(input));
            }
        }

        /// <summary>
        /// Dla każdego neuronu warstwy ustawiamy wartość błędu
        /// Funkcja jedynie dla ostatniej warstwy - podajemy wartości oczekiwane
        /// </summary>
        public void countErrorLastLayer(int[] _expectedOutput)
        {
            for(int i=0; i<neurons.Count; i++)
            {
                neurons[i].neuronError(output[i], _expectedOutput[i]);
            }
        }

        public override string ToString()
        {
            string ret;

            ret = String.Format("\n{0} warstwa zawiera {1} neurony: \n", this.id, neurons.Count);

            foreach(Neuron neuron in neurons)
            {
                ret += neuron.ToString();
            }

            return ret;
        }
    }
}
