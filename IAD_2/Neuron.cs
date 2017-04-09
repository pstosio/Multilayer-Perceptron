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
        private ActivateFunction activateFunction; 

        /// <summary>
        /// Wektor wag neuronu
        /// </summary>
        public double[] weights;

        /// <summary>
        /// Wartość sumatora neuronu
        /// </summary>
        public double outputAdder;

        /// <summary>
        /// Wartość wyjściowa neuronu
        /// </summary>
        public double output;

        /// <summary>
        /// Wartość błędu
        /// </summary>
        public double error;

        /// <summary>
        /// Czy jest waga biasu
        /// </summary>
        private bool isBiasWeights;

        /// <summary>
        /// Różnice między błędem a wartością wyjściową
        /// </summary>
        public double delta { get; set; }

        public double[] prevDelta { get; set; }
        #endregion

        /// <summary>
        /// Konstruktor przyjmujący funkcję aktywacji oraz ilość wejśc sieci.
        /// </summary>
        /// <param name="_activateFunction"> Funkcja aktywacji </param>
        /// <param name="_inputAmount"> Ilość wejść </param>
        public Neuron(ActivateFunction _activateFunction, int _inputAmount, int _id, bool _isBias = false)
        {
            activateFunction = _activateFunction;
            weights = new double[_inputAmount + (_isBias ? 1 : 0)];
            prevDelta = new double[_inputAmount + (_isBias ? 1 : 0)]; // todo: Ilość poprzednich błędów zależy ilości epok
            id = _id;
            isBiasWeights = _isBias;
        }

        public void process(double[] _input)
        {
            activateFunction.initFunction(_input, weights);

            outputAdder = activateFunction.adderValue;
            output = activateFunction.outputValue;
        }

        /// <summary>
        /// Funkcja losuje wagi neuronu z przedziału -1,1
        /// </summary>
        public void generateRandomWeights()
        {
            Random rand;

            for(int i=0; i<weights.Length; i++)
            {
                rand = new Random(Guid.NewGuid().GetHashCode());
                weights[i] = rand.NextDouble() * 2 - 1;
            }
        }

        public void neuronError(double _neuronOutput, int _targetValue)
        {
            error = activateFunction.computeError(_neuronOutput, _targetValue);
        }

        public double getWeight(int _idx)
        {
            return weights[_idx];
        }

        public double getNeuronDeriative()
        {
            return activateFunction.getNeuronDeriativeOutput();
        }


        public override string ToString()
        {
            string ret;
            
            ret = String.Format("   Neuron {0}, wagi: \n", id);

            for(int i=0; i<weights.Length; i++)
            {
                ret += String.Format("      - waga {0} : {1} \n", i, weights[i]);
            }

            return ret;
        }
    }
}
