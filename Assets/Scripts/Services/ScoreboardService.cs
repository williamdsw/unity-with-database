using DAO;
using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Service
{
    public class ScoreboardService
    {
        // || Cached

        private ScoreboardDAO scoreboardDAO = new ScoreboardDAO();

        public bool Save(Scoreboard scoreboard)
        {
            return (scoreboard.Id <= 0 ? Insert(scoreboard) : Update(scoreboard));
        }

        private bool Insert(Scoreboard scoreboard)
        {
            return scoreboardDAO.Insert(scoreboard);
        }

        private bool Update(Scoreboard scoreboard)
        {
            return scoreboardDAO.Insert(scoreboard);
        }

        public bool DeleteById(int id)
        {
            return scoreboardDAO.DeleteById(id);
        }

        public List<Scoreboard> ListAll()
        {
            return scoreboardDAO.ListAll();
        }

        public Scoreboard GetById(int id)
        {
            return scoreboardDAO.GetById(id);
        }
    }
}