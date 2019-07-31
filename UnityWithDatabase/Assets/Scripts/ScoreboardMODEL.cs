using System;

public class ScoreboardMODEL
{
    private int scoreID;
    private string username;
    private decimal score;
    private DateTime scoreDate;

    //----------------------------------------------------------------------------------//
    // PROPERTIES

    public int ScoreID { get { return scoreID; } set { scoreID = value; } }
    public string Username { get { return username; } set { username = value; } }
    public decimal Score { get { return score; } set { score = value; } }
    public DateTime ScoreDate { get { return scoreDate; } set { scoreDate = value; } }
}