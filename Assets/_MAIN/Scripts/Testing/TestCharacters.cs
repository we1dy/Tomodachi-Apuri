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
        private Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name);
        void Start()
        {
            //
            //Character Mob = CharacterManager.instance.CreateCharacter("Mob");
            //Character Reigen = CharacterManager.instance.CreateCharacter("Reigen");
            StartCoroutine(Test());
        }

        IEnumerator Test()
        {
            Character_Sprite MC = CreateCharacter("MC") as Character_Sprite;

            Character_Sprite Elara = CreateCharacter("Elara") as Character_Sprite;

            yield return MC.Show();

            yield return Elara.Show();

            Elara.SetPriority(1);

            yield return new WaitForSeconds(1);

            CharacterManager.instance.SortCharacters(new string[] { "MC" });

            yield return new WaitForSeconds(1);

            CharacterManager.instance.SortCharacters();

            yield return new WaitForSeconds(1);

            CharacterManager.instance.SortCharacters(new string[] { "Elara" });

            yield return null;
        }



        // Update is called once per frame
        void Update()
        {

        }
    }
}