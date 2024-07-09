using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SolderInteract : MonoBehaviour
{
    [Header("Preparing for work")]
    [SerializeField] bool isRosin;
    [SerializeField] bool isIronTin;
    [SerializeField] bool isIronTinnig;
    [SerializeField] bool isRosinCheck;
    [SerializeField] bool isBadReady;

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
    [SerializeField] float badHoldDuration = 0f;
    private RectTransform progressBarRect;
    private float badTimer = 0;

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
    [SerializeField] GameObject ironTinningProgress;
    [SerializeField] Image tinnignFill;
    [SerializeField] bool progressTinningDisable;

    [Header("SolderOnIronTip")]
    [SerializeField] float duration = 1f;
    [SerializeField] Transform solderOnIronTip;
    [SerializeField] private float timer = 0;
    [SerializeField] GameObject takingSolderProgress;
    [SerializeField] Image progressFill;
    [SerializeField] bool progressSolderOnTipDisable;

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

    [Header("LogMessage")]
    [SerializeField] LogMessageSpawn logMessageSpawn;

    private Rigidbody solderRb;

    private void Start()
    {
        solderRb= GetComponent<Rigidbody>();
        mixedColor = startColor;
        progressBarRect = progressBarSolder.GetComponent<RectTransform>();
        badHoldDuration = holdDuration;
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
            BadHoldSolder();
            UnsolderingProgress();
        }
        else
        {
            logMessageSpawn.GetTextMessageInLog(true, "������� �� ��������.");
            Debug.Log("������� �� ��������");
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

    public void ResetBadProgress()
    {
        badProgress.fillAmount = 0f;

    }

    private void IronTinning()
    {
        if(isIronTinnig)
        {
            Debug.Log("�������");
            if(progressTinningDisable==false)
            {
                ironTinningProgress.SetActive(true);
            }
            else
            {
                ironTinningProgress.SetActive(false);
            }
            ironTinningTimer += Time.deltaTime;
            mixedColor = Color.Lerp(mixedColor, endColor, ironTinningTimer);
            rosinMeshRenderer.material.color = mixedColor;
            tinnignFill.fillAmount = ironTinningTimer / ironTinningDuration;
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
                progressTinningDisable = true;
                Debug.Log("�������");
            }
        }
    }
    private void Rosining()
    {
        if(isRosinCheck)
        {
            Debug.Log("��������");
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
        if(thereIsSolder && isSolderingPoint) 
        {
            Debug.Log("�����");
            float currentProgress = radioelementSlot.GetComponent<LegsSolderingProgress>().SolderingLegsElement();
            holdTimer += Time.deltaTime;

            ProgressBarCorrectUIPosition();
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
                isBadReady = true;
                progressFill.fillAmount = 0;
                progressSolderOnTipDisable = false;
            }

        }
       
    }
    private void BadHoldSolder()
    {
        if(isReady && isSolderingPoint && isBadReady)
        {
            if(holdTimer>=holdDuration)
            {
                badTimer = radioelementSlot.GetComponent<LegsSolderingProgress>().ReturnBadTimer();
                //badTimer += Time.deltaTime;
                badProgress.fillAmount = radioelementSlot.GetComponent<LegsSolderingProgress>().BadSolderingLegsElement();
                //badProgress.fillAmount = badTimer / badHoldDuration;

                if (badTimer >= badHoldDuration)
                {
                    logMessageSpawn.GetTextMessageInLog(false, "����� ���������� ��������");
                    Debug.Log("��������");
                    isBadReady= false;
                }
            }
        }
        else
        {
            badTimer = 0;
            //badProgress.fillAmount = 0;
        }
    }
    private void TakingSolder()
    {
        if(isReady && isIronTin && isRosin && isIronTinnig)
        {
            if(progressSolderOnTipDisable ==false)
            {
                takingSolderProgress.SetActive(true);
            }
            else
            {
                takingSolderProgress.SetActive(false);
            }
            Debug.Log("����� ������");
            Vector3 currentScale = solderOnIronTip.localScale;
            timer += Time.deltaTime;
            Vector3 newScale = Vector3.Lerp(currentScale, Vector3.one, timer);
            solderOnIronTip.localScale = newScale;

            progressFill.fillAmount = timer / duration;
            if(timer>=duration)
            {
                Debug.Log("������ �� ���������");
                progressSolderOnTipDisable= true;
                thereIsSolder = true;
                holdTimer = 0;
                badTimer = 0;
                badProgress.fillAmount = 0;
                
            }
        }
    }
    public void StartSolderingDetectionSlot(bool isActive)
    {
        solderSlotsDetect.DetecActive(isActive);
    }
    public void ActiveOrtoBt(bool isGo)
    {
        ortoViewBt.SetActive(isGo);
    }


    private void UnsolderingProgress()
    {
        if(radioelementSlot != null && isReady && isSolderingPoint)
        {
            if (radioelementSlot.GetComponent<LegsSolderingProgress>())
            {
                LegsSolderingProgress legs = radioelementSlot.GetComponent<LegsSolderingProgress>();
                if (legs.GetStatusLegs() && legs.GetFluxingLeg())
                {


                    badProgress.fillAmount = 0;


                    ProgressBarCorrectUIPosition();
                    progressBarSolder.SetActive(true);

                    goodProgress.fillAmount = legs.UnsolderingLegs();
                }
            }
            
        }
    }

    private void ProgressBarCorrectUIPosition()
    {
        Vector3 desiredPosition = solderOnIronTip.position;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(desiredPosition);
        screenPosition.x = Mathf.Clamp(screenPosition.x, 0, Screen.width);
        screenPosition.y = Mathf.Clamp(screenPosition.y, 0, Screen.height);
        progressBarRect.position = screenPosition;
    }
}
