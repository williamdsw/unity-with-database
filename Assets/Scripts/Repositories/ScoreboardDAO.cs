using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ScoreboardDAO
{
    private DatabaseConnection databaseConnection = new DatabaseConnection();

    public ScoreboardDAO() { }

    /// <summary>
    /// Insert scoreboard data
    /// </summary>
    public bool Insert(Scoreboard model)
    {
        try
        {
            using (MySqlConnection connection = databaseConnection.GetConnection())
            {
                connection.Open();

                // Command
                StringBuilder sql = new StringBuilder();
                sql.Append(" insert into SCOREBOARD (USERNAME, SCORE, SCORE_DATE) ");
                sql.Append(" values (@USERNAME, @SCORE, @SCORE_DATE) ");

                // Parameters
                MySqlCommand command = new MySqlCommand(sql.ToString(), connection);
                command.Parameters.AddWithValue("@USERNAME", model.User);
                command.Parameters.AddWithValue("@SCORE", model.Score);
                command.Parameters.AddWithValue("@SCORE_DATE", model.Moment);

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
            using (MySqlConnection connection = databaseConnection.GetConnection())
            {
                connection.Open();

                // Command
                StringBuilder sql = new StringBuilder();
                sql.Append(" update SCOREBOARD ");
                sql.Append(" set USERNAME = @USERNAME, ");
                sql.Append(" SCORE = @SCORE, ");
                sql.Append(" SCORE_DATE = @SCORE_DATE ");
                sql.Append(" where SCORE_ID = @SCORE_ID ");

                // Parameters
                MySqlCommand command = new MySqlCommand(sql.ToString(), connection);
                command.Parameters.AddWithValue("@USERNAME", model.User);
                command.Parameters.AddWithValue("@SCORE", model.Score);
                command.Parameters.AddWithValue("@SCORE_DATE", model.Moment);
                command.Parameters.AddWithValue("@SCORE_ID", model.Id);

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

            using (MySqlConnection connection = databaseConnection.GetConnection())
            {
                connection.Open();

                // Command
                string sql = " delete from SCOREBOARD where SCORE_ID = @SCORE_ID ";
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@SCORE_ID", id);

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
            using (MySqlConnection connection = databaseConnection.GetConnection())
            {
                connection.Open();

                // Command
                StringBuilder sql = new StringBuilder();
                sql.Append(" select SCORE_ID, USERNAME, SCORE, SCORE_DATE ");
                sql.Append(" from SCOREBOARD ");
                sql.Append(" order by SCORE desc ");

                // Read data
                MySqlCommand command = new MySqlCommand(sql.ToString(), connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<Scoreboard> models = new List<Scoreboard>();
                    while (reader.Read())
                    {
                        Scoreboard model = new Scoreboard();
                        model.Id = reader.GetInt16("SCORE_ID");
                        model.User = reader.GetString("USERNAME");
                        model.Score = reader.GetDecimal("SCORE");
                        model.Moment = reader.GetDateTime("SCORE_DATE");
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
            using (MySqlConnection connection = databaseConnection.GetConnection())
            {
                connection.Open();

                // Command
                StringBuilder sql = new StringBuilder();
                sql.Append(" select SCORE_ID, USERNAME, SCORE, SCORE_DATE ");
                sql.Append(" from SCOREBOARD ");
                sql.Append(" where SCORE_ID = @SCORE_ID ");

                // Params
                MySqlCommand command = new MySqlCommand(sql.ToString(), connection);
                command.Parameters.AddWithValue("@SCORE_ID", id);

                // Read data
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    Scoreboard model = new Scoreboard();
                    if (reader.Read())
                    {
                        model.Id = reader.GetInt16("SCORE_ID");
                        model.User = reader.GetString("USERNAME");
                        model.Score = reader.GetDecimal("SCORE");
                        model.Moment = reader.GetDateTime("SCORE_DATE");
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