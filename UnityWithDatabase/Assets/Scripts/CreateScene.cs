using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateScene : MonoBehaviour
{
    // Config
    [SerializeField] private TMP_InputField usernameText;
    [SerializeField] private TMP_InputField scoreText;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button backButton;
    [SerializeField] private Button registerButton;

    //-----------------------------------------------------------------------------//

    private void Start () 
    {
        BindEvents ();
    }

    //-----------------------------------------------------------------------------//

    private void BindEvents ()
    {
        // Cancels
        if (!backButton || !registerButton) { return; }

        backButton.onClick.AddListener (BackToPreviousScene);
        registerButton.onClick.AddListener (RegisterScore);
    }

    private void BackToPreviousScene ()
    {
        SceneManager.LoadScene ("TableScene");
    }

    private void RegisterScore ()
    {
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
        model.Username = usernameText.text;
        model.Score = decimal.Parse (scoreText.text);
        model.ScoreDate = DateTime.Now;

        ScoreboardDAO scoreboardDAO = new ScoreboardDAO ();
        string exceptionMessage;
        bool hasInserted = scoreboardDAO.InsertScoreboard (model, out exceptionMessage);
        messageText.text = (hasInserted ? "Score registered sucessfully!" : string.Concat ("Error on register the Score! ", exceptionMessage));
    }
}