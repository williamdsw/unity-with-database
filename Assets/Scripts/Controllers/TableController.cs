using Model;
using Others;
using Service;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TableController : MonoBehaviour
{
    // || Inspector References

    [Header("Needed UI Elements")]
    [SerializeField] private Transform tableContent;
    [SerializeField] private GameObject rowPrefab;
    [SerializeField] private GameObject textColumnPrefab;
    [SerializeField] private GameObject textButtonColumnPrefab;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button registerButton;

    // || State

    private Vector3 defaultPosition;

    // || Cached

    private ScoreboardService service;

    private GameObject parent;



    private void Awake()
    {
        service = new ScoreboardService();
        SetEventListeners();
    }

    private void Start()
    {
        messageText.text = string.Empty;
        StartCoroutine(DrawTable());
    }

    private void SetEventListeners() => registerButton.onClick.AddListener(() => GotoRegisterUpdate(0));

    private IEnumerator ClearTable()
    {
        foreach (Transform item in tableContent)
        {
            Destroy(item.gameObject);
        }

        yield return new WaitUntil(() => tableContent.childCount == 0);
    }

    private IEnumerator DrawTable()
    {
        yield return StartCoroutine(ClearTable());

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
            CreateButtonColumn(row.transform, "Delete", () => DeleteScore(item.Id), new Color32(255, 132, 97, 255));
            ranking++;
        }
    }

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

    private void CreateTextColumn(Transform rowParent, string value) => CreateColumn(textColumnPrefab, rowParent, value);

    private void CreateButtonColumn(Transform rowParent, string value, UnityAction callback, Color? color = null)
    {
        GameObject column = CreateColumn(textButtonColumnPrefab, rowParent, value, color);
        Button button = column.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(callback);
        }
    }

    private void GotoRegisterUpdate(int id)
    {
        Transporter.Instance.ScoreboardId = id;
        Transporter.Instance.IsUpdate = (id >= 1);
        SceneManager.LoadScene(Configuration.Scenes.RegisterUpdate);
    }

    private void DeleteScore(int id)
    {
        if (id <= 0) return;

        bool hasDeleted = service.DeleteById(id);
        messageText.text = (hasDeleted ? "Score deleted sucessfully" : "Error on delete Score!");
        if (hasDeleted)
        {
            StartCoroutine(DrawTable());
        }
    }
}