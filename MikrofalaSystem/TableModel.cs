using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikrofalaSystem
{
    public abstract class TableModel
    {

        public abstract string GetCSVLine();
        public abstract TableModel ParseCSVLine(string a);

        public static void WriteToCSV<T>(string path, List<T> models) where T : TableModel
        {
            File.WriteAllLines(path, from x in models
                                     select x.GetCSVLine());
        }

        public List<T> ReadFromCSV<T>(string path) where T : TableModel
        {
            return File.Exists(path) ? (from x in File.ReadAllLines(path)
                                        select (T)ParseCSVLine(x)).ToList()
            : [];
        }
    }
}
