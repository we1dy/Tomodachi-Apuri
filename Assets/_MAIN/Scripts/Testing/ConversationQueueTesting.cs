using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ConversationQueueTesting : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Running());
    }

    IEnumerator Running()
    {
        Debug.Log("running coroutine has started.");

        List<string> lines = new List<string>()
        {
            "narrator\"Talent is something you make bloom, instinct is something you polish.\"",
            "narrator\"This is my design.\"",
            "narrator\"In pursuit of great, we failed to do good.\""
        };

        yield return DialogueSystem.instance.Say(lines);

        DialogueSystem.instance.Hide();
    }


    void Update()
    {
        List<string> lines = new List<string>();
        Conversation conversation = null;

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            lines = new List<string>()
            {
                "narrator\"Wow! I love Haikyuu!\"",
                "narrator\"It made me interested in volleyball too.\"",
            };
            conversation = new Conversation(lines);
            DialogueSystem.instance.conversationManager.Enqueue(conversation);
        }

        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            lines = new List<string>()
            {
                "narrator\"I love Arcane!\"",
                "narrator\"Perfectly describes every character's motivation and actions in the show.\"",
            };
            conversation = new Conversation(lines);
            DialogueSystem.instance.conversationManager.EnqueuePriority(conversation);
        }
    }
}
