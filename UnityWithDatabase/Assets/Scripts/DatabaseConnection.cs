using System;
using MySql.Data.MySqlClient;

public class DatabaseConnection
{
    // Config
    private const string SERVER = "localhost";
    private const string DATABASE = "unity_mysql";
    private const string USER_ID = "root";
    private const string PASSWORD = "";
    private const int PORT = 3306;

    // Cached
    private MySqlConnection connection;

    //----------------------------------------------------------------------------------//
    // GETTERS / SETTERS

    public MySqlConnection GetConnection () { return this.connection; }

    //----------------------------------------------------------------------------------//

    public DatabaseConnection ()
    {
        if (connection == null)
        {
            connection = new MySqlConnection (BuildConnectionString ());
        }
    }

    //----------------------------------------------------------------------------------//

    private string BuildConnectionString ()
    {
        return $" SERVER = {SERVER}; DATABASE = {DATABASE}; PORT = {PORT}; " + 
               $" USER ID = {USER_ID}; PASSWORD = {PASSWORD}; ";
    }
}