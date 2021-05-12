using DAO;
using Model;
using System.Collections.Generic;

namespace Service
{
    public class ScoreboardService
    {
        // || Cached

        private ScoreboardDAO scoreboardDAO = new ScoreboardDAO();

        public bool Save(Scoreboard scoreboard) => (scoreboard.Id <= 0 ? Insert(scoreboard) : Update(scoreboard));

        private bool Insert(Scoreboard scoreboard) => scoreboardDAO.Insert(scoreboard);

        private bool Update(Scoreboard scoreboard) => scoreboardDAO.Update(scoreboard);

        public bool DeleteById(long id) => scoreboardDAO.DeleteById(id);

        public List<Scoreboard> ListAll() => scoreboardDAO.ListAll();

        public Scoreboard GetById(long id) => scoreboardDAO.GetById(id);
    }
}