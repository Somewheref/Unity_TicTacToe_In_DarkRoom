using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KS_ImageRoller : MonoBehaviour
{
    public float rollingSpeed = 1f;
    public RollDirection direction;
    public int tileNumber = 2;  // Set the number of text tiles
    public GameObject ImageTemplate;  // Assign a TextMeshPro prefab
    private List<RectTransform> uiObjects = new List<RectTransform>();

    public enum RollDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    void Start()
    {
        for (int i = 0; i < tileNumber; i++)
        {
            var uiObject = Instantiate(ImageTemplate, transform).GetComponent<RectTransform>();
            uiObject.gameObject.SetActive(true);
            uiObjects.Add(uiObject);
        }
        PositionTextObjects();
    }

    void Update()
    {
        float offset = Time.deltaTime * rollingSpeed;
        foreach (var uiObject in uiObjects)
        {
            RectTransform rectTransform = uiObject;
            Vector2 newPosition = rectTransform.anchoredPosition;

            if (direction == RollDirection.Left)
            {
                newPosition.x -= offset;
                if (newPosition.x + rectTransform.rect.width < 0)
                    newPosition.x += rectTransform.rect.width * uiObjects.Count;
            }
            else if (direction == RollDirection.Right)
            {
                newPosition.x += offset;
                if (newPosition.x > rectTransform.rect.width * uiObjects.Count)
                    newPosition.x -= rectTransform.rect.width * uiObjects.Count;
            }
            else if (direction == RollDirection.Up)
            {
                newPosition.y += offset;
                if (newPosition.y > rectTransform.rect.height * uiObjects.Count)
                    newPosition.y -= rectTransform.rect.height * uiObjects.Count;
            }
            else if (direction == RollDirection.Down)
            {
                newPosition.y -= offset;
                if (newPosition.y + rectTransform.rect.height < 0)
                    newPosition.y += rectTransform.rect.height * uiObjects.Count;
            }
            

            rectTransform.anchoredPosition = newPosition;
        }
    }

    void PositionTextObjects()
    {
        float width = uiObjects[0].GetComponent<RectTransform>().rect.width;
        for (int i = 0; i < uiObjects.Count; i++)
        {
            RectTransform rectTransform = uiObjects[i];
            rectTransform.anchoredPosition = new Vector2(i * width, 0);
        }
    }
}
