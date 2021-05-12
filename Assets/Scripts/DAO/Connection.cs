using Mono.Data.Sqlite;
using Others;
using System.Data;
using UnityEngine;

namespace DAO
{
    public class Connection
    {
        private IDbConnection connection;
        private IDbCommand command;
        private IDbTransaction transaction;
        private IDataReader reader;

        public Connection()
        {
            string path = string.Format("URI=file:{0}", Configuration.Properties.DatabasePath);
            connection = new SqliteConnection(path);
        }

        protected IDataReader ExecuteQuery(string query)
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = query;
            reader = command.ExecuteReader();
            return reader;
        }

        protected long ExecuteScalar(string query)
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = query;
            return (long)command.ExecuteScalar();
        }

        protected int ExecuteNonQuery(string query)
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = query;
            return command.ExecuteNonQuery();
        }

        protected void CloseConnection()
        {
            if (reader != null)
            {
                reader.Close();
            }

            command.Dispose();
            connection.Close();
        }
    }
}