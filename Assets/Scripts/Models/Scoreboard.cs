using System;

namespace Model
{
    public class Scoreboard
    {
        private int id;
        private string user;
        private decimal score;
        private DateTime moment;

        public int Id { get => id; set => id = value; }
        public string User { get => user; set => user = value; }
        public decimal Score { get => score; set => score = value; }
        public DateTime Moment { get => moment; set => moment = value; }
    }
}