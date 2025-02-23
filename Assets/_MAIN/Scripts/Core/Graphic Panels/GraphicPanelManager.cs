using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicPanelManager : MonoBehaviour
{
    public static GraphicPanelManager instance {  get; private set; }

    public const float DEFFAULT_TRANSITION_SPEED = 3f;

    [SerializeField] private GraphicPanel[] allPanels;

    private void Awake()
    {
        instance = this;    
    }

    public GraphicPanel GetPanel(string name)
    {
        name = name.ToLower();

        foreach (var panel in allPanels)
        {
            if (panel.panelName.ToLower() == name)
                return panel;
        }

        return null;    
    }

}

