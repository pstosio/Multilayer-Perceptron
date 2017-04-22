using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_2
{
    public class TeachingPatterns
    {
        int retPattern = 0;
        int[] order = new int[4];

        public TeachingPatterns()
        {
            order = this.getRandOrder();
        }

        public double[] getRandomTeachingPattern()
        {
            retPattern++;

            return this.getPattern(order[retPattern - 1]);
        }

        public double[] getTeachingPattern(int _patterNum)
        {
            return this.getPattern(_patterNum);
        }

        private double[] getPattern(int _num)
        {
            switch (_num)
            {
                case 1:
                    return new double[4] { 1, 0, 0, 0 };

                case 2:
                    return new double[4] { 0, 1, 0, 0 };

                case 3:
                    return new double[4] { 0, 0, 1, 0 };

                case 4:
                    return new double[4] { 0, 0, 0, 1 };
            }

            return new double[4] { 0, 0, 0, 0 };
        }

        private int[] getRandOrder()
        {
            int k = 4;
            int n = 4;
            int[] ret = new int[4];
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            // wypełnianie tablicy liczbami 1,2...n

            int[] numbers = new int[n];
            for (int i = 0; i < n; i++)
                numbers[i] = i + 1;
            // losowanie k liczb

            for (int i = 0; i < k; i++)
            {
                // tworzenie losowego indeksu pomiędzy 0 i n - 1
                int r = rand.Next(n);

                // wybieramy element z losowego miejsca
                ret[i] = numbers[r];

                // przeniesienia ostatniego elementu do miejsca z którego wzięliśmy
                numbers[r] = numbers[n - 1];
                n--;
            }

            return ret;
        }
    }
}
