using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_2
{
    public class TeachingPatterns
    {
        Random rnd;
        int pattern = 0;

        public double[] getRandomTeachingPattern()
        {
            rnd = new Random(Guid.NewGuid().GetHashCode());
            pattern = rnd.Next(1, 4);

            switch (pattern)
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

        public double[] getTeachingPattern(int _patterNum)
        {
            switch (_patterNum)
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
    }
}
