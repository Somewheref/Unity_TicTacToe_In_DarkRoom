using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace KayphoonStudio.Dialogue
{
    public class KS_SimpleDialogueManager : MonoBehaviour
    {
        public KS_SimpleDialogueData dialogueData;
        public KS_SimpleDialogueUI dialogueUI;

        public UnityEvent onDialogueStart;
        public UnityEvent onDialogueEnd;

        protected int currentSectionIndex = 0;

        protected virtual void Start()
        {
            //StartNewDialogue();
        }

        public virtual void StartNewDialogue()
        {
            currentSectionIndex = 0;

            dialogueUI.OpenDialoguePanel();

            onDialogueStart?.Invoke();
        }

        public virtual void PrepareNextDialogue()
        {
            dialogueUI.PrepareNextDialogue();
        }

        public virtual void DisplayDialogue()
        {
            if (currentSectionIndex < dialogueData.dialogueSections.Count)
            {
                dialogueUI.DisplayDialogue(dialogueData.dialogueSections[currentSectionIndex].GetTextAndExecute());
                currentSectionIndex++;
            }
            else
            {
                EndDialogue();
            }
        }

        public virtual void EndDialogue()
        {
            dialogueUI.CloseDialoguePanel();
            onDialogueEnd?.Invoke();
        }
    }

}
