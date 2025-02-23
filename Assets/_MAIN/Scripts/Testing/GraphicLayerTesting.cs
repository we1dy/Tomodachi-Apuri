using System.Collections;
using DIALOGUE;
using UnityEngine;
using CHARACTERS;

public class GraphicLayerTesting : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        StartCoroutine(RunningLayers());
    }

    IEnumerator RunningLayers()
    {
        GraphicPanel panel = GraphicPanelManager.instance.GetPanel("Background");
        GraphicLayer layer0 = panel.GetLayer(0, true);
        GraphicLayer layer1 = panel.GetLayer(1, true);

        layer0.SetVideo("Graphics/BG Videos/Nebula");
        layer1.SetTexture("Graphics/BG Images/SpaceshipInterior");

        //yield return new WaitForSeconds(1);

        GraphicPanel cinematic = GraphicPanelManager.instance.GetPanel("Cinematic");
        GraphicLayer cinLayer = cinematic.GetLayer(0, true);

        Character MC = CharacterManager.instance.CreateCharacter("MC", true);

        yield return MC.Say("MC\"Why are we at space right now????\"");

        cinLayer.SetTexture("Graphics/Gallery/pup");

        yield return DialogueSystem.instance.Say("Narrator", "Who cares?? Look at the pupppy!!");

        cinLayer.Clear();

        yield return new WaitForSeconds(1);

        panel.Clear();
    }

}
