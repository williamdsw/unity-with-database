
namespace Model
{
    /// <summary>
    /// User scoreboard
    /// </summary>
    public class Scoreboard
    {
        public long Id { get; set; }
        public string User { get; set; }
        public decimal Score { get; set; }
        public long Moment { get; set; }

        public Scoreboard() { }

        public Scoreboard(long id, string user, decimal score, long moment)
        {
            Id = id;
            User = user;
            Score = score;
            Moment = moment;
        }
    }
}