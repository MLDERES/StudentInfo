using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExample
{
    class SQLHelper
    {
        private string _connectionString = "";
        public string DBName { get; set; }
        public string ServerName { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string ConnectionString {
            get {
                return (_connectionString == "") ? string.Format("user id={0};" +
                        "password={1};" +
                        "database={2};" +
                        "server={3};" +
                        "connection timeout=30",UserId,Password,DBName,ServerName):
                _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }
        // Test to see if the connection can be made
        public bool TestConnection()
        {
            return 1 ==(int)this.ExecuteScalar("select 1",CommandType.Text);
        }
        // Using the current connection string command, and then execute the command with non query.  
        public Int32 ExecuteNonQuery(String commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            return SQLHelper.ExecuteNonQuery(ConnectionString, commandText, commandType, parameters);
        }

        //public Int64 RunQueryGetInt(String sql, params SqlParameter[] parameters) 
        //{
        //    return SQLHelper.ExecuteScalar(ConnectionString, sql, CommandType.Text, parameters);
        //}
        // Set the connection, command, and then execute the command and only return one value.  
        public Object ExecuteScalar(String commandText,CommandType commandType, params SqlParameter[] parameters)
        {
            return SQLHelper.ExecuteScalar(ConnectionString, commandText, commandType, parameters);
        }

        // This is the static version of ExecuteNonQuery
        public static Int32 ExecuteNonQuery(String connectionString, String commandText,
            CommandType commandType, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    // There're three command types: StoredProcedure, Text, TableDirect. The TableDirect   
                    // type is only for OLE DB.    
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Set the connection, command, and then execute the command and only return one value.  
        public static Object ExecuteScalar(String connectionString, String commandText,
            CommandType commandType, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        // Set the connection, command, and then execute the command with query and return the reader.  
        public static SqlDataReader ExecuteReader(String connectionString, String commandText,
            CommandType commandType, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(parameters);

                conn.Open();
                // When using CommandBehavior.CloseConnection, the connection will be closed when the   
                // IDataReader is closed.  
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                return reader;
            }
        }

        // Set the connection, command, and then execute the command with query and return the reader.  
        public DataTable GetDataTable(String commandText,
            CommandType commandType, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(this.ConnectionString);
            DataTable retVal = new DataTable();
            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(parameters);

                conn.Open();
                // Create a data adapter object
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(retVal);
                return retVal;
            }
 
        }
     }
}
