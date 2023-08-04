using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace CuteEngine.Utilities.Dialogue
{
    public class DialogueCanvas : MonoBehaviour
    {
        public TMP_Text dialogueText;

        public TMP_Text character;

        public void UpdateDialogueUI(DialogueData dialogueData)
        {
            //TODO Update Character Image and Name
            
            character.text = dialogueData.CharacterName;
        }

        public void UpdateDialogueText(string text)
        {
            dialogueText.text = text;
        }
    }
}