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
        private double[] weights;

        public int output { get; private set; }

        /// <summary>
        /// Czy jest waga biasu
        /// </summary>
        private bool isBiasWeights;

        /// <summary>
        /// Różnice między błędem a wartością wyjściową
        /// </summary>
        public double delta { get; set; }

        public double[] prevDelta { get; set; }

        /// <summary>
        /// Wartość błędu
        /// </summary>
        private double error { get; set; }
        #endregion

        /// <summary>
        /// Konstruktor przyjmujący funkcję aktywacji oraz ilość wejśc sieci.
        /// </summary>
        /// <param name="_activateFunction"> Funkcja aktywacji </param>
        /// <param name="_inputAmount"> Ilość wejść </param>
        public Neuron(IActivateFunction _activateFunction, int _inputAmount, int _id, bool _isBias = false)
        {
            activateFunction = _activateFunction;
            weights = new double[_inputAmount + (_isBias ? 1 : 0)];
            prevDelta = new double[_inputAmount + (_isBias ? 1 : 0)];
            id = _id;
            isBiasWeights = _isBias;
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

        /// <summary>
        /// Funkcja zwraca wyjście neuronu.
        /// </summary>
        /// <param name="_input"> Wektor wejściowy </param>
        /// <returns></returns>
        public int neuronOutput(int[] _input)
        {
            output = activateFunction.getNeuronOutput(_input, weights, id);

            return output;
        }

        public void neuronError(int _neuronOutput, int _targetValue)
        {
            this.setError(activateFunction.computeError(_neuronOutput, _targetValue));
        }

        #region Get/Set error
        public void setError(double _err)
        {
            error = _err;
        }

        public double getError()
        {
            return error;
        }
        #endregion

        public double getWeight(int _idx)
        {
            return weights[_idx];
        }

        public double[] getWeights()
        {
            return weights;
        }

        public double getNeuronDeriative()
        {
            return activateFunction.getNeuronDeriativeOutput();
        }

        public void setPrevDelta(int _idx, double _value)
        {
            this.prevDelta[_idx] = _value;
        }

        public void setWeight(int _idx, double _value)
        {
            this.weights[_idx] = _value;
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
