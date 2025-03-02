using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TESTING
{
    public class ChoicePanelTesting : MonoBehaviour
    {
        private ChoicePanel panel;

        void Start()
        {
            StartCoroutine(InitializeAndRun());
        }

        IEnumerator InitializeAndRun()
        {
            // Wait until ChoicePanel.instance is assigned
            yield return new WaitUntil(() => ChoicePanel.instance != null);

            panel = ChoicePanel.instance;

            if (panel == null)
            {
                Debug.LogError("❌ ChoicePanel instance is still NULL after waiting!");
                yield break;
            }

            Debug.Log("✅ ChoicePanel instance found! Proceeding to show choices.");

            yield return StartCoroutine(Running());
        }

        IEnumerator Running()
        {
            string[] choices = new string[]
            {
                "Absolutely!",
                "Uh... maybe?",
                "Nope.",
                "In all timelines, in all possibilities, only you."
            };

            panel.Show("Did you study the material I sent you?", choices);

            while (panel.isWaitingOnUserChoice)
                yield return null;

            var decision = panel.lastDecision;

            Debug.Log($"✅ Made choice {decision.answerIndex}: '{decision.choices[decision.answerIndex]}'");
        }
    }
}
