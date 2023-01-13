using DAO;
using Model;
using System.Collections.Generic;

namespace Service
{
    public class ScoreboardService
    {
        private ScoreboardDAO scoreboardDAO = new ScoreboardDAO();

        /// <summary>
        /// Insert or update scoreboard data
        /// </summary>
        /// <param name="scoreboard"> Model with data </param>
        /// <returns> Scoreboard was inserted ? </returns>
        public bool Save(Scoreboard scoreboard) => (scoreboard.Id <= 0 ? Insert(scoreboard) : Update(scoreboard));

        /// <summary>
        /// Insert scoreboard data
        /// </summary>
        /// <param name="scoreboard"> Model with data </param>
        /// <returns> Scoreboard was inserted ? </returns>
        private bool Insert(Scoreboard scoreboard) => scoreboardDAO.Insert(scoreboard);

        /// <summary>
        /// Update scoreboard data
        /// </summary>
        /// <param name="scoreboard"> Model with data </param>
        /// <returns> Scoreboard was updated ? </returns>
        private bool Update(Scoreboard scoreboard) => scoreboardDAO.Update(scoreboard);

        /// <summary>
        /// Delete scoreboard data
        /// </summary>
        /// <param name="id"> Scoreboard id </param>
        /// <returns> Scoreboard was deleted ? </returns>
        public bool DeleteById(long id) => scoreboardDAO.DeleteById(id);

        /// <summary>
        /// List all scoreboard data
        /// </summary>
        /// <returns> All scoreboard data </returns>
        public List<Scoreboard> ListAll() => scoreboardDAO.ListAll();

        /// <summary>
        /// Recover scoreboard data by ID
        /// </summary>
        /// <param name="id"> Scoreboard Id </param>
        /// <returns> Scoreboard data </returns>
        public Scoreboard GetById(long id) => scoreboardDAO.GetById(id);
    }
}