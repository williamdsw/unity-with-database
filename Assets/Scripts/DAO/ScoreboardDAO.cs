using DAO.Utils;
using Model;
using Others;
using System;
using System.Collections.Generic;

namespace DAO
{
    /// <summary>
    /// DAO for Scoreboard
    /// </summary>
    public class ScoreboardDAO : DatabaseConnection
    {
        /// <summary>
        /// Insert scoreboard data
        /// </summary>
        /// <param name="model"> Model with data </param>
        /// <returns> Scoreboard was inserted ? </returns>
        public bool Insert(Scoreboard model)
        {
            try
            {
                Dictionary<string, object> keyValuePairs = new Dictionary<string, object>()
                {
                    { "@USER", model.User },
                    { "@SCORE", model.Score },
                    { "@MOMENT", model.Moment },
                };

                return ExecuteNonQuery(Configuration.Queries.Scoreboard.Insert, keyValuePairs) == 1;
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
                Dictionary<string, object> keyValuePairs = new Dictionary<string, object>()
                {
                    { "@USER", model.User },
                    { "@SCORE", model.Score },
                    { "@MOMENT", model.Moment },
                    { "@ID", model.Id },
                };

                return ExecuteNonQuery(Configuration.Queries.Scoreboard.Update, keyValuePairs) == 1;
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
                Dictionary<string, object> keyValuePairs = new Dictionary<string, object>()
                {
                    { "@ID", id },
                };

                return ExecuteNonQuery(Configuration.Queries.Scoreboard.Delete, keyValuePairs) == 1;
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

                Dictionary<string, object> keyValuePairs = new Dictionary<string, object>()
                {
                    { "@ID", id },
                };

                return Factory<Scoreboard>.CreateOne(ExecuteQuery(Configuration.Queries.Scoreboard.GetById, keyValuePairs));
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