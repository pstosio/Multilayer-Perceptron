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

        /// <summary>
        /// Błąd średniokwadratowy sieci
        /// </summary>
        public double sumSquaredError;
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
        public void initLayer(int _id, int _neuronAmount, int _inputAmount, IActivateFunction _activateFunction, bool _isBias)
        {
            layers.Add(new Layer(_id, _neuronAmount, _inputAmount, _activateFunction, _isBias));
        }

        /// <summary>
        /// Przyznawanie losowych wag do funkcji sumującej każdego z neuronów
        /// </summary>
        public void randomWeights()
        {
            foreach (Layer layer in layers)
            {
                foreach (Neuron neuron in layer.neurons)
                {
                    neuron.generateRandomWeights();
                }
            }
        }

        /// <summary>
        /// Algorytm wstecznej propagacji błędów do wywoływania w pętli
        /// </summary>
        public void backPropagation(double _learningFaktor, double _momentum, double[] _input, double[] _expected)
        {
            this.forwardPropagation(_input);
            this.countErrors(_expected);
            this.updateWeights(_learningFaktor, _momentum);
        }

        /// <summary>
        /// Propagowanie w przód
        /// </summary>
        public void forwardPropagation(double[] _input)
        {
            /*** 1. Podajemy na wejście sygnał wejściowy - wektor wejściowy ***/
            double[] inputDouble = new double[_input.Length];
            for (int i = 0; i < _input.Length; i++)
            {
                inputDouble[i] = Convert.ToDouble(_input[i]);
            }

            /*** 2. Obliczamy wartości wyjściowe warstw ***/
            for (int i = 0; i < layers.Count; i++) // Pętla po warstwach sieci
            {
                if (i == 0) // Dla pierwszej warstwy podajemy stały wzorzec , int na double
                {
                    layers[0].process(inputDouble);
                }
                else // Kolejne warstwy przyjmują wektor wejściowy obliczony przez warstwę poprzedającą
                {
                    layers[i].process(layers[i - 1].output);
                }
            }
        }

        /// <summary>
        /// Zliczanie błędów poszczególnych neuronów
        /// </summary>
        public void countErrors(double[] _expectedOutput)
        {
            Layer lastLayer = layers[layers.Count - 1];
            for (int i = 0; i < lastLayer.neurons.Count; i++)
            {
                Neuron neuron = lastLayer.neurons[i];
                neuron.error = _expectedOutput[i] - neuron.outputValue;
            }
            for (int i = layers.Count - 2; i > 0; i--)
            {
                Layer nextLayer = layers[i + 1];
                Layer actual = layers[i];
                for (int j = 0; j < actual.neurons.Count; j++)
                {
                    double errTmp = 0.0d;
                    foreach (Neuron neuronFromNextLayer in nextLayer.neurons)
                    {
                        errTmp += neuronFromNextLayer.error * neuronFromNextLayer.weights[j];
                    }
                    actual.neurons[j].error = errTmp;
                }
            }
        }

        /// <summary>
        /// Błąd średniokwadratowy sieci 
        /// </summary>
        public void addSumSquaredError(double[] _expected)
        {
            Layer lastLayer = layers.Last();

            for (int i = 0; i < layers.Last().output.Length; i++)
            {
                sumSquaredError += _expected[i] - lastLayer.output[i];
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void estimateSumSquaredError()
        {
            sumSquaredError = Math.Pow(sumSquaredError, 2) / 2;
        }

        public double getSumSquaredError()
        {
            return sumSquaredError;
        }

        public void resetSumSquaredError()
        {
            sumSquaredError = 0.0d;
        }

        public void saveWeights()
        {
        }

        public void saveErrorToFile()
        {
            FileService.saveToFile(sumSquaredError);
        }

        /// <summary>
        /// Funkcja dokonuje korekty wartości wag połączeń synaptycznych, propaguje błąd wstecz sieci
        /// </summary>
        /// <param name="_learningFactor"></param>
        /// <param name="_momentumFactor"></param>
        public void updateWeights(double _learnFactor, double _momentum)
        {
            // Pętla po warstwach
            for (int i = 1; i < layers.Count; i++)
            {
                Layer prevLayer = layers[i - 1];
                Layer actualLayer = layers[i];

                for (int j = 0; j < actualLayer.neurons.Count; j++)
                {
                    Neuron neuron = actualLayer.neurons[j];

                    for (int k = 0; k < neuron.weights.Length; k++)
                    {
                        double tmpValue = 0.0d;
                        if (k >= prevLayer.neurons.Count)
                            tmpValue = 1.0d;
                        else
                            tmpValue = prevLayer.neurons[k].outputValue;

                        double derivative = neuron.deriative;
                        double delta = derivative * tmpValue * _learnFactor * neuron.error;
                        double newWeight = neuron.weights[k] + delta +  _momentum * neuron.prevDelta[k];
                        neuron.prevDelta[k] = delta;
                        neuron.weights[k] = newWeight;
                    }
                }
            }
        }

        public void DEL_uploadWeights(double _learningFactor, double _momentumFactor)
        {
            // Iteracja po warstwach - od ostatniej
            for (int i = layers.Count - 1; i >= 0; i--)
            {
                Layer currentLayer = layers[i];

                // Iteracja po Neuronach
                for (int j = 0; j < currentLayer.neurons.Count; j++)
                {
                    Neuron currentNeuron = currentLayer.neurons[j];

                    // Iteracja po wagach
                    for (int k = 0; k < currentNeuron.weights.Length; k++)
                    {
                        if (currentLayer == layers.Last()) // Jesli ostatnia warstwa
                        {
                            currentNeuron.prevWeights[k] = currentNeuron.weights[k];
                            currentNeuron.weights[k] += _learningFactor * currentNeuron.error *
                                                       currentNeuron.deriative * currentNeuron.input[k];
                        }
                        else // Pozostałe warstwy
                        {
                            Layer prevLayer = layers[i + 1];
                            double d = 0.0;
                            double sumWD = 0.0;

                            // Pętla po neuronach warstwy poprzedniej
                            for (int l = 0; l < prevLayer.neurons.Count; l++)
                            {
                                Neuron prevNeuron = prevLayer.neurons[l];

                                if (prevLayer == layers.Last()) // Jesli poprzednia warstwa jest warstwą ostatnią
                                {
                                    d = prevNeuron.deriative * prevNeuron.error;
                                }
                                else
                                {
                                    // Pętla po wagach neuronu poprzedniego
                                    for (int m = 0; m < prevNeuron.prevWeights.Length; m++)
                                    {
                                        sumWD += prevNeuron.prevWeights[m] * prevNeuron.error;
                                    }

                                    d = currentNeuron.deriative * sumWD;
                                }
                            }

                            currentNeuron.prevWeights[k] = currentNeuron.weights[k];
                            currentNeuron.weights[k] += _learningFactor * d * currentNeuron.input[k];
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            string ret;

            ret = "Zainicjowano następujący perceptron: \n";

            foreach (Layer layer in layers)
            {
                ret += layer.ToString();
            }

            return ret;

        }
    }
}
