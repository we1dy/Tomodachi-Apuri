using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


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
            // Keyboard input (for testing in)
            if (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.enterKey.wasPressedThisFrame)
                PromptAdvance();

            // Touch input (for mobile devices)
            //if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            //    PromptAdvance();
        }


        public void PromptAdvance()
        {
            DialogueSystem.instance.OnUserPrompt_Next();
        }
    }

}
