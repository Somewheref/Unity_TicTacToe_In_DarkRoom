using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class KS_TextRoller : MonoBehaviour
{
    public string text;
    public float rollingSpeed = 1f;
    public RollDirection direction;
    public int tileNumber = 2;  // Set the number of text tiles
    public GameObject textPrefab;  // Assign a TextMeshPro prefab
    private List<TMP_Text> textObjects = new List<TMP_Text>();

    public enum RollDirection
    {
        Left,
        Right
        // Extend this enum for Up and Down if needed
    }

    void Start()
    {
        for (int i = 0; i < tileNumber; i++)
        {
            var textObject = Instantiate(textPrefab, transform).GetComponent<TMP_Text>();
            textObject.gameObject.SetActive(true);
            textObject.text = text;
            textObjects.Add(textObject);
        }
        PositionTextObjects();
    }

    void Update()
    {
        float offset = Time.deltaTime * rollingSpeed;
        foreach (var textObject in textObjects)
        {
            RectTransform rectTransform = textObject.GetComponent<RectTransform>();
            Vector2 newPosition = rectTransform.anchoredPosition;

            if (direction == RollDirection.Left)
            {
                newPosition.x -= offset;
                if (newPosition.x + rectTransform.rect.width < 0)
                    newPosition.x += rectTransform.rect.width * textObjects.Count;
            }
            else if (direction == RollDirection.Right)
            {
                newPosition.x += offset;
                if (newPosition.x > rectTransform.rect.width * textObjects.Count)
                    newPosition.x -= rectTransform.rect.width * textObjects.Count;
            }

            rectTransform.anchoredPosition = newPosition;
        }
    }

    void PositionTextObjects()
    {
        float width = textObjects[0].GetComponent<RectTransform>().rect.width;
        for (int i = 0; i < textObjects.Count; i++)
        {
            RectTransform rectTransform = textObjects[i].GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(i * width, 0);
        }
    }
}
