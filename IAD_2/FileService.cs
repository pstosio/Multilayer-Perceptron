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
        public static void saveToFile(string _path, string _line)
        {
            using (StreamWriter writer = new StreamWriter(_path, true)) // true - appending
            {
                writer.WriteLine(_line);
            }
        }

        public static void importWeight(string _path)
        {
            
        }
    }
}
