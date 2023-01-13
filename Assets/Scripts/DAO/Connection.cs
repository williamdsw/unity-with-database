using Mono.Data.Sqlite;
using Others;
using System;
using System.Data;

namespace DAO
{
    /// <summary>
    /// Connection class for SQLite
    /// </summary>
    public class Connection
    {
        private IDbConnection connection;
        private IDbCommand command;
        private IDbTransaction transaction;
        private IDataReader reader;

        public Connection()
        {
            try
            {
                string path = string.Format("URI=file:{0}", Configuration.Properties.DatabasePath);
                connection = new SqliteConnection(path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Execute given query (SELECT etc)
        /// </summary>
        /// <param name="query"> Query to be executed </param>
        /// <returns> Reader with data </returns>
        protected IDataReader ExecuteQuery(string query)
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = query;
            reader = command.ExecuteReader();
            return reader;
        }

        /// <summary>
        /// Execute given query (INSERT, UPDATE, DELETE)
        /// </summary>
        /// <param name="query"> Query to be executed </param>
        /// <returns> How many rows are affected </returns>
        protected long ExecuteScalar(string query)
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = query;
            return (long)command.ExecuteScalar();
        }

        /// <summary>
        /// Execute given query (INSERT, UPDATE, DELETE)
        /// </summary>
        /// <param name="query"> Query to be executed </param>
        /// <returns> How many rows are affected </returns>
        protected int ExecuteNonQuery(string query)
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = query;
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// Close connection with SQLite
        /// </summary>
        protected void CloseConnection()
        {
            if (reader != null)
            {
                reader.Close();
            }

            if (command != null)
            {
                command.Dispose();
            }

            if (connection != null)
            {
                connection.Close();
            }
        }
    }
}