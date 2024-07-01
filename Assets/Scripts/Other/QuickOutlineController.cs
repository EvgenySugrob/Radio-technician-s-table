using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickOutlineController : MonoBehaviour
{
    [Header("OutlineScript")]
    [SerializeField] List<Outline> outlineList;

    [Header("Inputs")]
    [SerializeField] DragAndDrop dragAndDrop;
    [SerializeField] DragAndRotation dragAndRotation;

    [Header("State")]
    [SerializeField] bool isActive;

    public void EnableOutline()
    {
        if(isActive == false)
        {
            foreach (Outline outline in outlineList)
            {
                outline.enabled = true;
            }
            isActive = true;
        }
    }
    public void DisableOutline()
    {
        if (isActive)
        {
            foreach (Outline outline in outlineList)
            {
                outline.enabled = false;
            }
            isActive = false;
        }
    }
}
