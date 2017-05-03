using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_2
{
    public class ClassificationTeachingSet
    {
        //  4 caechy Irisów
        List<double[]> setosa;
        List<double[]> versicolor;
        List<double[]> virginica;

        int[] setosaOrder;
        int[] versicolorOrder;
        int[] virginicaOrder;

        public int counter;

        public ClassificationTeachingSet()
        {
            counter = 0;

            //  4 cechy Irisów
            setosa = new List<double[]>();
            versicolor = new List<double[]>();
            virginica = new List<double[]>();

            this.uploadIrisDataSet();

            setosaOrder = this.getRandOrder(setosa.Count);
            versicolorOrder = this.getRandOrder(versicolor.Count);
            virginicaOrder = this.getRandOrder(virginica.Count);
        }

        public double[] getRandomTeachingPattern(int _featureNum, int _counter)
        {
            switch (_featureNum)
            {
                case 1:
                    return setosa[setosaOrder[_counter] - 1];
                case 2:
                    return versicolor[versicolorOrder[_counter] - 1];
                case 3:
                    return virginica[virginicaOrder[_counter] - 1];
            }

            return new double[4];
        }

        public double[] getTeachingPattern(int _featureNum, int _counter)
        {
            switch (_featureNum)
            {
                case 1:
                    return setosa[_counter];
                case 2:
                    return versicolor[_counter];
                case 3:
                    return virginica[_counter];
            }

            return new double[4];
        }

        public double[] getOutput(int _featureNum)
        {
            switch (_featureNum)
            {
                case 1:
                    return new double[3] { 1, 0, 0 };

                case 2:
                    return new double[3] { 0, 1, 0 };

                case 3:
                    return new double[3] { 0, 0, 1 };
            }

            return new double[3] { 0, 0, 0 };
        }

        private void uploadIrisDataSet()
        {
            string path = @"C:\Users\Peter\Desktop\IAD\Zad 2\iris_data.txt";
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
                        string[] splittedLine = splittedLine = line.Split(','); // <-- separator
                        double[] features = new double[4];

                        features[0] = Convert.ToDouble(splittedLine[0], System.Globalization.CultureInfo.InvariantCulture); // Długość Sepala
                        features[1] = Convert.ToDouble(splittedLine[1], System.Globalization.CultureInfo.InvariantCulture); // Szerokość Sepala
                        features[2] = Convert.ToDouble(splittedLine[2], System.Globalization.CultureInfo.InvariantCulture); // Długość Petala
                        features[3] = Convert.ToDouble(splittedLine[3], System.Globalization.CultureInfo.InvariantCulture); // Szerokość Petala

                        switch (splittedLine[4].Substring(5, 2))
                        {
                            case "se":
                                setosa.Add(features);
                                break;

                            case "ve":
                                versicolor.Add(features);
                                break;

                            case "vi":
                                virginica.Add(features);
                                break;
                        }

                        counter++;
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

        private int[] getRandOrder(int _value)
        {
            int k = _value;
            int n = _value;
            int[] ret = new int[_value];
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



