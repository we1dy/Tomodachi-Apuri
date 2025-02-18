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
            Character_Sprite MC = CharacterManager.instance.CreateCharacter("MC") as Character_Sprite;

            Character_Sprite Elara = CharacterManager.instance.CreateCharacter("Elara") as Character_Sprite;

            yield return MC.Show();

            MC.Say("MC\"Hello!\"");

            yield return new WaitForSeconds(2);

            Sprite bodySprite = MC.GetSprite("mc_concerned");
            yield return MC.TransitionSprite(bodySprite, 0);

            MC.Say("MC\"Are you okay?\"");
            yield return new WaitForSeconds(1);

            yield return MC.Hide();

            Elara.Show();

            bodySprite = Elara.GetSprite("elara_ch");
            Elara.TransitionSprite(bodySprite, 0);

            yield return new WaitForSeconds(1);

            Elara.Say("Elara\"I-I'm fine. {wa 1} No need to worry.\"");

            bodySprite = Elara.GetSprite("elara_flustered");
            Elara.TransitionSprite(bodySprite, 0);

            yield return null;
        }



        // Update is called once per frame
        void Update()
        {

        }
    }
}