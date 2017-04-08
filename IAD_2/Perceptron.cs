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
       
        public Perceptron(int _layerAmount)
        {
            layers = new List<Layer>(_layerAmount);
        }

        public void initLayer(int _neuronAmount, int _inputAmount, IActivateFunction _activateFunction)
        {
            layers.Add(new Layer(_neuronAmount, _inputAmount, _activateFunction ));
        }

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
    }
}
