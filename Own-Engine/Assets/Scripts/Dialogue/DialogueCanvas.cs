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

        [Header("Main Character")]
        public TMP_Text mainCharacterName;
        public Image mainCharacterImage;

        [Header("Sub Character")]
        public TMP_Text subCharacterName;
        public Image subCharacterImage;

        public void UpdateDialogueUI(DialogueData dialogueData)
        {
            //TODO Update Character Image and Name
            
            if(dialogueData.IsMainCharacter)
            {
                SetMainCharacterDialogue(dialogueData);
            }
            else
            {
                SetSubCharacterDialogue(dialogueData);
            }
        }

        public void UpdateDialogueText(string text)
        {
            dialogueText.text = text;
        }

        void SetMainCharacterDialogue(DialogueData dialogueData)
        {
            mainCharacterName.text = dialogueData.CharacterName;

            mainCharacterImage.sprite = dialogueData.GetCharacterSprite();
            mainCharacterImage.SetNativeSize();
        }

        void SetSubCharacterDialogue(DialogueData dialogueData)
        {
            subCharacterName.text = dialogueData.CharacterName;

            subCharacterImage.sprite = dialogueData.GetCharacterSprite();
            subCharacterImage.SetNativeSize();
        }
    }
}