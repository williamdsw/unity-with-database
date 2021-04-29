using Model;
using MySql.Data.MySqlClient;
using Others;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DAO
{
    public class ScoreboardDAO
    {
        /// <summary>
        /// Insert scoreboard data
        /// </summary>
        public bool Insert(Scoreboard model)
        {
            try
            {
                using (MySqlConnection connection = DatabaseConnection.Connection)
                {
                    connection.Open();

                    // Parameters
                    MySqlCommand command = new MySqlCommand(Configuration.Queries.Scoreboard.Insert, connection);
                    command.Parameters.AddWithValue("@USER", model.User);
                    command.Parameters.AddWithValue("@SCORE", model.Score);
                    command.Parameters.AddWithValue("@MOMENT", model.Moment);

                    return (command.ExecuteNonQuery() == 1);
                }
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("Insert -> {0}", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Update scoreboard data
        /// </summary>
        public bool Update(Scoreboard model)
        {
            try
            {
                using (MySqlConnection connection = DatabaseConnection.Connection)
                {
                    connection.Open();

                    // Parameters
                    MySqlCommand command = new MySqlCommand(Configuration.Queries.Scoreboard.Update, connection);
                    command.Parameters.AddWithValue("@USER", model.User);
                    command.Parameters.AddWithValue("@SCORE", model.Score);
                    command.Parameters.AddWithValue("@MOMENT", model.Moment);
                    command.Parameters.AddWithValue("@ID", model.Id);

                    return (command.ExecuteNonQuery() == 1);
                }
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("Update -> {0}", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Delete scoreboard data
        /// </summary>
        public bool DeleteById(int id)
        {
            try
            {
                if (id <= 0) throw new Exception(string.Format("Invalid Id -> {0}", id));

                using (MySqlConnection connection = DatabaseConnection.Connection)
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand(Configuration.Queries.Scoreboard.Delete, connection);
                    command.Parameters.AddWithValue("@ID", id);

                    return (command.ExecuteNonQuery() == 1);
                }
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("DeleteById -> {0}", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// List all scoreboard data
        /// </summary>
        public List<Scoreboard> ListAll()
        {
            try
            {
                using (MySqlConnection connection = DatabaseConnection.Connection)
                {
                    connection.Open();

                    // Read data
                    MySqlCommand command = new MySqlCommand(Configuration.Queries.Scoreboard.ListAll, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        List<Scoreboard> models = new List<Scoreboard>();
                        while (reader.Read())
                        {
                            Scoreboard model = new Scoreboard();
                            model.Id = reader.GetInt16("ID");
                            model.User = reader.GetString("USER");
                            model.Score = reader.GetDecimal("SCORE");
                            model.Moment = reader.GetDateTime("MOMENT");
                            models.Add(model);
                        }

                        return models;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("ListAll -> {0}", ex.Message);
                return new List<Scoreboard>();
            }
        }

        /// <summary>
        /// Recover scoreboard data by ID
        /// </summary>
        public Scoreboard GetById(int id)
        {
            try
            {
                if (id <= 0) throw new Exception(string.Format("Invalid Id -> {0}", id));

                using (MySqlConnection connection = DatabaseConnection.Connection)
                {
                    connection.Open();

                    // Params
                    MySqlCommand command = new MySqlCommand(Configuration.Queries.Scoreboard.GetById, connection);
                    command.Parameters.AddWithValue("@ID", id);

                    // Read data
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        Scoreboard model = new Scoreboard();
                        if (reader.Read())
                        {
                            model.Id = reader.GetInt16("ID");
                            model.User = reader.GetString("USER");
                            model.Score = reader.GetDecimal("SCORE");
                            model.Moment = reader.GetDateTime("MOMENT");
                        }

                        return model;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("GetById -> {0}", ex.Message);
                return null;
            }
        }
    }
}