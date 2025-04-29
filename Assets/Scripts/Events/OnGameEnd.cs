using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KayphoonStudio;
using TMPro;

public class OnGameEnd : MonoBehaviour
{
    public GameObject newCamera;
    public CanvasGroup canvasGroup;

    public TMP_Text dialogueText;
    public float eventDuration = 4f;

    public string playerWinText = "你赢了。";
    public string aiWinText = "你输了。";
    public string drawText = "平局。";

    void Start()
    {
        KS_NotificationCenter.AddEventListener(NKeys.OnGameEnd, OnGameEndEvent);
    }

    public void OnGameEndEvent(Notification data)
    {
        GameWinState gameWinState = (GameWinState)data.param;
        dialogueText.text = "";


        KS_Timer.DelayedAction(0.5f, () =>
        {
            canvasGroup.KS_FadeIn(0.3f);
        });

        KS_Timer.DelayedAction(1.5f, () =>
        {
            switch (gameWinState)
            {
                case GameWinState.PlayerWin:
                    dialogueText.text = playerWinText;
                    break;
                case GameWinState.AiWin:
                    dialogueText.text = aiWinText;
                    break;
                case GameWinState.Draw:
                    dialogueText.text = drawText;
                    break;
            }
        });
        

        // Switch to new camera
        newCamera.SetActive(true);


        KS_Timer.DelayedAction(eventDuration - 1, () =>
        {
            canvasGroup.KS_FadeOut(0.5f);
        });

        KS_Timer.DelayedAction(eventDuration, () =>
        {
            // Switch back to the original camera
            newCamera.SetActive(false);
        });
    }
}


public enum GameWinState
{
    PlayerWin,
    AiWin,
    Draw
}
