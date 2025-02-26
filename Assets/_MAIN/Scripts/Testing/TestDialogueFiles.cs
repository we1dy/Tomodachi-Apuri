using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class TestDialogueFiles : MonoBehaviour
{
    [SerializeField] private TextAsset fileToRead = null;
    void Start()
    {
        StartConversation();
    }

    void StartConversation()
    {
        List<string> lines = FileManager.ReadTextAsset(fileToRead);

        //foreach (string line in lines)
        //{ 
        //    if (string.IsNullOrWhiteSpace(line))
        //        continue;

        //    DIALOGUE_LINE dl = DialogueParser.Parse(line);

        //    for (int i = 0; i < dl.commandsData.commands.Count; i++)
        //    { 
        //        DL_COMMAND_DATA.Command command = dl.commandsData.commands[i];
        //        Debug.Log($"Command [{i}] '{command.name}' has arguments [{string.Join(", ", command.arguments)}]");
        //    }
        //}
        DialogueSystem.instance.Say(lines);
    }

    private void Update()
    {
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            DialogueSystem.instance.dialogueContainer.Hide();

        else if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            DialogueSystem.instance.dialogueContainer.Show();
    }
}


