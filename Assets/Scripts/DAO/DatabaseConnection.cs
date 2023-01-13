using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAO
{
    /// <summary>
    /// Connection class for MySQL
    /// </summary>
    public class DatabaseConnection
    {
        // || Config

        private const string SERVER = "localhost";
        private const string DATABASE = "unity_mysql";
        private const string UID = "root";
        private const string PWD = "";
        private const int PORT = 3306;

        // || Cached

        private MySqlCommand command;
        private IDataReader reader;

        // || Properties

        public static MySqlConnection Connection { get; private set; }

        public DatabaseConnection()
        {
            try
            {
                if (Connection == null)
                {
                    Connection = new MySqlConnection(BuildConnectionString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Build connection string for MySQL
        /// </summary>
        private static string BuildConnectionString() => $" SERVER = {SERVER}; DATABASE = {DATABASE}; PORT = {PORT}; UID = {UID}; PWD = {PWD};";

        /// <summary>
        /// Execute given query (SELECT etc)
        /// </summary>
        /// <param name="query"> Query to be executed </param>
        /// <returns> Reader with data </returns>
        protected IDataReader ExecuteQuery(string query, Dictionary<string, object> keyValuePairs = null)
        {
            Connection.Open();
            command = new MySqlCommand(query, Connection);

            if (keyValuePairs != null)
            {
                foreach (var kv in keyValuePairs)
                {
                    command.Parameters.AddWithValue(kv.Key, kv.Value);
                }
            }

            reader = command.ExecuteReader();
            return reader;
        }

        /// <summary>
        /// Execute given query (INSERT, UPDATE, DELETE)
        /// </summary>
        /// <param name="query"> Query to be executed </param>
        /// <param name="keyValuePairs"> KeyValues to be binded </param>
        /// <returns> How many rows are affected </returns>
        protected int ExecuteNonQuery(string query, Dictionary<string, object> keyValuePairs)
        {
            Connection.Open();

            command = new MySqlCommand(query, Connection);
            foreach (var kv in keyValuePairs)
            {
                command.Parameters.AddWithValue(kv.Key, kv.Value);
            }

            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// Close connection with MySQL
        /// </summary>
        protected void CloseConnection()
        {
            if (reader != null)
            {
                reader.Dispose();
            }

            if (command != null)
            {
                command.Dispose();
            }

            if (Connection != null)
            {
                Connection.Close();
            }
        }
    }
}