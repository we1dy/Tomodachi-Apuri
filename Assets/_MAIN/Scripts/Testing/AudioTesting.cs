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

        IEnumerator Running2()
        {
            Character_Sprite Elara = CreateCharacter("Elara") as Character_Sprite;
            Character_Sprite MC = CreateCharacter("MC") as Character_Sprite;

            yield return new WaitForSeconds(0.5f);

            Elara.Show();

            AudioManager.instance.PlaySoundEffect("Audio/SFX/RadioStatic", loop: true);

            yield return Elara.Say("Elara\"Can someone turn the radio off?\"");

            AudioManager.instance.StopSoundEffect("RadioStatic");

            MC.Animate("Hop");
            AudioManager.instance.PlayVoice("Audio/Voices/exclamation");
            yield return MC.Say("MC\"Oh!{wa 0.75}. I'll turn it off.\"");

        }

        IEnumerator Running()
        {
            yield return new WaitForSeconds(1);

            Character_Sprite MC = CreateCharacter("MC") as Character_Sprite;
            MC.Hide();

            AudioManager.instance.PlayTrack("Audio/Music/Calm");

            GraphicPanelManager.instance.GetPanel("background").GetLayer(0, true).SetTexture("Graphics/BG Images/canteen_bg");

            yield return MC.Say("narrator\"Elara is currently at the canteen, hoping to get an early lunch before the inevitable rush that comes during the afternoon. \"");
            yield return MC.Say("narrator\"What she thought to be a peaceful reprieve from noisy classmates. . . {wa 0.75}well, let's just say what happened next comes to be a wonderful surprise.\"");

            Character_Sprite Elara = CreateCharacter("Elara") as Character_Sprite;
            Elara.SetSprite(Elara.GetSprite("elara_body"), 0);
            Elara.SetSprite(Elara.GetSprite("elara_calm"), 1);

            yield return Elara.Say("Elara\"I wonder what I should buy today. . .\"");

            Elara.Hide();

            MC.Show();
            MC.SetSprite(MC.GetSprite("mc_body"), 0);
            MC.SetSprite(MC.GetSprite("mc_curious"), 1);
            MC.Animate("Surprise");
            yield return MC.Say("MC\"Watcha doing? Watcha doing??\"");

            MC.Hide();

            Elara.Show();
            Elara.SetSprite(Elara.GetSprite("elara_body"), 0);
            Elara.SetSprite(Elara.GetSprite("elara_surprised"), 1);
            Elara.Animate("Hop");
            AudioManager.instance.PlayVoice("Audio/Voices/exclamation");

            yield return Elara.Say("Elara\"W-What the hell!?\"");

            Elara.SetSprite(Elara.GetSprite("elara_body"), 0);
            Elara.SetSprite(Elara.GetSprite("elara_flustered"), 1);
            yield return Elara.Say("Elara\"Don't creep up on me like that!\"");

            Elara.Hide();

            MC.Show();
            MC.SetSprite(MC.GetSprite("mc_body"), 0);
            MC.SetSprite(MC.GetSprite("mc_panic"), 1);
            yield return MC.Say("MC\"S-sorry!!\"");

            MC.Hide();

            Elara.Show();
            Elara.SetSprite(Elara.GetSprite("elara_body"), 0);
            Elara.SetSprite(Elara.GetSprite("elara_serious"), 1);
            yield return Elara.Say("Elara\". . . .\"");

            Elara.Hide();

            MC.Show();
            MC.SetSprite(MC.GetSprite("mc_body"), 0);
            MC.SetSprite(MC.GetSprite("mc_concerned"), 1);
            yield return MC.Say("MC\"Elara. . .I'm sorry. I didn't mean to scare you.\"");

            MC.Hide();

            Elara.Show();
            Elara.SetSprite(Elara.GetSprite("elara_body"), 0);
            Elara.SetSprite(Elara.GetSprite("elara_serious"), 1);
            yield return Elara.Say("Elara\". . . .\"");

            Elara.Hide();

            MC.Show();
            MC.SetSprite(MC.GetSprite("mc_body"), 0);
            MC.SetSprite(MC.GetSprite("mc_concerned"), 1);
            yield return MC.Say("MC\"Elara. . .?\"");

            MC.Hide();

            Elara.Show();
            Elara.SetSprite(Elara.GetSprite("elara_body"), 0);
            Elara.SetSprite(Elara.GetSprite("elara_serious"), 1);
            Elara.Animate("Shiver");
            yield return Elara.Say("Elara\". . . .\"");

            AudioManager.instance.PlayTrack("Audio/Music/Happy2", volumeCap: 0.5f);

            Elara.TransitionSprite(Elara.GetSprite("elara_smiling"), 1);
            yield return Elara.Say("Elara\"Hahahahahaha!\"");
            yield return Elara.Say("Elara\"You should have seen your face!\"");

            Elara.Hide();

            MC.Show();
            MC.SetSprite(MC.GetSprite("mc_body"), 0);
            MC.SetSprite(MC.GetSprite("mc_tired"), 1);
            yield return MC.Say("MC\"Elaraaaaa!\"");

            MC.TransitionSprite(MC.GetSprite("mc_concerned"), 1);
            yield return MC.Say("MC\"I thought you were mad at me!\"");

            MC.Hide();

            Elara.Show();
            Elara.Animate("Shiver");
            Elara.SetSprite(Elara.GetSprite("elara_body"), 0);
            Elara.SetSprite(Elara.GetSprite("elara_smiling"), 1);
            yield return Elara.Say("Elara\"Pft!{wa 0.75} Sorry! I couldn't help it.\"");

            Elara.Hide();

            MC.Show();
            MC.SetSprite(MC.GetSprite("mc_body"), 0);
            MC.SetSprite(MC.GetSprite("mc_angry"), 1);
            yield return MC.Say("MC\"Hmph! If you weren't so cute, I would've. . .\"");

            Elara.Show();
            Elara.SetSprite(Elara.GetSprite("elara_body"), 0);
            Elara.SetSprite(Elara.GetSprite("elara_smiling"), 1);
            yield return Elara.Say("Elara\"What did you say?\"");

            Elara.Hide();

            MC.Show();
            MC.SetSprite(MC.GetSprite("mc_body"), 0);
            MC.SetSprite(MC.GetSprite("mc_flustered"), 1);
            MC.Animate("Hop");
            yield return MC.Say("MC\"N-nothing! Nothing.\"");

            yield return null;
        }
    }
}