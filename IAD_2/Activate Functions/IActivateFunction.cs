using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_2
{
    public interface IActivateFunction
    {
        /// <summary>
        /// Funkcja inicjuje liczenie sumatora i wartości wyjściowej neuronu
        /// </summary>
        /// <param name="_input"></param>
        /// <param name="_weights"></param>
        /// <returns></returns>
        void initFunction(double[] _input, double[] _weights, int _id = 0);

        /// <summary>
        /// Funkcja zwraca sumator (sumę iloczynu wektora wejściowego i wag neuronu)
        /// </summary>
        double getNeuronAdder();

        /// <summary>
        /// Funkcja liczy wyjście danego neuronu obliczone z funkcji aktywacji
        /// </summary>
        /// <returns></returns>
        double getNeuronOutputValue();

        /// <summary>
        /// Funkca liczy pochodną wyjścia 
        /// </summary>
        /// <returns></returns>
        double getNeuronDeriativeOutput();
    }
}
