using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikrofalaSystem
{
    public class Setting : TableModel
    {
        public string? potrawa;
        public int moc;
        public int czas;

        public Setting()
        {
        }

        public Setting(int moc, int czas)
        {
            this.moc = moc;
            this.czas = czas;
        }

        public Setting(string potrawa, int moc, int czas)
        {
            this.potrawa = potrawa;
            this.moc = moc;
            this.czas = czas;
        }

        public override string GetCSVLine()
        {
            return $"{potrawa},{moc},{czas}";
        }

        public override TableModel ParseCSVLine(string a)
        {
            string[] splt = a.Split(",");
            return new Setting(
                potrawa = splt[0],
                moc = int.Parse(splt[1]),
                czas = int.Parse(splt[2])
            );
        }
    }
}
