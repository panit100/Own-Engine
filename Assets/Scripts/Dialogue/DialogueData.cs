using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CuteEngine.Utilities.Dialogue
{   
    [Serializable]
    public class DialogueData
    {
        public string ID;
        public string CharacterName;
        public string DialogueText;
        public string NextDialogueID;

        // public bool IsHaveNextDialogue()
        // {
        //     if(NextDialogueID != "" || NextDialogueID != null)
        //         return true;
            
        //     return false;
        // }
    }
}
