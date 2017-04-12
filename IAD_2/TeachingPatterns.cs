using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_2
{
    public class RandomTeachingPattern
    {
        int pattern = 0;
        public RandomTeachingPattern()
        {
            Random rnd = new Random();
            pattern = rnd.Next(1, 4);
        }

        public double[] getTeachingPattern()
        {
            switch(pattern)
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
    public static class TeachingPattern_1
    {
        public static int[] input = new int[4] { 1, 0, 0, 0 };
        public static int[] output = new int[4] { 1, 0, 0, 0 };
    }

    public static class TeachingPattern_2
    {
        public static int[] input = new int[4] { 0, 1, 0, 0 };
        public static int[] output = new int[4] { 0, 1, 0, 0 };
    }

    public static class TeachingPattern_3
    {
        public static int[] input = new int[4] { 0, 0, 1, 0 };
        public static int[] output = new int[4] { 0, 0, 1, 0 };
    }

    public static class TeachingPattern_4
    {
        public static int[] input = new int[4] { 0, 0, 0, 1 };
        public static int[] output = new int[4] { 0, 0, 0, 1 };
    }
}
