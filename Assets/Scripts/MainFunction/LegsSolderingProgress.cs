using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsSolderingProgress : MonoBehaviour
{
    [SerializeField] float finalProgress = 1f;
    [SerializeField] float currentProgress = 0;
    [SerializeField] Transform solderLegsScale;
    [SerializeField] bool progressDone;
    private float amountForBar;

    public void SetFinalProgress(float duration)
    {
        finalProgress= duration;
    }

    public float SolderingLegsElement()
    {
        currentProgress += Time.deltaTime;
        amountForBar = currentProgress / finalProgress;
        solderLegsScale.localScale = Vector3.Lerp(solderLegsScale.localScale, Vector3.one, amountForBar);

        if(currentProgress>=finalProgress)
        {
            progressDone = true;
            Debug.Log("Перегрев ножки элемента");
        }
        return amountForBar;
    }
}
