using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccess
{
    public class DatabaseManager
    {
        private string databaseName = "SIS_GYM";
        public bool BackupDatabase( string backupPath)
        {
            string folder = System.IO.Path.GetDirectoryName(backupPath);
            if (!System.IO.Directory.Exists(folder))
                System.IO.Directory.CreateDirectory(folder);

            string query = $@"
            BACKUP DATABASE [{databaseName}] 
            TO DISK = '{backupPath}' 
            WITH FORMAT, 
                 NAME = 'Respaldo completo - {databaseName}', 
                 DESCRIPTION = 'Respaldo generado el {DateTime.Now}',
                 STATS = 10";

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandTimeout = 300; // 5 minutos
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool RestoreDatabase(string backupPath)
        {
            string killConnections = $@"
        ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";

            string restoreQuery = $@"
        RESTORE DATABASE [{databaseName}] 
        FROM DISK = '{backupPath}' 
        WITH REPLACE, STATS = 10;
        ALTER DATABASE [{databaseName}] SET MULTI_USER;";

            try
            {
                string masterConn = Conexion.ConnectionString.Replace(databaseName, "master");

                using (SqlConnection conn = new SqlConnection(masterConn))
                {
                    conn.Open();

                    // Cerrar conexiones activas
                    using (SqlCommand cmd1 = new SqlCommand(killConnections, conn))
                    {
                        cmd1.CommandTimeout = 300;
                        cmd1.ExecuteNonQuery();
                    }

                    // Restaurar
                    using (SqlCommand cmd2 = new SqlCommand(restoreQuery, conn))
                    {
                        cmd2.CommandTimeout = 300;
                        cmd2.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
