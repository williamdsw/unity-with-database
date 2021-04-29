﻿using Model;
using Others;
using Service;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controller
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

        private void Awake()
        {
            scoreboard = new Scoreboard();
            service = new ScoreboardService();

            if (Transporter.Instance.IsUpdate)
            {
                LoadScoreData(Transporter.Instance.ScoreboardId);
            }

            headerText.text = (isUpdate ? "Update Score" : "Register New Score");

            BindEvents();
        }

        private void BindEvents()
        {
            closeButton.onClick.AddListener(BackToPreviousScene);
            clearButton.onClick.AddListener(ClearFields);
            okButton.onClick.AddListener(() => Save(isUpdate));
        }

        private void BackToPreviousScene() => SceneManager.LoadScene(Configuration.Scenes.Table);

        private void ClearFields() => userInput.text = scoreInput.text = string.Empty;

        private void LoadScoreData(int id)
        {
            if (id <= 0) return;

            // Loads data
            scoreboard = service.GetById(id);
            userInput.text = scoreboard.User;
            scoreInput.text = scoreboard.Score.ToString();
            isUpdate = true;
        }

        private void Save(bool isUpdate)
        {
            // Checks ID
            if (isUpdate && scoreboard.Id <= 0)
            {
                messageText.text = "Invalid Scoreboard Id!";
                return;
            }

            if (CheckIfTextIsNullOrEmpty(userInput.text))
            {
                messageText.text = "Please, inform the Username!";
            }
            else if (CheckIfTextIsNullOrEmpty(scoreInput.text))
            {
                messageText.text = "Please, inform the Score!";
            }
            else
            {
                messageText.text = string.Empty;

                // Fills model
                scoreboard.User = userInput.text;
                scoreboard.Score = decimal.Parse(scoreInput.text);
                scoreboard.Moment = DateTime.Now;

                string exceptionMessage = string.Empty;
                if (isUpdate)
                {
                    bool hasUpdated = service.Save(scoreboard);
                    messageText.text = (hasUpdated ? "Score updated sucessfully!" : "Error on update the Score!");
                }
                else
                {
                    bool hasInserted = service.Save(scoreboard);
                    messageText.text = (hasInserted ? "Score registered sucessfully!" : "Error on register the Score!");
                }
            }
        }

        private bool CheckIfTextIsNullOrEmpty(string text) => string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);
    }
}