using DAO.Utils;
using Model;
using Others;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DAO
{
    public class ScoreboardDAO : Connection
    {
        public ScoreboardDAO() : base() { }


        /// <summary>
        /// Insert scoreboard data
        /// </summary>
        public bool Insert(Scoreboard model)
        {
            try
            {
                string query = string.Format(Configuration.Queries.Scoreboard.Insert, model.User, model.Score.ToString().Replace(",", "."), model.Moment);
                return (ExecuteNonQuery(query) == 1);
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("Insert -> {0}", ex.Message);
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
        public bool Update(Scoreboard model)
        {
            try
            {
                string query = string.Format(Configuration.Queries.Scoreboard.Update, model.User, model.Score.ToString().Replace(",", "."), model.Moment, model.Id);
                return (ExecuteNonQuery(query) == 1);
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("Update -> {0}", ex.Message);
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
        public bool DeleteById(long id)
        {
            try
            {
                GetById(id);
                ExecuteQuery(string.Format(Configuration.Queries.Scoreboard.Delete, id));
                return GetById(id) == null;
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("DeleteById -> {0}", ex.Message);
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
        public List<Scoreboard> ListAll()
        {
            try
            {
                return Factory<Scoreboard>.CreateMany(ExecuteQuery(Configuration.Queries.Scoreboard.ListAll));
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("ListAll -> {0}", ex.Message);
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
                Debug.LogErrorFormat("GetById -> {0}", ex.Message);
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}