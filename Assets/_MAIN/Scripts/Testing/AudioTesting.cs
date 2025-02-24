using System.Collections;
using System.Globalization;
using CHARACTERS;
using DIALOGUE;
using UnityEngine;

namespace TESTING
{
    public class AudioTesting : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            StartCoroutine(Running());
        }

        Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name);        

        IEnumerator Running()
        {
            Character_Sprite Elara = CreateCharacter("Elara") as Character_Sprite;
            Elara.Show();

            yield return new WaitForSeconds(0.5f);

            AudioManager.instance.PlaySoundEffect("Audio/SFX/RadioStatic", loop: true);

            yield return Elara.Say("Elara\"I'm going to turn off the radio now.\"");

            AudioManager.instance.StopSoundEffect("RadioStatic");

            yield return Elara.Say("Elara\"It's off.\"");
        }
        
    }
}