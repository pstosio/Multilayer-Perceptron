﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        public List<Layer> layers;

        /// <summary>
        /// Błąd średniokwadratowy sieci
        /// </summary>
        public double sumSquaredError;

        /// <summary>
        /// Bład dla wszystkich zestawów
        /// </summary>
        public double sumSquaredErrorTotal;

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
                neuron.error = (_expectedOutput[i] - neuron.outputValue) * neuron.deriative;
            }

            for (int i = layers.Count - 2; i > 0; i--)
            {
                Layer nextLayer = layers[i + 1];
                Layer actual = layers[i];

                for (int j = 0; j < actual.neurons.Count; j++)
                {
                    bool hasBias = actual.neurons[j].isBias;
                    int shift = hasBias ? 1 : 0;
                    double errTmp = 0.0d;

                    foreach (Neuron neuronFromNextLayer in nextLayer.neurons)
                    {
                        errTmp += neuronFromNextLayer.error * neuronFromNextLayer.weights[j + shift]; // <== Trzeba przesunąć jeżeli neuron z kolejnej warstwy posiada bias
                    }
                    actual.neurons[j].error = errTmp * actual.neurons[j].deriative;
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
                double expTmp = _expected[i];
                double outTmp = lastLayer.output[i];
                sumSquaredError += Math.Pow((expTmp - outTmp), 2);
            }

            sumSquaredErrorTotal += sumSquaredError;
            sumSquaredError = 0.0d;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void estimateSumSquaredError()
        {
            sumSquaredErrorTotal = sumSquaredErrorTotal / 2;
        }

        public double getTotalSumSquaredError()
        {
            return sumSquaredErrorTotal;
        }

        public void resetSumSquaredError()
        {
            sumSquaredErrorTotal = 0.0d;
        }

        /// <summary>
        /// Zapis wag do pliku
        /// </summary>
        public void saveWeightsToFile()
        {
            string path = @"C:\Users\Peter\Desktop\IAD\Zad 2\neuronWeights.txt";
            for (int i = 1; i <= layers.Count; i++)
            {
                Layer actual = layers[i - 1];
                for (int j = 1; j <= actual.neurons.Count; j++)
                {
                    Neuron n = actual.neurons[j - 1];

                    for (int k = 0; k < n.weights.Length; k++)
                    {
                        double tmpWeight = n.weights[k];
                        //                       Layer, neuron, weight, value
                        string line = String.Format("{0} {1} {2} {3}", i, j, k, tmpWeight);
                        FileService.saveToFile(path, line);
                    }
                }
            }
        }

        /// <summary>
        /// Wczytanie wag neuronów z pliku
        /// </summary>
        public void uploadWeights()
        {
            string path = @"C:\Users\Peter\Desktop\IAD\Zad 2\neuronWeights.txt";
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;

                    // Read and display lines from the file until 
                    // the end of the file is reached. 
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] splittedLine = splittedLine = line.Split(' '); // <-- separator
                        int layerNum = Convert.ToInt32(splittedLine[0]) - 1; // <== Warstwa
                        int neuronNum = Convert.ToInt32(splittedLine[1]) - 1; // <== Neuron
                        int weightNum = Convert.ToInt32(splittedLine[2]); // <== Waga
                        double weightValue = Convert.ToDouble(splittedLine[3]); // <== Wartość 

                        this.layers[layerNum].neurons[neuronNum].weights[weightNum] = weightValue;
                    }
                }
            }
            catch (Exception e)
            {

                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Zapis globalnego błędu średniowadratowego do pliku
        /// </summary>
        public void saveErrorToFile()
        {
            string path = @"C:\Users\Peter\Desktop\IAD\Zad 2\errorLog.txt";
            FileService.saveToFile(path, Convert.ToString(sumSquaredErrorTotal));
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
                Layer actualLayer = layers[i];

                for (int j = 0; j < actualLayer.neurons.Count; j++)
                {
                    Neuron neuron = actualLayer.neurons[j];

                    for (int k = 0; k < neuron.weights.Length; k++)
                    {
                        double delta = _learnFactor * neuron.error * neuron.input[k] + _momentum * neuron.prevDelta[k];
                        neuron.prevDelta[k] = delta;

                        double newWeight = neuron.weights[k] + delta;
                        neuron.weights[k] = newWeight;
                    }
                }
            }
        }

        [Obsolete("Wrong function")]
        public void uploadWeights(double _learningFactor, double _momentumFactor)
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

            /*
            Layer firstLayer = layers.First();
            ret += "Wektor wejściowy: \n";
            for (int i = 0; i < firstLayer.input.Length; i++)
            {
                ret += firstLayer.input[i] + " ";
            }
            */
            return ret;

        }
    }
}
