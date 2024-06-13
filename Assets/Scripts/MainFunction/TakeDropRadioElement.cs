using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDropRadioElement : MonoBehaviour
{
    [Header("NormalTweezersButton")]
    [SerializeField] GameObject takeButton;
    [SerializeField] GameObject dropButton;
    [SerializeField] GameObject ortoViewGo;

    [Header("LittleTweezersButton")]
    [SerializeField] GameObject littleTakeButton;
    [SerializeField] GameObject littleDropButton;
    [SerializeField] GameObject littleOrtoViewGo;

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

    public void EnableButtonOrtoView(bool isActive,bool isLittleTweezers)
    {
        if (isLittleTweezers)
        {
            OrtoViewBtLittleTweezers(isActive);
        }
        else
        {
            OrtoViewBtTweezers(isActive);
        }
    }

    public void AllButtonDisable()
    {
        takeButton.SetActive(false);
        dropButton.SetActive(false);

        littleTakeButton.SetActive(false);
        littleDropButton.SetActive(false);

        DisableAllOrtoViewBt();
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

    private void OrtoViewBtLittleTweezers(bool isActive)
    {
        littleOrtoViewGo.SetActive(isActive);
    }
    private void OrtoViewBtTweezers(bool isActive)
    {
        ortoViewGo.SetActive(isActive);
    }

    public void DisableAllOrtoViewBt()
    {
        ortoViewGo.SetActive(false);
        littleOrtoViewGo.SetActive(false);
    }
}
