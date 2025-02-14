using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DIALOGUE;

namespace TESTING
{
    public class Testing_Architect : MonoBehaviour
    {
        DialogueSystem ds;
        TextArchitect architect;

        ///public TextArchitect.BuildMethod bm = TextArchitect.BuildMethod.instant;

        string[] lines = new string[9]
        {
            "The morning air is crisp, carrying the faint scent of damp pavement. The campus gates loom ahead, familiar yet intimidating. A soft breeze rustles the leaves above, casting dappled shadows on the ground.",
            "The faint hum of distant chatter and the occasional honk of a car fill the air, but it all feels muffled, as if you’re standing at the edge of a dream.",
            "You stand at the gates, your chest tightening as you gaze at the familiar buildings. So many memories linger here.",
            "That building over there is where you aced your first-ever quiz. And that spot by the bleachers that's where you used to hang out after class, laughing with friends as the sun dipped below the horizon.",
            "The warmth of the memory fades as you blink and realize how much has changed. The faces around you are unfamiliar, and the weight of the past tugs at your heart. It’s as if the campus itself is watching, waiting to see if you still belong.",
            "*Phone Alarm*",
            "The sound of an alarm from your phone snaps you out of your thoughts. You fumble to pull it out of your pocket, the screen lighting up with the time.",
            "Wait... is that the time?!",
            "Panic surges through you as you realize you’re about to be late for your first class. You take off in a sprint, your bag bouncing against your side. The world blurs around you as you dodge clusters of students, your heart pounding louder than your shoes hitting the pavement.",
        };

        void Start()
        {
            ds = DialogueSystem.instance;
            architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            architect.buildMethod = TextArchitect.BuildMethod.typewriter;
            architect.speed = 0.5f;
        }

        void Update()
        {
            //if (bm != architect.buildMethod)
            //{
            //    architect.buildMethod = bm;
            //    architect.Stop();
            //}

            if (Keyboard.current != null && Keyboard.current.sKey.wasPressedThisFrame)
                architect.Stop();

            //string longLine = "The morning air is crisp, carrying the faint scent of damp pavement as I stand at the gates of the University of Makati. My chest tightens as I take in the familiar buildings, where so many memories linger. A soft breeze rustles the leaves above, making the whole scene feel both nostalgic and intimidating.";
            // Check for Space key press on keyboard
            if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                if (architect.isBuilding)
                {
                    if (!architect.isBuilding)
                    {
                        architect.hurryUp = true;
                    }
                    else
                        architect.ForceComplete();
                }
                else
                    if (currentIndex < lines.Length)
                {
                    architect.Build(lines[currentIndex]);
                    currentIndex++;
                }
                //architect.Build(longLine);
                //architect.Build(lines[Random.Range(0, lines.Length)]);
            }
            else if (Keyboard.current != null && Keyboard.current.aKey.wasPressedThisFrame)
            {
                if (currentIndex < lines.Length)
                {
                    architect.Append(lines[currentIndex]);
                    currentIndex++;
                }
                //architect.Append(longLine);
                //architect.Append(lines[Random.Range(0, lines.Length)]);
            }

            // Check for screen taps on touch devices
            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                if (architect.isBuilding)
                {
                    if (!architect.isBuilding)
                    {
                        architect.hurryUp = true;
                    }
                    else
                        architect.ForceComplete();
                }
                else
                    if (currentIndex < lines.Length)
                {
                    architect.Build(lines[currentIndex]);
                    currentIndex++;
                }
                //architect.Build(longLine);
                //architect.Build(lines[Random.Range(0, lines.Length)]);
            }
            else if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                if (currentIndex < lines.Length)
                {
                    architect.Append(lines[currentIndex]);
                    currentIndex++;
                }
            }
        }
        private int currentIndex = 0;

    }
}
