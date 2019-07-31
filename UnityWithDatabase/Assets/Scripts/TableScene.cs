using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TableScene : MonoBehaviour
{
    // Cached
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button registerButton;

    // State
    private Vector3 defaultPosition;

    // Cached
    private GameObject parent;

    //-------------------------------------------------------------------------------//
    
    private void Start ()
    {
        defaultPosition = new Vector3 (- 1100, - 160, 0);
        parent = GameObject.Find ("Content");
        DrawTable ();
        BindEvents ();
    }

    //-------------------------------------------------------------------------------//

    private void BindEvents ()
    {
        if (!registerButton) { return; }

        registerButton.onClick.AddListener (delegate 
        {
            SceneManager.LoadScene ("CreateScene");
        });
    }

    private void CleanTable ()
    {
        GameObject[] cells = GameObject.FindGameObjectsWithTag ("Cell");
        foreach (GameObject cell in cells)
        {
            Destroy (cell);
        }

        defaultPosition = new Vector3 (- 1100, - 160, 0);
    }

    private void DrawTable ()
    {
        CleanTable ();

        // Search
        ScoreboardDAO scoreboardDAO = new ScoreboardDAO ();
        List<ScoreboardMODEL> listModels = scoreboardDAO.ListScoreboard ();

        // Params
        int rankingIndex = 1;
        float currentPosY = - 160;
        for (int index = 0; index < listModels.Count; index++)
        {
            ScoreboardMODEL model = listModels[index];

            for (int j = 0; j < 1; j++)
            {
                CreateTextElement ("Cell", rankingIndex.ToString (), defaultPosition);
                CreateTextElement ("Cell", model.ScoreID.ToString (), defaultPosition);
                CreateTextElement ("Cell", model.Username, defaultPosition);
                CreateTextElement ("Cell", model.Score.ToString (), defaultPosition);
                CreateTextElement ("Cell", model.ScoreDate.ToString (), defaultPosition);
                CreateButtonElement ("Cell", model.ScoreID.ToString (), "Update", defaultPosition);
                CreateButtonElement ("Cell", model.ScoreID.ToString (), "Delete", defaultPosition);
                currentPosY -= 100f;
                defaultPosition = new Vector3 (- 1100, currentPosY, 0);
            }

            rankingIndex++;
        }
    }

    private void CreateTextElement (string name, string value, Vector3 position)
    {
        // New Position
        position = new Vector3 (position.x + 250f, position.y, 0f);
        defaultPosition = position;

        // Object and parent
        GameObject cell = new GameObject (name);
        cell.tag = "Cell";
        cell.transform.SetParent (parent.transform);

        // TextMesh properties
        TextMeshProUGUI text = cell.AddComponent<TextMeshProUGUI>();
        text.text = value;
        text.alignment = TextAlignmentOptions.Center;

        // RectTransform properties
        RectTransform rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.one;
        rectTransform.SetInsetAndSizeFromParentEdge (RectTransform.Edge.Top, 0, 50f);
        rectTransform.anchoredPosition = position;
    }

    private void CreateButtonElement (string name, string value, string buttonText, Vector3 position)
    {
        // New Position
        position = new Vector3 (position.x + 300f, position.y, 0f);
        defaultPosition = position;

        // Object and parent
        GameObject cell = new GameObject (name);
        cell.tag = "Cell";
        cell.transform.SetParent (parent.transform);

        // TextMesh properties
        TextMeshProUGUI text = cell.AddComponent<TextMeshProUGUI>();
        text.text = buttonText;
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.yellow;

        // TextMesh properties
        Button button = cell.AddComponent<Button>();
        button.onClick.AddListener (delegate
        {
            if (buttonText.Equals ("Update"))
            {
                GotoUpdateScene (int.Parse (value));
            }
            else if (buttonText.Equals ("Delete"))
            {
                DeleteScore (int.Parse (value));
            }
        });

        // RectTransform properties
        RectTransform rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.one;
        rectTransform.SetInsetAndSizeFromParentEdge (RectTransform.Edge.Top, 0, 50f);
        rectTransform.anchoredPosition = position;
    }

    private void GotoUpdateScene (int scoreID)
    {
        InformationTransporter.instance.SetScoreID (scoreID);
        SceneManager.LoadScene ("UpdateScene");
    }

    private void DeleteScore (int scoreID)
    {
        // Cancels
        if (scoreID <= 0) { return; }

        ScoreboardDAO scoreboardDAO = new ScoreboardDAO ();
        string exceptionMessage;
        bool hasDeleted = scoreboardDAO.Delete (scoreID, out exceptionMessage);

        if (hasDeleted)
        {
            messageText.text = "Score deleted sucessfully";
            DrawTable ();
        }
        else 
        {
            messageText.text = string.Concat ("Error on delete Score! ", exceptionMessage);
        }
    }
}