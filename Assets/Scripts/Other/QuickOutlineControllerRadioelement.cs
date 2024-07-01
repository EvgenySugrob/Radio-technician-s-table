using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeLegsOutline
{
    withoutLegs,
    cutterLegs
}

public class QuickOutlineControllerRadioelement : MonoBehaviour
{
    public TypeLegsOutline typeLegsOutline;
    
    [Header("OutlineScript")]
    [SerializeField] List<Outline> outlineList;
    [SerializeField] List<Outline> defaultsLegs;
    [SerializeField] List<Outline> cuttersLegs;

    [Header("Inputs")]
    [SerializeField] DragAndDrop dragAndDrop;
    [SerializeField] DragAndRotation dragAndRotation;

    [Header("State")]
    [SerializeField] bool globalActivate = true;
    [SerializeField] bool isCuttersLegs;
    [SerializeField] bool isActive;
    public bool onlyLegsDetect;

    private int countCuttersLeg = 0; 

    public void EnableOutline()
    {
        if (isActive == false && globalActivate)
        {
            switch(typeLegsOutline)
            {
                case TypeLegsOutline.withoutLegs:
                    RadioelementHousingOutline(true);
                    break;
                case TypeLegsOutline.cutterLegs:
                    RadioelementHousingOutline(true);
                    RadioelementLegsOutline(true);
                    break;
            }
            isActive = true;
        }
    }
    public void DisableOutline()
    {
        if (isActive && globalActivate)
        {
            switch (typeLegsOutline)
            {
                case TypeLegsOutline.withoutLegs:
                    RadioelementHousingOutline(false);
                    break;
                case TypeLegsOutline.cutterLegs:
                    RadioelementHousingOutline(false);
                    RadioelementLegsOutline(false);
                    break;

            }
            isActive = false;
        }
    }
    public void LegsDetectOutlineEnable(bool isEnable)
    {
        foreach(Outline outline in defaultsLegs)
        {
            outline.enabled = isEnable;
        }
    }
    public void GlobalActivateDiactivate(bool isACtive)
    {
        globalActivate = isACtive;
    }
    public bool IsGlobalActive()
    {
        return globalActivate;
    }
    public void CheckCountCuttersLeg(int add)
    {
        countCuttersLeg += add;
        if (countCuttersLeg>=add)
        {
            isCuttersLegs = true;
        }
    }


    private void RadioelementHousingOutline(bool isEnable)
    {
        foreach (Outline outline in outlineList)
        {
            outline.enabled = isEnable;
        }
    }
    private void RadioelementLegsOutline(bool isEnable)
    {
        if (isCuttersLegs)
        {
            foreach (Outline outline in cuttersLegs)
            {
                outline.enabled = isEnable;
            }
        }
        else
        {
            foreach (Outline outline in defaultsLegs)
            {
                outline.enabled = isEnable;
            }
        }
    }
}
