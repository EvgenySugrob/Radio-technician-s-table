using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolderInteract : MonoBehaviour
{
    [Header("Hold solder input")]
    [SerializeField] float holdDuration = 1f;
    [SerializeField] GameObject progressBarSolder;
    [SerializeField] Image goodProgress;
    [SerializeField] Image badProgress;
    [SerializeField] private float holdTimer = 0f;
    [SerializeField] Transform startStandPosition;

    [Header("MainSolder Settings")]
    [SerializeField] SolderStation solderStation;

    [Header("SolderStandDetetion")]
    [SerializeField] SolderStandDetect solderStandDetect;

    [Header("Inputs")]
    [SerializeField] DragAndDrop dragAndDrop;

    private Rigidbody solderRb;

    private void Start()
    {
        solderRb= GetComponent<Rigidbody>();
    }

    public void EnableSolderDetectionZone()
    {
        solderStandDetect.EnableDisableZone(true);
    }

    public void ReturnToStand()
    {
        solderStandDetect.EnableDisableZone(false);
        dragAndDrop.RemoveSolderHand();

        transform.position = startStandPosition.position;
        transform.rotation = startStandPosition.rotation;
    }

    public void StartSoldering()
    {
        if(solderStation.FunctionalityCheck())
        {
            progressBarSolder.SetActive(true);
            HoldSolder();
        }
        else
        {
            Debug.Log("Станция не включена");
        }
    }
    public void StopSoldering()
    {
        progressBarSolder.SetActive(false);
    }

    private void HoldSolder()
    {
        holdTimer += Time.deltaTime;
        goodProgress.fillAmount = holdTimer / holdDuration;
        if (holdTimer >= holdDuration)
        {
            Debug.Log("Cool");//StartBad progress
        }
    }

    private void ResetHold() //Сброс режима пайки, путем заморозки объекта или возвращение его на место
    {
        holdTimer = 0;                          //заменить на продолжение пайки
        goodProgress.fillAmount = 0;            //так же
    }

}
