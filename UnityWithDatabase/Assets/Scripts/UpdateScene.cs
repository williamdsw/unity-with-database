using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpdateScene : MonoBehaviour
{
    // Config
    [SerializeField] private TMP_InputField usernameText;
    [SerializeField] private TMP_InputField scoreText;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button backButton;
    [SerializeField] private Button updateButton;

    // State
    private int scoreID;

    //-------------------------------------------------------------------------------//

    private void Start () 
    {
        scoreID = (InformationTransporter.instance ? InformationTransporter.instance.GetScoreID () : 0);
        LoadScoreData (scoreID);
        BindEvents ();
    }

    //-------------------------------------------------------------------------------//

    private void BindEvents ()
    {
        // Cancels
        if (!backButton || !updateButton) { return; }

        backButton.onClick.AddListener (BackToPreviousScene);
        updateButton.onClick.AddListener (UpdateScore);
    }

    private void BackToPreviousScene ()
    {
        SceneManager.LoadScene ("TableScene");
    }

    private void LoadScoreData (int scoreID)
    {
        if (scoreID <= 0) { return; }

        // Loads data
        ScoreboardDAO scoreboardDAO = new ScoreboardDAO ();
        ScoreboardMODEL model = scoreboardDAO.ListScoreboardByID (scoreID);
        usernameText.text = model.Username;
        scoreText.text = model.Score.ToString ();
    }

    private void UpdateScore ()
    {
        // Checks ID
        if (scoreID <= 0)
        {
            messageText.text = "Invalid ID!";
            return;
        }

        // Checks username
        if (string.IsNullOrEmpty (usernameText.text) || string.IsNullOrWhiteSpace (usernameText.text))
        {
            messageText.text = "Please, inform the Username!";
            return;
        }

        // Checks score
        if (string.IsNullOrEmpty (scoreText.text) || string.IsNullOrWhiteSpace (scoreText.text))
        {
            messageText.text = "Please, inform the Score!";
            return;
        }

        // Fills model
        ScoreboardMODEL model = new ScoreboardMODEL ();
        model.ScoreID = this.scoreID;
        model.Username = usernameText.text;
        model.Score = decimal.Parse (scoreText.text);
        model.ScoreDate = DateTime.Now;

        ScoreboardDAO scoreboardDAO = new ScoreboardDAO ();
        string exceptionMessage;
        bool hasUpdated = scoreboardDAO.UpdateScoreboard (model, out exceptionMessage);
        messageText.text = (hasUpdated ? "Score updated sucessfully!" : string.Concat ("Error on update the Score! ", exceptionMessage));
    }
}