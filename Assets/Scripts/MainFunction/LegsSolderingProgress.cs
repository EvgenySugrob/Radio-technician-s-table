using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsSolderingProgress : MonoBehaviour
{
    [SerializeField] float finalProgress = 1f;
    [SerializeField] float currentProgress = 0;
    [SerializeField] Transform solderLegsScale;
    [SerializeField] bool progressDone;
    [SerializeField] CheckSolderOnLegsElement checkSolderOnLegsElement;

    private float amountForBar;

    private void Awake()
    {
        solderLegsScale = transform.GetChild(0).transform;
        checkSolderOnLegsElement = transform.parent.GetComponent<CheckSolderOnLegsElement>();
    }
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
            checkSolderOnLegsElement.CheckLegsSoldering();
            Debug.Log("Перегрев ножки элемента");
        }
        return amountForBar;
    }

    public bool GetStatusLegs()
    {
        return progressDone;
    }
}
