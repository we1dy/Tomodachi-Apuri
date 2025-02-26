using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace DIALOGUE
{ 
    [System.Serializable]
    public class DialogueContainer
    {
        public GameObject root;
        public NameContainer nameContainer;
        public TextMeshProUGUI dialogueText;

        private CanvasGroupController cgContrtoller;
        
        public void SetDialogueColor(Color color) => dialogueText.color = color;
        public void SetDialogueFont(TMP_FontAsset font) => dialogueText.font = font;
        //public void SetDialogueFontSize(float size) => dialogueText.fontSize = size;

        private bool initialized = false;

        public void Initialize()
        {
            if (initialized) 
                return;

            cgContrtoller = new CanvasGroupController(DialogueSystem.instance, root.GetComponent<CanvasGroup>());
        }

        public bool isVisible => cgContrtoller.isVisible;
        public Coroutine Show(float speed = 1f, bool immediate = false) => cgContrtoller.Show(speed, immediate);
        public Coroutine Hide(float speed = 1f, bool immediate = false) => cgContrtoller.Hide(speed, immediate);
    }
}