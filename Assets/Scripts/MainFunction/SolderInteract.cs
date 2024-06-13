using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolderInteract : MonoBehaviour
{
    [Header("Preparing for work")]
    [SerializeField] bool isRosin;
    [SerializeField] bool isIronTin;
    [SerializeField] bool isIronTinnig;
    [SerializeField] bool isRosinCheck;
   
    [Header("Soldering ready for work")]
    [SerializeField] bool isReady;
    [SerializeField] bool isSolderingPoint;

    [Header("Hold solder input")]
    [SerializeField] float holdDuration = 1f;
    [SerializeField] GameObject progressBarSolder;
    [SerializeField] Image goodProgress;
    [SerializeField] Image badProgress;
    [SerializeField] float holdTimer = 0f;
    [SerializeField] Transform startStandPosition;

    [Header("RosinTimeParam")]
    [SerializeField] float rosinHoldDuration = 1f;
    [SerializeField] float rosinHoldTimer = 0f;
    [SerializeField] RosinSmokeDirection rosinSmokeDirection;

    [Header("IronTinningColorChange")]
    [SerializeField] float ironTinningDuration = 2f;
    [SerializeField] float ironTinningTimer = 0f;
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;
    [SerializeField] private Color mixedColor;
    [SerializeField] MeshRenderer rosinMeshRenderer;
    private Transform consumedPart;
    [SerializeField] float scaleReductionStep = 0.05f;

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
        mixedColor = startColor;
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
            Rosining();
            HoldSolder();
            IronTinning();
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

    public bool GetReadySolder()
    {
        return isReady;
    }

    public void RosinCheck(bool isActive)
    {
        isRosinCheck = isActive;
    }    

    public void IronTinningCheck(bool isActive, Transform solderPart)
    {
        if (isRosin)
        {
            consumedPart = solderPart;
            isIronTinnig = isActive;
        }
    }

    private void IronTinning()
    {
        if(isIronTinnig)
        {
            ironTinningTimer += Time.deltaTime;
            mixedColor = Color.Lerp(mixedColor, endColor, ironTinningTimer);
            rosinMeshRenderer.material.color = mixedColor;

            Vector3 consumedPartScale = consumedPart.localScale;
            consumedPartScale.z -= scaleReductionStep * Time.deltaTime;
            consumedPart.localScale = new Vector3(consumedPart.localScale.x, consumedPart.localScale.y, consumedPartScale.z);

            if (ironTinningTimer>=ironTinningDuration)
            {
                isReady = true;
                Debug.Log("Залудил");
            }
        }
    }

    private void Rosining()
    {
        if(isRosinCheck)
        {
            rosinSmokeDirection.ActiveSmoke(true);
            rosinHoldTimer += Time.deltaTime;
            if (rosinHoldTimer>=rosinHoldDuration)
            {
                isRosin = true;
                rosinSmokeDirection.ActiveSmoke(false);
                Debug.Log("WellDoneKanifol");
            }
        }
    }

    private void HoldSolder()
    {
        if(isReady && isSolderingPoint)
        {
            progressBarSolder.SetActive(true);
            holdTimer += Time.deltaTime;
            goodProgress.fillAmount = holdTimer / holdDuration;
            if (holdTimer >= holdDuration)
            {
                Debug.Log("Cool");//StartBad progress
            }

        }
       
    }

    private void ResetHold() //Сброс режима пайки, путем заморозки объекта или возвращение его на место
    {
        holdTimer = 0;                          //заменить на продолжение пайки
        goodProgress.fillAmount = 0;            //так же
    }


    
}
