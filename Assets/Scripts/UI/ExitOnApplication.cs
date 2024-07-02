using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitOnApplication : MonoBehaviour
{
    [SerializeField] DragAndDrop dragAndDrop;
    [SerializeField] DragAndRotation dragAndRotation;
    [SerializeField] PlayerController playerController;

    [SerializeField] GameObject WindowExitDialoge;
    private bool rotationUsed;

    public void Exit()
    {
        Application.Quit();
    }

    public void OpenDialogExit(bool isOpen)
    {
        if(isOpen)
        {
            if (dragAndRotation.enabled)
            {
                rotationUsed = true;
            }
            else
            {
                rotationUsed = false;
            }
            dragAndDrop.enabled = false;
            dragAndRotation.enabled = false;
            playerController.enabled = false;
            WindowExitDialoge.SetActive(true);
        }
        else
        {
            if(rotationUsed)
            {
                dragAndRotation.enabled = true;
            }
            else
            {
                dragAndDrop.enabled = true;
            }
            playerController.enabled = true;
            WindowExitDialoge.SetActive(false);
        }
    }
}
