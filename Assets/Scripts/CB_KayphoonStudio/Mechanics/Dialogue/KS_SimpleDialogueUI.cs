using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class KS_SimpleDialogueUI : MonoBehaviour
{
    public TMP_Text dialogueText;
    public CanvasGroup dialogueGroup;

    public virtual void DisplayDialogue(string dialogue)
    {
        dialogueText.text = dialogue;
    }

    public virtual void DisplayDialogue(string dialogue, TMP_FontAsset font)
    {
        dialogueText.text = dialogue;
        dialogueText.font = font;
    }

    public virtual void PrepareNextDialogue()
    {
        dialogueText.text = "";
    }

    public virtual void OpenDialoguePanel()
    {
        dialogueGroup.KS_Show();
    }

    public virtual void CloseDialoguePanel()
    {
        dialogueGroup.KS_Hide();
    }
}
