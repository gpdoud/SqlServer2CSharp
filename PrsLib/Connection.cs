using System;

using Microsoft.Data.SqlClient;

namespace PrsLib {

    public class Connection {

        public SqlConnection sqlConnection { get; set; } = null;

        public void Connect() {
            sqlConnection.Open();
            if(sqlConnection.State != System.Data.ConnectionState.Open) {
                throw new Exception("SqlConnection failed to open!");
            }
            return;
        }
        public void Disconnect() {
            if (sqlConnection == null) return;
            sqlConnection.Close();
            sqlConnection = null;
        }

        public Connection(string server, string database) {
            var connectionString = $"server={server}\\sqlexpress;database={database};trusted_connection=true;";
            sqlConnection = new SqlConnection(connectionString);
        }
    }
}
