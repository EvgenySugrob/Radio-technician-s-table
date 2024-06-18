using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    [SerializeField] bool thereIsSolder;
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

    [Header("SolderOnIronTip")]
    [SerializeField] float duration = 1f;
    [SerializeField] Transform solderOnIronTip;
    //private float maxSizeSolderOnTip = 1f;
    //private float minSizeSolderOnTip = 0f;
    private float timer = 0;

    [Header("MainSolder Settings")]
    [SerializeField] SolderStation solderStation;

    [Header("SolderStandDetetion")]
    [SerializeField] SolderStandDetect solderStandDetect;

    [Header("Inputs")]
    [SerializeField] DragAndDrop dragAndDrop;

    [Header("SolderDetectionSlot")]
    [SerializeField] GameObject ortoViewBt;
    [SerializeField] SolderSlotsDetect solderSlotsDetect;
    [SerializeField] Transform radioelementSlot;

    private Rigidbody solderRb;

    private void Start()
    {
        solderRb= GetComponent<Rigidbody>();
        mixedColor = startColor;
    }

    public void SetRadioelement(Transform slot)
    {
        radioelementSlot = slot;
        radioelementSlot.GetComponent<LegsSolderingProgress>().SetFinalProgress(holdDuration);
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
            IronTinning();
            TakingSolder();
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

    public bool GetReadySolder()
    {
        return isReady;
    }

    public void RosinCheck(bool isActive)
    {
        isRosinCheck = isActive;
    } 
    
    public void IsSolderingPointEnable(bool IsEnable)
    {
        isSolderingPoint = IsEnable;
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
            Debug.Log("Лужение");
            ironTinningTimer += Time.deltaTime;
            mixedColor = Color.Lerp(mixedColor, endColor, ironTinningTimer);
            rosinMeshRenderer.material.color = mixedColor;
            
            if(isReady ==false)
            {
                Vector3 consumedPartScale = consumedPart.localScale;
                consumedPartScale.z -= scaleReductionStep * Time.deltaTime;
                consumedPart.localScale = new Vector3(consumedPart.localScale.x, consumedPart.localScale.y, consumedPartScale.z);
            }

            if (ironTinningTimer>=ironTinningDuration)
            {
                isReady = true;
                isIronTin = true;
                Debug.Log("Залудил");
            }
        }
    }
    private void Rosining()
    {
        if(isRosinCheck)
        {
            Debug.Log("Канифоль");
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
        if(thereIsSolder && isSolderingPoint) //Выполняется только при пайке для распайки другое условие
        {
            Debug.Log("Пайка");
            float currentProgress = radioelementSlot.GetComponent<LegsSolderingProgress>().SolderingLegsElement();
            holdTimer += Time.deltaTime;
            //holdTimer = currentProgress;
            Vector3 currentScaleSolderOnIronTip = solderOnIronTip.localScale;
            progressBarSolder.SetActive(true);

            Vector3 newScale = Vector3.Lerp(currentScaleSolderOnIronTip, Vector3.zero, currentProgress);
            solderOnIronTip.localScale = newScale;

            goodProgress.fillAmount = currentProgress;
            if (holdTimer >= holdDuration)
            {
                thereIsSolder = false;
                isIronTin= false;
                timer = 0;
                Debug.Log("Cool");//StartBad progress
            }

        }
       
    }
    private void TakingSolder()
    {
        if(isReady && isIronTin && isRosin && isIronTinnig)
        {
            Debug.Log("Забор припоя");
            Vector3 currentScale = solderOnIronTip.localScale;
            timer += Time.deltaTime;
            Vector3 newScale = Vector3.Lerp(currentScale, Vector3.one, timer);
            solderOnIronTip.localScale = newScale;
            if(timer>=duration)
            {
                Debug.Log("Припой на паяльнике");
                thereIsSolder = true;
                holdTimer = 0;
            }
        }
    }
    private void ResetHold() //Сброс режима пайки, путем заморозки объекта или возвращение его на место
    {
        holdTimer = 0;                          //заменить на продолжение пайки
        goodProgress.fillAmount = 0;            //так же
    }


    public void StartSolderingDetectionSlot(bool isActive)
    {
        solderSlotsDetect.DetecActive(isActive);
    }
    public void ActiveOrtoBt(bool isGo)
    {
        ortoViewBt.SetActive(isGo);
    }

    public void SetHoldTimer()
    {

    }
}
