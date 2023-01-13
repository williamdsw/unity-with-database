using DAO.Utils;
using Model;
using Others;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DAO
{
    /// <summary>
    /// DAO for Scoreboard
    /// </summary>
    public class ScoreboardDAO : Connection
    {
        public ScoreboardDAO() : base() { }

        /// <summary>
        /// Insert scoreboard data
        /// </summary>
        /// <param name="model"> Model with data </param>
        /// <returns> Scoreboard was inserted ? </returns>
        public bool Insert(Scoreboard model)
        {
            try
            {
                string query = string.Format(Configuration.Queries.Scoreboard.Insert, model.User, model.Score.ToString().Replace(",", "."), model.Moment);
                return (ExecuteNonQuery(query) == 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// Update scoreboard data
        /// </summary>
        /// <param name="model"> Model with data </param>
        /// <returns> Scoreboard was updated ? </returns>
        public bool Update(Scoreboard model)
        {
            try
            {
                string query = string.Format(Configuration.Queries.Scoreboard.Update, model.User, model.Score.ToString().Replace(",", "."), model.Moment, model.Id);
                return (ExecuteNonQuery(query) == 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// Delete scoreboard data
        /// </summary>
        /// <param name="id"> Scoreboard id </param>
        /// <returns> Scoreboard was deleted ? </returns>
        public bool DeleteById(long id)
        {
            try
            {
                return ExecuteNonQuery(string.Format(Configuration.Queries.Scoreboard.Delete, id)) == 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// List all scoreboard data
        /// </summary>
        /// <returns> All scoreboard data </returns>
        public List<Scoreboard> ListAll()
        {
            try
            {
                return Factory<Scoreboard>.CreateMany(ExecuteQuery(Configuration.Queries.Scoreboard.ListAll));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// Recover scoreboard data by ID
        /// </summary>
        /// <param name="id"> Scoreboard Id </param>
        /// <returns> Scoreboard data </returns>
        public Scoreboard GetById(long id)
        {
            try
            {
                if (id <= 0) throw new Exception(string.Format("Invalid Id -> {0}", id));
                string query = string.Format(Configuration.Queries.Scoreboard.GetById, id);
                return Factory<Scoreboard>.CreateOne(ExecuteQuery(query));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}