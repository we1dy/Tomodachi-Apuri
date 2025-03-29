using CHARACTERS;
using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

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

            yield return Elara.UnHighLight();       
            Elara.TransitionSprite(Elara.GetSprite("elara_calm"), layer: 1);
            Elara.TransitionSprite(Elara.GetSprite("elara_body"));

            //List<string> lines = new List<string>()
            //{
            //    "narrator\"She’s dressed neatly, the standard college uniform and a pink sweater wrapped around her waist, her long dark hair swishing with every movement. A notebook is tucked under her arm, and her brows are slightly furrowed, like she’s already impatient about something.\"",
            //    "narrator\"Then she spots you.\"",
            //    "narrator\"For a brief moment, her carefully composed expression falters.\""
            //};

            yield return MC.Say("narrator\"She’s dressed neatly, the standard college uniform and a pink sweater wrapped around her waist, her long dark hair swishing with every movement. A notebook is tucked under her arm, and her brows are slightly furrowed, like she’s already impatient about something.\"");
            
            Elara.TransitionSprite(Elara.GetSprite("elara_surprised"), layer: 1);
            Elara.TransitionSprite(Elara.GetSprite("elara_body"));

            yield return MC.Say("narrator\"Then she spots you.\"");
            yield return MC.Say("narrator\"For a brief moment, her carefully composed expression falters.\"");

            yield return Elara.HighLight();

            yield return new WaitForSeconds(1);

            Elara.TransitionSprite(Elara.GetSprite("elara_surprised"), layer: 1);
            Elara.TransitionSprite(Elara.GetSprite("elara_body"));
            Elara.Animate("Hop");
           
            yield return new WaitForSeconds(1);

            Elara.TransitionSprite(Elara.GetSprite("elara_uncomfortable"), layer: 1);
            Elara.TransitionSprite(Elara.GetSprite("elara_body"));
            yield return Elara.Say("Elara\"Oh. . .{wa 0.75}It’s you.\"");
            yield return Elara.Hide();

            MC.Show();
            MC.TransitionSprite(MC.GetSprite("mc_angry"), layer: 1);
            MC.TransitionSprite(MC.GetSprite("mc_body"));
            yield return MC.Say("MC\"Nice to see you too, I guess.\"");
            yield return MC.Hide();

            Elara.Show();
            Elara.TransitionSprite(Elara.GetSprite("elara_ch"), layer: 1);
            Elara.TransitionSprite(Elara.GetSprite("elara_body"));
            yield return Elara.Say("Elara\"Ha! As if.{wa 0.75} No need to be fake.\"");
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