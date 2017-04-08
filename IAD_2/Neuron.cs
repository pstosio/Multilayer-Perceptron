using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_2
{
    /// <summary>
    /// Klasa odpowiadająca za instację pojedynczego nuronu
    /// </summary>
    public sealed class Neuron
    {
        #region Const
        /// <summary>
        /// Funkcja aktywacji - dependency injection
        /// </summary>
        private IActivateFunction activateFunction; 

        /// <summary>
        /// Wektor wag neuronu
        /// </summary>
        private double[] weights;

        /// <summary>
        /// Wektor waaartości podany na wejście neuronu
        /// </summary>
        private double[] input;      
        #endregion

        /// <summary>
        /// Konstruktor przyjmujący funkcję aktywacji i wektor wejściowy
        /// </summary>
        /// <param name="_activateFunction"> Dependency Injection </param>
        /// <param name="_inputWeights"></param>
        public Neuron(IActivateFunction _activateFunction, double[] _input)
        {
            if (_input.Length == 0)
                throw new Exception("Liczba wejść neronu nie może być zerowa.");

            weights = new double[_input.Length];

            activateFunction = _activateFunction;
            input = _input;

            // Losowe wagi neuronu
            this.generateRandomWeights();
        }

        /// <summary>
        /// Funkcja losuje wagi neuronu z przedziału -1,1
        /// </summary>
        private void generateRandomWeights()
        {
            int seed = System.DateTime.Now.Millisecond;
            Random rand = new Random(seed);

            for(int i=0; i<weights.Length; i++)
            {
                weights[i] = rand.Next(-1, 1);
            }
        }

        /// <summary>
        /// Funkcja zwrcaca wyjście neuronu
        /// </summary>
        /// <returns></returns>
        public double neuronOutput()
        {
            return activateFunction.getNeuronOutput(input, weights);
        }
    }
}
