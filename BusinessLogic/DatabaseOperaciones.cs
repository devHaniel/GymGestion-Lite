using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class DatabaseOperaciones
    {

        private readonly DatabaseManager databaseManager;
        
        public DatabaseOperaciones()
        {
            databaseManager = new DatabaseManager();
        }

        public bool BackupDatabase( string backupRuta)
        {
            return databaseManager.BackupDatabase( backupRuta);
        }

        public bool RestoreDatabase(string backupRuta)
        {
            return databaseManager.RestoreDatabase(backupRuta);
        }
    }
}
