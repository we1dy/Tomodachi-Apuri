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
            StartCoroutine(Test());
        }

        IEnumerator Test()
        {
            Character_Sprite MC = CreateCharacter("MC") as Character_Sprite;
            Character_Sprite Elara = CreateCharacter("Elara") as Character_Sprite;
            MC.isVisible = false;

            yield return new WaitForSeconds(2);

            Elara.TransitionSprite(Elara.GetSprite("elara_surprised"), layer: 1);
            Elara.TransitionSprite(Elara.GetSprite("elara_body"));
            Elara.Animate("Hop");
           
            yield return new WaitForSeconds(1);

            Elara.TransitionSprite(Elara.GetSprite("elara_uncomfortable"), layer: 1);
            Elara.TransitionSprite(Elara.GetSprite("elara_body"));
            Elara.Animate("Shiver", true);
            yield return Elara.Say("Elara\"Oh. . .{wa 1}It’s you.\"");
            Elara.Animate("Shiver", false);
            yield return Elara.Hide();

            MC.Show();
            MC.TransitionSprite(MC.GetSprite("mc_angry"), layer: 1);
            MC.TransitionSprite(MC.GetSprite("mc_body"));
            yield return MC.Say("MC\"Nice to see you too, I guess.\"");
            yield return MC.Hide();

            Elara.Show();
            Elara.TransitionSprite(Elara.GetSprite("elara_ch"), layer: 1);
            Elara.TransitionSprite(Elara.GetSprite("elara_body"));
            yield return Elara.Say("Elara\"Ha! As if.{wa 1} No need to be fake.\"");
            yield return Elara.Hide();

            MC.Show();

            MC.TransitionSprite(MC.GetSprite("mc_curious"), layer: 1);
            MC.TransitionSprite(MC.GetSprite("mc_body"));
            MC.Animate("Hop");
            yield return MC.Say("MC\"Hey! What's that supposed to mean?\"");

            yield return null;
        }



        // Update is called once per frame
        void Update()
        {

        }
    }
}