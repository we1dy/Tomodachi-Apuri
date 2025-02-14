using CHARACTERS;
using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TESTING
{
    public class TestCharacters : MonoBehaviour
    {
        public TMP_FontAsset tempFont;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //Character Elara = CharacterManager.instance.CreateCharacter("Elara");
            //Character Mob = CharacterManager.instance.CreateCharacter("Mob");
            //Character Reigen = CharacterManager.instance.CreateCharacter("Reigen");
            StartCoroutine(Test());
        }

        IEnumerator Test()
        {
            yield return new WaitForSeconds(1f);

            Character MC = CharacterManager.instance.CreateCharacter("MC");

            yield return new WaitForSeconds(1f);

            yield return MC.Hide();

            yield return new WaitForSeconds(0.5f);

            yield return MC.Show();

            yield return MC.Say("MC\"Hello!\"");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}