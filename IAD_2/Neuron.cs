﻿using System;
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
        /// Funkcja aktywacji - dependency injection
        /// </summary>
        private IActivateFunction activateFunction; 

        /// <summary>
        /// Wektor wag neuronu
        /// </summary>
        private double[] weights;

        /// <summary>
        /// Numer neuronu w warstwie - zmienna pomocnicza.
        /// Neuron powielający po otrzymaniu wektora wejściowego zwróci element z indeksem id.
        /// </summary>
        private int id;
        #endregion

        /// <summary>
        /// Konstruktor przyjmujący funkcję aktywacji oraz ilość wejśc sieci.
        /// </summary>
        /// <param name="_activateFunction"> Funkcja aktywacji </param>
        /// <param name="_inputAmount"> Ilość wejść </param>
        public Neuron(IActivateFunction _activateFunction, int _inputAmount, int _id)
        {
            activateFunction = _activateFunction;
            weights = new double[_inputAmount];
            id = _id;
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
            return activateFunction.getNeuronOutput(_input, weights, id);
        }

        public override string ToString()
        {
            string ret;

            ret = String.Format("   Neuron {0} posiada następujące wagi: \n", id);

            for(int i=0; i<weights.Length; i++)
            {
                ret += String.Format("      - waga {0} : {1} \n", i, weights[i]);
            }

            return ret;
        }
    }
}
