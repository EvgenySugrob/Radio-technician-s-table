using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    [SerializeField] private bool isOpen;
    [SerializeField] GameObject inventoryWindow;

    public void OpenClose()
    {
        if (isOpen == false)
        {
            inventoryWindow.SetActive(true);
            isOpen= true;
        }
        else
        {
            inventoryWindow.SetActive(false);
            isOpen= false;
        }
    }
}
