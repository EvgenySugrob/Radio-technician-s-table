using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDropRadioElement : MonoBehaviour
{
    [Header("NormalTweezersButton")]
    [SerializeField] GameObject takeButton;
    [SerializeField] GameObject dropButton;

    [Header("LittleTweezersButton")]
    [SerializeField] GameObject littleTakeButton;
    [SerializeField] GameObject littleDropButton;

    public void TakeDropButtonActivate(bool isTakeElement,bool isLittleTweezers)
    {
        if (isLittleTweezers) 
        {
            LittleTakeDropActive(isTakeElement);
        }
        else 
        {
            NormalTakeDropActive(isTakeElement);
        }
    }

    public void AllButtonDisable()
    {
        takeButton.SetActive(false);
        dropButton.SetActive(false);
        littleTakeButton.SetActive(false);
        littleDropButton.SetActive(false);
    }

    private void NormalTakeDropActive(bool isTakeElement)
    {
        if (isTakeElement)
        {
            takeButton.SetActive(false);
            dropButton.SetActive(true);

            littleTakeButton.SetActive(false);
            littleDropButton.SetActive(false);
        }
        else 
        {
            takeButton.SetActive(true);
            dropButton.SetActive(false);

            littleTakeButton.SetActive(false);
            littleDropButton.SetActive(false);
        }
    }
    private void LittleTakeDropActive(bool isTakeElement)
    {
        if (isTakeElement)
        {
            littleTakeButton.SetActive(false);
            littleDropButton.SetActive(true);

            takeButton.SetActive(false);
            dropButton.SetActive(false);
        }
        else
        {
            littleTakeButton.SetActive(true);
            littleDropButton.SetActive(false);

            takeButton.SetActive(false);
            dropButton.SetActive(false);
        }
    }
}
