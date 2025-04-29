using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KS_SimpleDialogueUI_Fade : KS_SimpleDialogueUI
{
    [Header("Fade Settings")]
    public float fadeDuration = 1f;


    public override void DisplayDialogue(string dialogue)
    {
        base.DisplayDialogue(dialogue);
        dialogueGroup.KS_FadeIn(fadeDuration);
    }

    public override void DisplayDialogue(string dialogue, TMP_FontAsset font)
    {
        base.DisplayDialogue(dialogue, font);
        dialogueGroup.KS_FadeIn(fadeDuration);
    }

    public override void PrepareNextDialogue()
    {
        dialogueGroup.KS_FadeOut(fadeDuration);
    }

    public override void OpenDialoguePanel()
    {
        //dialogueGroup.KS_FadeIn(fadeDuration);
    }

    public override void CloseDialoguePanel()
    {
        //dialogueGroup.KS_FadeOut(fadeDuration);
    }

    public void FadeIn()
    {
        dialogueGroup.KS_FadeIn(fadeDuration);
    }

    public void FadeOut()
    {
        dialogueGroup.KS_FadeOut(fadeDuration);
    }
}
