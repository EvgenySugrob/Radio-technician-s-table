using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsSolderingProgress : MonoBehaviour
{
    [SerializeField] float finalProgress = 1f;
    [SerializeField] float currentProgress = 0;
    [SerializeField] Transform solderLegsScale;
    [SerializeField] bool progressDone;
    [SerializeField] bool isFluxiLegs;
    [SerializeField] CheckSolderOnLegsElement checkSolderOnLegsElement;
    [SerializeField] LegsSolderingProgress neighboringLeg;

    [Header("Fluxing legs for remove")]
    [SerializeField] float fluxingDuration = 1.5f;
    [SerializeField] float fluxingTimer = 0f;
    [SerializeField] float amountBarProgressFlux = 0f;

    [Header("Unsoldering")]
    [SerializeField] float unsolderingDuration;
    [SerializeField] float currentUnsolderingProgress;
    private float amountUnsolderingBar;

    private float amountForBar;

    private void Awake()
    {
        solderLegsScale = transform.GetChild(0).transform;
        checkSolderOnLegsElement = transform.parent.GetComponent<CheckSolderOnLegsElement>();
    }
    public void SetFinalProgress(float duration)
    {
        finalProgress= duration;
        unsolderingDuration = finalProgress;
    }

    public float SolderingLegsElement()
    {
        currentProgress += Time.deltaTime;
        amountForBar = currentProgress / finalProgress;
        solderLegsScale.localScale = Vector3.Lerp(solderLegsScale.localScale, Vector3.one, amountForBar);

        if(currentProgress>=finalProgress)
        {
            progressDone = true;
            checkSolderOnLegsElement.CheckLegsSoldering();
        }
        return amountForBar;
    }

    public float UnsolderingLegs()
    {
        currentUnsolderingProgress += Time.deltaTime;
        amountUnsolderingBar = currentUnsolderingProgress / unsolderingDuration;

        solderLegsScale.localScale = Vector3.Lerp(solderLegsScale.localScale, Vector3.zero, amountForBar);
        if (currentUnsolderingProgress>=unsolderingDuration)
        {
            progressDone = false;
            checkSolderOnLegsElement.CheckLegsUnsoldering();
        }

        return amountUnsolderingBar;
    }

    public bool GetStatusLegs()
    {
        return progressDone;
    }
    
    public bool GetFluxingLeg() 
    {
        return isFluxiLegs;
    }
    public float FluxinglegsElement()
    {
        fluxingTimer += Time.deltaTime;
        amountBarProgressFlux = fluxingTimer / fluxingDuration;
        if(fluxingTimer>=fluxingDuration)
        {
            SetFluxing(true);
        }
        return amountBarProgressFlux;
    }

    public void SetFluxing(bool isFlux)
    {
        isFluxiLegs = isFlux;
    }
}
