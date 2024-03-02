using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikrofalaSystem
{
    public class DB
    {
        public List<Setting> dbSettings;

        public DB()
        {
            dbSettings = new Setting().ReadFromCSV<Setting>("db_settings.txt");
        }

        internal void SaveChanges()
        {
            Setting.WriteToCSV("db_settings.txt", dbSettings);
        }
    }
}
