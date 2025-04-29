using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SimpleDialogueData", menuName = "Kayphoon Studio/Dialogue/SimpleDialogueData")]
public class KS_SimpleDialogueData : ScriptableObject
{
    public List<DialogueSection> dialogueSections;

    [System.Serializable]
    public class DialogueSection
    {
        [TextArea(3, 10)]
        public string dialogueText;

        public virtual string GetTextOnly()
        {
            return dialogueText;
        }

        public virtual string GetTextAndExecute()
        {
            return dialogueText;
        }
    }
}
