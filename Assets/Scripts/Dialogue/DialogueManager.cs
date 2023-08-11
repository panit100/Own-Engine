using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CuteEngine.Utilities;
using CuteEngine.InputSystem;

namespace CuteEngine.Utilities.Dialogue
{
    enum DialogueStatus
    {
        START,
        TYPING,
        END,
    }

    public class DialogueManager : Singleton<DialogueManager>
    {
        public DialogueCanvas dialogueCanvas;

        [ContextMenuItem("OnPlayDialogue","OnPlayDialogue")]
        public string nextDialogueID;
        public List<DialogueData> DialogueDataList = new List<DialogueData>();
        [Range(0f,1f)]
        public float textSpeed;

        DialogueData currentDialogueData = null;
        DialogueStatus dialogueStatus = DialogueStatus.END;

        Coroutine displayTextCoroutine;

        protected override void InitAfterAwake()
        {   
            //Load Data form Json or CSV
        }

        void Start()
        {
            InputSystemManager.Instance.ToggleDialogueControl(true); //TODO Remove when finish

            AddInputAction();

            CreateDialogueCanvas();
        }

        void AddInputAction()
        {
            InputSystemManager.Instance.onNextDialogue += OnNextDialogue;
            InputSystemManager.Instance.onNextDialogue += SpeedDialogue;
            InputSystemManager.Instance.onSkipDialogue += SkipDialogue;
        }

        void RemoveInputAction()
        {
            InputSystemManager.Instance.onNextDialogue -= OnNextDialogue;
            InputSystemManager.Instance.onNextDialogue -= SpeedDialogue;
            InputSystemManager.Instance.onSkipDialogue -= SkipDialogue;
        }

        void CreateDialogueCanvas()
        {
            //TODO Create Dialogue Canvas
        }

        public void StartDialogue(DialogueData dialogueData)
        {
            if(dialogueStatus != DialogueStatus.END)
                return;

            if(currentDialogueData == null)
            {
                print("Dialogue Start");
                dialogueStatus = DialogueStatus.START;

                currentDialogueData = dialogueData;

                //TODO Add start dialogue action

                UpdateDialogueUI(currentDialogueData);
            }
            else
            {
                Debug.LogError($"Dialogue didn't finish yet. {dialogueData.ID} : {dialogueData.DialogueText}" );
            }
        }

        void EndDialogue(DialogueData dialogueData)
        {
            if(currentDialogueData == dialogueData)
            {
                //TODO Add end dialogue action
                nextDialogueID = dialogueData.NextDialogueID;
                OnDialogueEnd();
                dialogueStatus = DialogueStatus.END;
            }
            else
            {
                Debug.LogError($"Dialogue : {dialogueData.ID} isn't running now.");
            }
        }

        public void SpeedDialogue()
        {
            if(dialogueStatus == DialogueStatus.TYPING)
            {
                StopCoroutine(displayTextCoroutine);
                UpdateDialogueText(currentDialogueData.DialogueText);
                EndDialogue(currentDialogueData);
            }
        }

        public void SkipDialogue()
        {
            //TODO Skip Dialogue and close DialogeCanvas
            StopCoroutine(displayTextCoroutine);
            OnLastDialogueEnd();
        }

        DialogueData GetNextDialogue(string dialogueID)
        {
            return DialogueDataList.Find(n => n.ID == dialogueID);
        }

        void ContinueDialogue()
        {
            if(IsDialogueNotEnd())
                return;

            if(IsCurrentDialogueNull())
                return;

            if(IsHaveNextDialogue())
            {
                currentDialogueData = null;
                StartDialogue(GetNextDialogue(nextDialogueID));
                nextDialogueID = "";
                return;
            }
            else
            {
                OnLastDialogueEnd();
                return;
            }
        }

        bool IsDialogueNotEnd()
        {
            if(dialogueStatus != DialogueStatus.END)
            {
                Debug.LogError("Dialogue didn't finish.");
                return true;
            }
            else
                return false;
        }

        bool IsCurrentDialogueNull()
        {
            if(currentDialogueData == null)
            {
                Debug.LogError("Don't have next dialogue.");
                return true;
            }
            else
                return false;
        }

        bool IsHaveNextDialogue()
        {
            if(currentDialogueData.NextDialogueID != "")
                return true;
            else
                return false;
        }

        void UpdateDialogueUI(DialogueData dialogueData)
        {
            dialogueCanvas.UpdateDialogueUI(dialogueData);
            displayTextCoroutine = StartCoroutine(DisplayText(dialogueData.DialogueText));
        }

        void UpdateDialogueText(string text)
        {
            dialogueCanvas.UpdateDialogueText(text);
        }

        IEnumerator DisplayText(string dialogueText)
        {
            
            string _dialogueText = "";
            UpdateDialogueText(_dialogueText);
            yield return new WaitForSeconds(textSpeed);

            dialogueStatus = DialogueStatus.TYPING;
            foreach(var n in dialogueText.ToCharArray())
            {
                _dialogueText += n;
                UpdateDialogueText(_dialogueText);
                yield return new WaitForSeconds(textSpeed);
            }

            EndDialogue(currentDialogueData);
        }

        public void OnNextDialogue()
        {
            ContinueDialogue();
        }

        void OnDialogueEnd()
        {
            //TODO Run function when each dialogue end
        }

        void OnLastDialogueEnd()
        {
            currentDialogueData = null;
        }

#region  Test function
        void OnPlayDialogue()
        {
            StartDialogue(DialogueDataList[0]);
        }
#endregion

        void OnDestroy() 
        {
            RemoveInputAction();
        }
    }
}
