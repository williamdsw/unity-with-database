using Model;
using Others;
using Service;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controller for Register | Update Scene
/// </summary>
namespace Controllers
{
    public class RegisterUpdateController : MonoBehaviour
    {
        // || Inspector References

        [Header("Needed UI Elements")]
        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private TMP_InputField userInput;
        [SerializeField] private TMP_InputField scoreInput;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button clearButton;
        [SerializeField] private Button okButton;

        // || State References

        private bool isUpdate = false;

        // || Cached References

        private Scoreboard scoreboard;
        private ScoreboardService service;

        /// <summary>
        /// Unity Awake Event
        /// </summary>
        private void Awake()
        {
            scoreboard = new Scoreboard();
            service = new ScoreboardService();

            if (Transporter.Instance != null && Transporter.Instance.IsUpdate)
            {
                LoadScoreData(Transporter.Instance.ScoreboardId);
            }

            headerText.text = (isUpdate ? "Update Score" : "Register New Score");

            SetEventListeners();
        }

        /// <summary>
        /// Set event listeners for buttons
        /// </summary>
        private void SetEventListeners()
        {
            try
            {
                closeButton.onClick.AddListener(BackToPreviousScene);
                clearButton.onClick.AddListener(ClearFields);
                okButton.onClick.AddListener(() => Save(isUpdate));
            }
            catch (Exception ex)
            {
                messageText.text = ex.Message;
            }
        }

        /// <summary>
        /// Load table scene
        /// </summary>
        private void BackToPreviousScene() => SceneManager.LoadScene(Configuration.Scenes.Table);

        /// <summary>
        /// Clear inputs fields
        /// </summary>
        private void ClearFields() => userInput.text = scoreInput.text = string.Empty;

        /// <summary>
        /// Load score data by Id
        /// </summary>
        /// <param name="id"> Score Id </param>
        private void LoadScoreData(int id)
        {
            try
            {
                if (id <= 0) return;

                scoreboard = service.GetById(id);
                userInput.text = scoreboard.User;
                scoreInput.text = scoreboard.Score.ToString();
                isUpdate = true;
            }
            catch (Exception ex)
            {
                messageText.text = ex.Message;
            }
        }

        /// <summary>
        /// Save record data
        /// </summary>
        /// <param name="isUpdate"> Is update or insert </param>
        private void Save(bool isUpdate)
        {
            try
            {
                if (isUpdate && scoreboard.Id <= 0)
                {
                    messageText.text = "Invalid Scoreboard Id!";
                    return;
                }

                if (CheckIfTextIsNullOrEmpty(userInput.text))
                {
                    messageText.text = "Please, inform the Username!";
                    return;
                }

                if (CheckIfTextIsNullOrEmpty(scoreInput.text))
                {
                    messageText.text = "Please, inform the Score!";
                    return;
                }

                messageText.text = string.Empty;

                // Fills model
                scoreboard.User = userInput.text;
                scoreboard.Score = decimal.Parse(scoreInput.text);
                scoreboard.Moment = DateTime.Now;

                bool success = false;
                if (isUpdate)
                {
                    success = service.Save(scoreboard);
                    messageText.text = (success ? "Score updated sucessfully!" : "Error on update the Score!");
                }
                else
                {
                    success = service.Save(scoreboard);
                    messageText.text = (success ? "Score registered sucessfully!" : "Error on register the Score!");
                }

                if (success)
                {
                    okButton.interactable = clearButton.interactable = false;
                    StartCoroutine(CountAndGetBack());
                }
            }
            catch (Exception ex)
            {
                messageText.text = ex.Message;
            }
        }

        /// <summary>
        /// Check if given text is null or empty
        /// </summary>
        /// <param name="text"> Text </param>
        private bool CheckIfTextIsNullOrEmpty(string text) => string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);

        /// <summary>
        /// Counts 3 seconds and get back to previous scene
        /// </summary>
        private IEnumerator CountAndGetBack()
        {
            yield return new WaitForSecondsRealtime(3f);
            BackToPreviousScene();
        }
    }
}