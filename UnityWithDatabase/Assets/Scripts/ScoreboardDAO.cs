using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

public class ScoreboardDAO
{
    private DatabaseConnection databaseConnection = new DatabaseConnection ();

    //-----------------------------------------------------------------------------------//

    public ScoreboardDAO () { }

    //-----------------------------------------------------------------------------------//

    /// <summary>
    /// Insert scoreboard data
    /// </summary>
    public bool InsertScoreboard (ScoreboardMODEL model, out string exceptionMessage)
    {
        bool hasExecuted = false;
        MySqlConnection connection = databaseConnection.GetConnection ();

        try
        {
            connection.Open ();

            // Command
            StringBuilder sql = new StringBuilder ();
            sql.Append (" INSERT INTO scoreboard (USERNAME, SCORE, SCORE_DATE) ");
            sql.Append (" VALUES (@USERNAME, @SCORE, @SCORE_DATE) ");

            // Parameters
            MySqlCommand command = new MySqlCommand (sql.ToString (), connection);
            command.Parameters.AddWithValue ("@USERNAME", model.Username);
            command.Parameters.AddWithValue ("@SCORE", model.Score);
            command.Parameters.AddWithValue ("@SCORE_DATE", model.ScoreDate);

            hasExecuted = (command.ExecuteNonQuery () == 1);
            exceptionMessage = "";
        }
        catch (Exception ex)
        {
            exceptionMessage = ex.Message;
        }
        finally
        {
            connection.Close ();
        }

        return hasExecuted;
    }

    /// <summary>
    /// Update scoreboard data
    /// </summary>
    public bool UpdateScoreboard (ScoreboardMODEL model, out string exceptionMessage)
    {
        exceptionMessage = "";
        bool hasExecuted = false;
        MySqlConnection connection = databaseConnection.GetConnection ();

        try
        {
            connection.Open ();

            // Command
            StringBuilder sql = new StringBuilder ();
            sql.Append (" UPDATE scoreboard ");
            sql.Append (" SET USERNAME = @USERNAME, ");
            sql.Append (" SCORE = @SCORE, ");
            sql.Append (" SCORE_DATE = @SCORE_DATE ");
            sql.Append (" WHERE SCORE_ID = @SCORE_ID ");

            // Parameters
            MySqlCommand command = new MySqlCommand (sql.ToString (), connection);
            command.Parameters.AddWithValue ("@USERNAME", model.Username);
            command.Parameters.AddWithValue ("@SCORE", model.Score);
            command.Parameters.AddWithValue ("@SCORE_DATE", model.ScoreDate);
            command.Parameters.AddWithValue ("@SCORE_ID", model.ScoreID);

            hasExecuted = (command.ExecuteNonQuery () == 1);
        }
        catch (Exception ex)
        {
            exceptionMessage = ex.Message;
        }
        finally
        {
            connection.Close ();
        }

        return hasExecuted;
    }

    /// <summary>
    /// Delete scoreboard data
    /// </summary>
    public bool Delete (int scoreID, out string exceptionMessage)
    {
        exceptionMessage = "";
        bool hasExecuted = false;
        MySqlConnection connection = databaseConnection.GetConnection ();

        try
        {
            // Cancels
            if (scoreID <= 0) { exceptionMessage = "Invalid ID"; return false; }

            connection.Open ();

            // Command
            StringBuilder sql = new StringBuilder ();
            sql.Append (" DELETE FROM scoreboard ");
            sql.Append (" WHERE SCORE_ID = @SCORE_ID ");

            // Parameters
            MySqlCommand command = new MySqlCommand (sql.ToString (), connection);
            command.Parameters.AddWithValue ("@SCORE_ID", scoreID);

            hasExecuted = (command.ExecuteNonQuery () == 1);
        }
        catch (Exception ex)
        {
            exceptionMessage = ex.Message;
        }
        finally
        {
            connection.Close ();
        }

        return hasExecuted;
    }

    /// <summary>
    /// List all scoreboard data
    /// </summary>
    public List<ScoreboardMODEL> ListScoreboard ()
    {
        List<ScoreboardMODEL> models = new List<ScoreboardMODEL> ();
        MySqlConnection connection = databaseConnection.GetConnection ();

        try
        {
            connection.Open ();

            // Command
            StringBuilder sql = new StringBuilder ();
            sql.Append (" SELECT SCORE_ID, USERNAME, SCORE, SCORE_DATE ");
            sql.Append (" FROM scoreboard ");
            sql.Append (" ORDER BY SCORE DESC ");

            // Read data
            MySqlCommand command = new MySqlCommand (sql.ToString (), connection);
            MySqlDataReader reader = command.ExecuteReader ();
            while (reader.Read ())
            {
                ScoreboardMODEL model = new ScoreboardMODEL ();
                model.ScoreID = reader.GetInt16 ("SCORE_ID");
                model.Username = reader.GetString ("USERNAME");
                model.Score = reader.GetDecimal ("SCORE");
                model.ScoreDate = reader.GetDateTime ("SCORE_DATE");
                models.Add (model);
            }
        }
        catch (Exception ex)
        {
            throw new Exception (ex.Message);
        }
        finally
        {
            connection.Close ();
        }

        return models;
    }

    /// <summary>
    /// Recover scoreboard data by ID
    /// </summary>
    public ScoreboardMODEL ListScoreboardByID (int scoreID)
    {
        ScoreboardMODEL model = new ScoreboardMODEL ();
        MySqlConnection connection = databaseConnection.GetConnection ();

        try
        {
            connection.Open ();

            // Command
            StringBuilder sql = new StringBuilder ();
            sql.Append (" SELECT SCORE_ID, USERNAME, SCORE, SCORE_DATE ");
            sql.Append (" FROM scoreboard ");
            sql.Append (" WHERE SCORE_ID = @SCORE_ID ");

            // Params
            MySqlCommand command = new MySqlCommand (sql.ToString (), connection);
            command.Parameters.AddWithValue ("@SCORE_ID", scoreID);

            // Read data
            MySqlDataReader reader = command.ExecuteReader ();
            if (reader.Read ())
            {
                model.ScoreID = reader.GetInt16 ("SCORE_ID");
                model.Username = reader.GetString ("USERNAME");
                model.Score = reader.GetDecimal ("SCORE");
                model.ScoreDate = reader.GetDateTime ("SCORE_DATE");
            }
        }
        catch (Exception ex)
        {
            throw new Exception (ex.Message);
        }
        finally
        {
            connection.Close ();
        }

        return model;
    }
}