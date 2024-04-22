using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    private bool isOpen;
    [SerializeField] GameObject inventoryWindow;

    public void OpenClose()
    {
        if (isOpen == false)
        {
            inventoryWindow.SetActive(true);
        }
        else
        {
            inventoryWindow.SetActive(false);
        }
    }
}
