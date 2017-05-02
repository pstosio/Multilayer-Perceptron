using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace IAD_2
{
    /// <summary>
    /// Klasa odpowiadająca za instację pojedynczego nuronu
    /// </summary>
    public sealed class Neuron
    {
        #region Const
        /// <summary>
        /// Numer neuronu w warstwie - zmienna pomocnicza.
        /// Neuron powielający po otrzymaniu wektora wejściowego zwróci element z indeksem id.
        /// </summary>
        private int id;

        /// <summary>
        /// Funkcja aktywacji - dependency injection
        /// </summary>
        private IActivateFunction activateFunction;

        /// <summary>
        /// Wektor wag neuronu
        /// </summary>
        public double[] weights;

        /// <summary>
        /// Poprzednie wagi - wykorzystywane w procesie nauki
        /// </summary>
        public double[] prevWeights;

        /// <summary>
        /// Delty
        /// </summary>
        public double[] prevDelta;

        /// <summary>
        /// Czy jest waga biasu
        /// </summary>
        public bool isBias;

        /// <summary>
        /// Wektor wejściowy neuronu
        /// </summary>
        public double[] input;

        /// <summary>
        /// Wartość sumatora neuronu
        /// </summary>
        public double adder;

        /// <summary>
        /// Pochodna funkcji aktywacji
        /// </summary>
        public double deriative;

        /// <summary>
        /// Wartość wyjściowa neuronu
        /// </summary>
        public double outputValue;

        /// <summary>
        /// Wartość błędu
        /// </summary>
        public double error;
        #endregion

        /// <summary>
        /// Konstruktor przyjmujący funkcję aktywacji oraz ilość wejśc sieci.
        /// </summary>
        /// <param name="_activateFunction"> Funkcja aktywacji </param>
        /// <param name="_inputAmount"> Ilość wejść </param>
        public Neuron(IActivateFunction _activateFunction, int _inputAmount, int _id, bool _isBias)
        {
            id = _id;
            isBias = _isBias;
            activateFunction = _activateFunction;
            weights     = new double[_inputAmount + (isBias ? 1 : 0)];
            prevWeights = new double[_inputAmount + (isBias ? 1 : 0)];
            prevDelta   = new double[_inputAmount + (isBias ? 1 : 0)];
        }

        public void process(double[] _input)
        {
            if (isBias)
            {
                input = new double[_input.Length + 1];
                for (int i = 0; i < _input.Length + 1; i++)
                {
                    if (i == 0)
                        input[i] = 1; // Zerowa waga biasu na wejście otrzymuje zawsze wartość 1
                    else
                        input[i] = _input[i - 1];
                }
            }
            else
            {
                input = _input;
            }

            activateFunction.initFunction(input, weights, id);

            adder = activateFunction.getNeuronAdder();
            outputValue = activateFunction.getNeuronOutputValue();
            deriative = activateFunction.getNeuronDeriativeOutput();
        }

        /// <summary>
        /// Funkcja losuje wagi neuronu z przedziału -0.5, 0.5
        /// </summary>
        public void generateRandomWeights()
        {
            Random rand;
            for (int i = 0; i < weights.Length; i++)
            {
                rand = new Random(Guid.NewGuid().GetHashCode());
                weights[i] = rand.NextDouble() * (0.5 + 0.5) - 0.5; // * (maximum - minimum ) + minimum
            }
        }

        public override string ToString()
        {
            string ret;
            ret = String.Format("   Neuron {0}, wagi: \n", id);
            for (int i = 0; i < weights.Length; i++)
            {
                ret += String.Format("      - waga {0} : {1} \n", i, weights[i]);
            }
            ret += String.Format("Odpowiedź neuronu: {0} \n", outputValue);

            return ret;
        }
    }
}
