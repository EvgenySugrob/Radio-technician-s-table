using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSolderOnLegsElement : MonoBehaviour
{
    [SerializeField] PrefabRisistNominalSetting prefabRisistNominalSetting;
    [SerializeField] List<LegsSolderingProgress> legsSolderingProgressesList;

    private void Awake()
    {
        prefabRisistNominalSetting = transform.parent.GetComponent<PrefabRisistNominalSetting>();

        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    if(transform.GetChild(i).GetComponent<LegsSolderingProgress>())
        //    {
        //        legsSolderingProgressesList.Add(transform.GetChild(i).GetComponent<LegsSolderingProgress>());
        //    }
        //}
    }

    public void CheckLegsSoldering()
    {
        int countSolderingLegs = 0;
        foreach (LegsSolderingProgress legs in legsSolderingProgressesList)
        {
            if(legs.GetStatusLegs())
            {
                countSolderingLegs++;
            }
        }
        if(countSolderingLegs == legsSolderingProgressesList.Count)
        {
            prefabRisistNominalSetting.FullSolderingElement(true);
        }
    }

    public void CheckLegsUnsoldering()
    {
        int countUnsolderinglegs = 0;
        foreach(LegsSolderingProgress legs in legsSolderingProgressesList)
        {
            if(legs.GetStatusLegs() == false)
            {
                countUnsolderinglegs++;
            }
        }
        if(countUnsolderinglegs == legsSolderingProgressesList.Count)
        {
            prefabRisistNominalSetting.FullUnsolderingElement();
            foreach(LegsSolderingProgress legs in legsSolderingProgressesList)
            {
                legs.SetFluxing(false);
            }
        }
    }

    public void IsGrabTweezersLegsCheck(bool isGrab)
    {
        foreach (LegsSolderingProgress leg in legsSolderingProgressesList)
        {
            leg.TweezersGrab(isGrab);
        }
    }
}
