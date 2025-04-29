using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using KayphoonStudio.Language;

namespace KayphoonStudio
{
    /// <summary>
    /// A simple image gallery panel base
    /// uses two buttons (left and right) or one button (next) to navigate through images
    /// </summary>

    [System.Serializable]
    public class DisplaySection
    {
        public Sprite image;
        public string description;
        public List<GameObject> customObjects;
    }

    public class KS_UI_ImageGalleryPanel : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public List<DisplaySection> displaySections;

        public Image displayImage;
        public TMP_Text galleryPanelDescription;
        [Tooltip("The final page should contain a close button.")]
        public GameObject finalPage;
        public Button leftButton;
        public Button rightButton;
        public Button nextButton;
        public Button closeButton;

        public UnityEvent OnPanelClosed;

        private int currentSection = 0;


        private void Awake() 
        {
            if (leftButton)
                leftButton.onClick.AddListener(Left);
            if (rightButton)
                rightButton.onClick.AddListener(Right);
            if (nextButton)
                nextButton.onClick.AddListener(Right);
            if (closeButton)
                closeButton.onClick.AddListener(Hide);
        }

        [ContextMenu("Show")]
        public void Show()
        {
            canvasGroup.DOKill();
            canvasGroup.KS_FadeIn(0.5f, onFinish: () => gameObject.SetActive(true));

            currentSection = 0;

            UpdatePage();

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            canvasGroup.DOKill();
            canvasGroup.interactable = false;
            canvasGroup.KS_FadeOut(0.5f, onFinish: () => gameObject.SetActive(false));

            currentSection = 0;

            OnPanelClosed.Invoke();
        }

        public void Right()
        {
            if (currentSection == displaySections.Count - 1 + (finalPage ? 1 : 0))
            {
                return;
            }
            currentSection++;
            
            UpdatePage();
        }

        public void Left()
        {
            if (currentSection == 0)
            {
                return;
            }
            currentSection--;
            
            UpdatePage();
        }


        public void DeactivateAllCustomObjects()
        {
            foreach (var section in displaySections)
            {
                foreach (var obj in section.customObjects)
                {
                    obj.SetActive(false);
                }
            }
        }


        public void UpdatePage()
        {
            DeactivateAllCustomObjects();

            // check to see if we are on page 0 or last page, if so, disable the buttons
            if (leftButton)
                leftButton.gameObject.SetActive(currentSection != 0);
            if (rightButton)
                rightButton.gameObject.SetActive(currentSection != displaySections.Count - 1 +1);

            if(currentSection <= displaySections.Count - 1 - (finalPage ? 0 : 1))
            {
                displayImage.gameObject.SetActive(true);
                if (finalPage)
                    finalPage.SetActive(false);
                // update the image and description
                displayImage.sprite = displaySections[currentSection].image;
                if (galleryPanelDescription)
                {
                    galleryPanelDescription.gameObject.SetActive(true);
                    galleryPanelDescription.text = KS_Language.Instance.GetText(displaySections[currentSection].description);
                    galleryPanelDescription.font = KS_Language.Instance.GetCurrentData().defaultFont;
                }

                // display custom object
                foreach (var obj in displaySections[currentSection].customObjects)
                {
                    obj.SetActive(true);
                }

                // buttons
                if (nextButton)
                    nextButton.gameObject.SetActive(true);
                if (closeButton)
                    closeButton.gameObject.SetActive(false);
            }
            else if (finalPage)
            {
                displayImage.gameObject.SetActive(false);
                finalPage.SetActive(true);

                if (galleryPanelDescription)
                {
                    galleryPanelDescription.gameObject.SetActive(false);
                }
            }

            else
            {
                // no final page. Replace next button with close button
                if (nextButton)
                    nextButton.gameObject.SetActive(false);
                if (closeButton)
                    closeButton.gameObject.SetActive(true);

                displayImage.gameObject.SetActive(true);

                displayImage.sprite = displaySections[currentSection].image;
                if (galleryPanelDescription)
                {
                    galleryPanelDescription.gameObject.SetActive(true);
                    galleryPanelDescription.text = KS_Language.Instance.GetText(displaySections[currentSection].description);
                    galleryPanelDescription.font = KS_Language.Instance.GetCurrentData().defaultFont;
                }

                // display custom object
                foreach (var obj in displaySections[currentSection].customObjects)
                {
                    obj.SetActive(true);
                }
            }

        }
    }
}
