using CHARACTERS;
using System.Collections;
using UnityEngine;

public class InputPanelTesting : MonoBehaviour
{
    public InputPanel inputPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Running());
    }

    IEnumerator Running()
    {
        Character Elara = CharacterManager.instance.CreateCharacter("Elara", revealAfterCreation: true);

        yield return Elara.Say("Elara\"What's your name?\"");

        inputPanel.Show("Input your name:");

        while (inputPanel.isWaitingOnUserInput)
            yield return null;

        string characterName = inputPanel.lastInput;

        yield return Elara.Say($"Elara\"Hm, nice to meet you I guess, {characterName}\"");

    }

}
