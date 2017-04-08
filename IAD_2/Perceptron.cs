using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_2
{
    /// <summary>
    /// Perceptron wielowarstwowy - MLP
    /// </summary>
    public class Perceptron
    {
        /// <summary>
        /// Warstwy sieci neuronowej
        /// </summary>
        private List<Layer> layers;
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_layerAmount"> Ilość warstw sieci </param>
        public Perceptron(int _layerAmount)
        {
            layers = new List<Layer>(_layerAmount);
        }

        /// <summary>
        /// Inicjalizacja pojedynczej warstwy sieci
        /// </summary>
        /// <param name="_neuronAmount"> Ilość neuronów w warstwie </param>
        /// <param name="_inputAmount"> Ilość wejść - długośc wektora wejściowego </param>
        /// <param name="_activateFunction"> Funkcja aktywacji - Inversion of Control </param>
        public void initLayer(int _neuronAmount, int _inputAmount, IActivateFunction _activateFunction)
        {
            layers.Add(new Layer(_neuronAmount, _inputAmount, _activateFunction ));
        }

        /// <summary>
        /// Przyznawanie losowych wag do funkcji sumującej każdego z neuronów
        /// </summary>
        public void randomWeights()
        {
            foreach(Layer layer in layers)
            {
                foreach(Neuron neuron in layer.neurons)
                {
                    neuron.generateRandomWeights();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void process()
        {
            int[] input = new int[4]; // Wektor wejściowy warstwy przetwarzającej

            input[0] = 1;
            input[1] = 0;
            input[2] = 0;
            input[3] = 0;

            List<int[]> output = new List<int[]>();

            output.Add(layers[0].process(input));
            output.Add(layers[1].process(output[0]));
            output.Add(layers[2].process(output[1]));
        }
    }
}
