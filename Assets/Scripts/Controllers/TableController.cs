using Model;
using Others;
using Service;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers
{
    /// <summary>
    /// Controller for Table Scene
    /// </summary>
    public class TableController : MonoBehaviour
    {
        // || Inspector References

        [Header("Needed UI Elements")]
        [SerializeField] private CanvasGroup tableCanvasGroup;
        [SerializeField] private Transform tableContent;
        [SerializeField] private GameObject rowPrefab;
        [SerializeField] private GameObject textColumnPrefab;
        [SerializeField] private GameObject textButtonColumnPrefab;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button registerButton;

        // || State

        private Vector3 defaultPosition;

        // || Cached

        private ScoreboardService service;

        /// <summary>
        /// Unity Awake Event
        /// </summary>
        private void Awake()
        {
            service = new ScoreboardService();
            SetEventListeners();
        }

        /// <summary>
        /// Unity Start Event
        /// </summary>
        private IEnumerator Start()
        {
            messageText.text = string.Empty;
            yield return DrawTable();
        }

        /// <summary>
        /// Set event listeners for buttons
        /// </summary>
        private void SetEventListeners()
        {
            try
            {
                quitButton.onClick.AddListener(() => Application.Quit());
                registerButton.onClick.AddListener(() => GotoRegisterUpdate(0));
            }
            catch (Exception ex)
            {
                messageText.text = ex.Message;
            }
        }

        /// <summary>
        /// Clear table items
        /// </summary>
        private IEnumerator ClearTable()
        {
            foreach (Transform item in tableContent)
            {
                Destroy(item.gameObject);
            }

            yield return new WaitUntil(() => tableContent.childCount == 0);
        }

        /// <summary>
        /// Draw table with database items
        /// </summary>
        private IEnumerator DrawTable()
        {
            yield return ClearTable();

            List<Scoreboard> scoreboards = service.ListAll();
            int ranking = 1;
            foreach (Scoreboard item in scoreboards)
            {
                GameObject row = Instantiate(rowPrefab);
                row.transform.SetParent(tableContent);
                row.transform.SetAsLastSibling();

                RectTransform rectTransform = row.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.localScale = Vector3.one;
                }

                CreateTextColumn(row.transform, ranking.ToString());
                CreateTextColumn(row.transform, item.User.ToString());
                CreateTextColumn(row.transform, item.Score.ToString());
                CreateTextColumn(row.transform, item.Moment.ToString());
                CreateButtonColumn(row.transform, "Update", () => GotoRegisterUpdate(item.Id), new Color32(109, 255, 97, 255));
                CreateButtonColumn(row.transform, "Delete", () => StartCoroutine(DeleteScore(item.Id)), new Color32(255, 132, 97, 255));
                ranking++;
            }

            messageText.text = (scoreboards.Count >= 1 ? string.Format(Configuration.Messages.ItemsFound, scoreboards.Count) : Configuration.Messages.NothingWasFound);
        }

        /// <summary>
        /// Create a table column element
        /// </summary>
        /// <param name="prefab"> Prefab to be cloned </param>
        /// <param name="rowParent"> Parent Row </param>
        /// <param name="value"> Column Value </param>
        /// <param name="color"> Column Color </param>
        /// <returns> Created game object of column </returns>
        private GameObject CreateColumn(GameObject prefab, Transform rowParent, string value, Color32? color = null)
        {
            GameObject column = Instantiate(prefab);
            column.transform.SetParent(rowParent);
            column.transform.SetAsLastSibling();

            TextMeshProUGUI textMeshPro = column.GetComponent<TextMeshProUGUI>();
            if (textMeshPro != null)
            {
                textMeshPro.text = value;
                if (color != null)
                {
                    textMeshPro.color = color.Value;
                }
            }

            RectTransform rectTransform = column.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.localScale = Vector3.one;
            }

            return column;
        }

        /// <summary>
        /// Create a table text column element
        /// </summary>
        /// <param name="rowParent"> Parent Row </param>
        /// <param name="value"> Column Value </param>
        private void CreateTextColumn(Transform rowParent, string value) => CreateColumn(textColumnPrefab, rowParent, value);

        /// <summary>
        /// Create a table button column element
        /// </summary>
        /// <param name="rowParent"> Parent Row </param>
        /// <param name="value"> Column Value </param>
        /// <param name="callback"> Callback to be called on button click </param>
        /// <param name="color"> Column Color</param>
        private void CreateButtonColumn(Transform rowParent, string value, UnityAction callback, Color? color = null)
        {
            GameObject column = CreateColumn(textButtonColumnPrefab, rowParent, value, color);
            Button button = column.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(callback);
            }
        }

        /// <summary>
        /// Load Register | Update scene
        /// </summary>
        /// <param name="id"> Score Id </param>
        private void GotoRegisterUpdate(int id)
        {
            Transporter.Instance.ScoreboardId = id;
            Transporter.Instance.IsUpdate = (id >= 1);
            SceneManager.LoadScene(Configuration.Scenes.RegisterUpdate);
        }

        /// <summary>
        /// Delete score record by Id
        /// </summary>
        /// <param name="id"> Score Id </param>
        private IEnumerator DeleteScore(int id)
        {
            if (id <= 0) yield break;

            bool hasDeleted = service.DeleteById(id);
            messageText.text = (hasDeleted ? "Score deleted sucessfully" : "Error on delete Score!");
            if (hasDeleted)
            {
                tableCanvasGroup.alpha = 0.5f;
                tableCanvasGroup.interactable = false;
                yield return new WaitForSecondsRealtime(1f);
                tableCanvasGroup.alpha = 1f;
                tableCanvasGroup.interactable = true;
                yield return DrawTable();
            }
        }
    }
}