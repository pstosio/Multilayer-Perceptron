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
        /// Wektory wyjściowe poszczególnych warstw
        /// </summary>
        public List<int[]> vectors;

        /// <summary>
        /// Konstruktor przyjmujcy ilość warstw sieci perceptronu
        /// </summary>
        /// <param name="_layerAmount"> Ilość warstw sieci </param>
        public Perceptron(int _layerAmount)
        {
            layers = new List<Layer>(_layerAmount);
            vectors = new List<int[]>(_layerAmount);
        }

        /// <summary>
        /// Inicjalizacja pojedynczej warstwy sieci
        /// </summary>
        /// <param name="_neuronAmount"> Ilość neuronów w warstwie </param>
        /// <param name="_inputAmount"> Ilość wejść - długośc wektora wejściowego </param>
        /// <param name="_activateFunction"> Funkcja aktywacji - Inversion of Control </param>
        public void initLayer(int _id, int _neuronAmount, int _inputAmount, IActivateFunction _activateFunction)
        {
            layers.Add(new Layer(_id, _neuronAmount, _inputAmount, _activateFunction ));
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
        /// Metoda implementująca działanie perceptronu - 1 epoka
        /// </summary>
        public void process()
        {
            // 1. Podajemy na wejście sygnał wejściowy - wektor wejściowy
            int[] input = TeachingPatterns.input; 

            // 2. Obliczamy wartości wyjściowe warstw 
            for(int i=0; i<layers.Count; i++) // Pętla po warstwach sieci
            {
                if(i==0) // Dla pierwszej warstwy podajemy stały wzorzec
                {
                    vectors.Add(layers[0].process(input));
                }
                else // Kolejne warstwy przyjmują wektor wejściowy obliczony przez warstwę poprzedającą
                {
                    vectors.Add(layers[i].process(vectors[i - 1]));
                }
            }

            // 3. Wartości wyznaczone w ostatniej warstwie stanowią odpowiedź sieci na podany sygnał wejściowy.

            // 4. Sygnał porównujemy z wzorcowym i wyznaczamy błąd

            // 5. Ppropagujemy błąd wstecz sieci i dokonujemy korekty wartości wag połączeń synaptycznych
        }

        public override string ToString()
        {
            string ret;

            ret = "Zainicjowano następujący perceptron: \n";

            foreach(Layer layer in layers)
            {
                ret += layer.ToString();
            }

            return ret;

        }
    }
}
