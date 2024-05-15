using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOpenUIComponent : MonoBehaviour
{
    [Header("UI component for check")]
    [SerializeField] GameObject inventoryWindow;
    [SerializeField] GameObject windowChoiseComponent;
    [SerializeField] GameObject coloredResistmark;
    [SerializeField] GameObject sliderMolding;

    private bool uiNoActive;

    private void Update()
    {
        if (!inventoryWindow.activeSelf && !windowChoiseComponent.activeSelf && !coloredResistmark.activeSelf && !sliderMolding.activeSelf)
        {
            uiNoActive = true;
        }
        else
        {
            uiNoActive = false;
        }
    }

    public bool NonActiveUIComponent()
    {
        return uiNoActive;
    }
}
