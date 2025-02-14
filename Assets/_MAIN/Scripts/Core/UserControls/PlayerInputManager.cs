using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace DIALOGUE
{ 
    public class PlayerInputManager : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            // Keyboard input (for testing in the Unity Editor)
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                PromptAdvance();

            //Touch input(for mobile devices)
            //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            //    PromptAdvance();
        }

        public void PromptAdvance()
        {
            DialogueSystem.instance.OnUserPrompt_Next();
        }
    }

}
