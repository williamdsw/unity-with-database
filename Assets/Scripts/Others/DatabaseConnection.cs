using MySql.Data.MySqlClient;

namespace Others
{
    public class DatabaseConnection
    {
        // Config
        private const string SERVER = "localhost";
        private const string DATABASE = "unity_mysql";
        private const string USER_ID = "root";
        private const string PASSWORD = "";
        private const int PORT = 3306;

        // || Properties

        public static MySqlConnection Connection { get; private set; }

        static DatabaseConnection()
        {
            if (Connection == null)
            {
                Connection = new MySqlConnection(BuildConnectionString());
            }
        }

        private static string BuildConnectionString()
        {
            return string.Format(" SERVER = {0}; DATABASE = {1}; PORT = {2}; USER ID = {3}; PASSWORD = {4};", SERVER, DATABASE, PORT, USER_ID, PASSWORD);
        }
    }
}