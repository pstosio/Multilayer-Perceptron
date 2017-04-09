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
        #region Const
        /// <summary>
        /// Warstwy sieci neuronowej
        /// </summary>
        private List<Layer> layers;
        #endregion

        /// <summary>
        /// Konstruktor przyjmujcy ilość warstw sieci perceptronu
        /// </summary>
        /// <param name="_layerAmount"> Ilość warstw sieci </param>
        public Perceptron(int _layerAmount)
        {
            layers = new List<Layer>(_layerAmount);
        }

        /// <summary>
        /// Inicjalizacja pojedynczej warstwy sieci
        /// </summary>
        /// <param name="_id">Identyfikator sieci </param>
        /// <param name="_neuronAmount"> Ilość neuronów w warstwie </param>
        /// <param name="_inputAmount"> Ilość wejść - długośc wektora wejściowego </param>
        /// <param name="_activateFunction"> Funkcja aktywacji - Inversion of Control </param>
        public void initLayer(int _id, int _neuronAmount, int _inputAmount, ActivateFunction _activateFunction)
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
        /// Algorytm wstecznej propagacji błędów do wywoływania w pętli
        /// </summary>
        public void backPropagation(double _wspolczynnikNauki, double _wspolczynnikMomentum)
        {
            this.process();
            this.countErrors();
            this.aktualizujWagi(_wspolczynnikMomentum, _wspolczynnikMomentum);
        }

        /// <summary>
        /// Epoka sieci
        /// </summary>
        public void process()
        {
            /*** 1. Podajemy na wejście sygnał wejściowy - wektor wejściowy ***/
            int[] input = TeachingPattern_1.input;

            double[] inputDouble = new double[input.Length];
            for(int i=0; i<input.Length; i++)
            {
                inputDouble[i] = Convert.ToDouble(input[i]);
            }

            /*** 2. Obliczamy wartości wyjściowe warstw ***/
            for(int i=0; i<layers.Count; i++) // Pętla po warstwach sieci
            {
                if(i==0) // Dla pierwszej warstwy podajemy stały wzorzec , int na double
                {
                    layers[0].process( inputDouble );
                }
                else // Kolejne warstwy przyjmują wektor wejściowy obliczony przez warstwę poprzedającą
                {
                    layers[i].process(layers[i - 1].output);
                }
            }
        }

        public void countErrors()
        {
            int[] expectedOutput = TeachingPattern_1.output;

            // Sygnał porównujemy z wzorcowym i wyznaczamy błąd
            //  - Wartości wyznaczone w ostatniej warstwie stanowią odpowiedź sieci na podany sygnał wejściowy.
            //  - liczymy błąd dla każdego nauronu rozpoczynając od warstwy ostatniej
            layers[layers.Count - 1].countErrorLastLayer(expectedOutput);

            // Pętla od przedostatniej warstwy do pierwszej
            for (int i = layers.Count - 2; i > 0; i--)
            {
                Layer następna = layers[i + 1];
                Layer aktualna = layers[i];

                // Pętla po neuronach warstwy aktualnej
                for (int j = 0; j < aktualna.neurons.Count; j++)
                {
                    double err = 0.0d;

                    foreach (Neuron neuronFromNext in następna.neurons)
                    {
                        err += neuronFromNext.error * neuronFromNext.getWeight(j);
                    }

                    aktualna.neurons[j].error = (err * aktualna.neurons[j].getNeuronDeriative());
                }
            }
        }

        /// <summary>
        /// Funkcja dokonuje korekty wartości wag połączeń synaptycznych, propaguje błąd wstecz sieci
        /// </summary>
        /// <param name="_wspolczynnikNauki"></param>
        /// <param name="_wspolczynnikMomentum"></param>
        public void aktualizujWagi(double _wspolczynnikNauki, double _wspolczynnikMomentum)
        {
            for (int i = 1; i < layers.Count; i++)
            {
                Layer aktualna = layers[i];
                Layer poprzednia = layers[i - 1];

                for (int j = 0; j < aktualna.neurons.Count; j++)
                {
                    Neuron neuron = aktualna.neurons[j];

                    for (int k = 0; k < neuron.weights.Length; k++)
                    {
                        double otrzymanaWartosc = 0.0d;
                        if (k >= poprzednia.neurons.Count)
                            otrzymanaWartosc = 1.0d;
                        else
                            otrzymanaWartosc = poprzednia.neurons[k].output;

                        double pochodna = neuron.getNeuronDeriative();
                        double delta = otrzymanaWartosc * _wspolczynnikNauki * neuron.error;
                        double nowaWaga = neuron.getWeight(k) + delta + _wspolczynnikMomentum * neuron.prevDelta[k];
                        neuron.prevDelta[k] = delta;
                        neuron.weights[k] = nowaWaga; 
                    }
                }
            }
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
