using MySql.Data.MySqlClient;

namespace DatabaseConnectionADO.Infrastructure
{
    public class DbConnection
    {
        //SqlConnection Class Instance
        MySqlConnection conn = new MySqlConnection();

        public DbConnection()
        {
            conn.ConnectionString = "server=localhost;database=NetStudiesDB;uid=root;pwd=MySecretPassword;";
            //Change the Password according to your Database
        }

        public MySqlConnection Connect()
        {
            if (conn.State == System.Data.ConnectionState.Closed) 
            {
                conn.Open();
            }

            return conn;
        }

        public void Disconnect()
        {
            if(conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
