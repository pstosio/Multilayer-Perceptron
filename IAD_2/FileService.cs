using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IAD_2
{
    /// <summary>
    /// Klasa odpowiedzialna za wczytanie wag sieci z pliku oraz zapis do pliku
    /// </summary>
    public static class FileService
    {
        public static void saveToFile(double _line)
        {
            string path = @"C:\Users\Peter\Desktop\IAD\Zad 2\errorLog.txt";
            using (StreamWriter writer = new StreamWriter(path, true)) // true - appending
            {
                writer.WriteLine(_line);
            }
        }
    }
}
