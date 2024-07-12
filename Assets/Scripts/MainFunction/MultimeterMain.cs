using cakeslice;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultimeterMain : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] DragAndDrop dragAndDrop;
    [SerializeField] DragAndRotation dragAndRotation;
    [SerializeField] PlayerController playerController;
    private bool isRotationEnable;

    [Header("MultimeterSetType")]
    [SerializeField] Transform diskType;
    [SerializeField] Outline diskOutline;
    [SerializeField] List<Vector3> eulerVectors;
    [SerializeField] int currentType = 0;
    int step = 1;
    private bool isTypeSelect;
    [SerializeField] bool multimeterIsEnabled;

    [Header("MultimeterUI")]
    [SerializeField] PopupMenuCustom popupMenuCustom;
    [SerializeField] TMP_Text buttonTextChange;
    [SerializeField] GameObject multimeterUI;
    [SerializeField] TMP_Text displayText;
    [SerializeField] GameObject ortoViewBt;
    [SerializeField] GameObject setTypeBt;
    [SerializeField] GameObject rotationBt;

    [SerializeField] Transform corpusOffset;
    private float offset = 0.085f;

    [Header("DetectionSlot")]
    [SerializeField] Transform raycastPoint;
    [SerializeField] float disanceRay;
    [SerializeField] LayerMask layerMask;
    [SerializeField] bool isdetectionActive;
    [SerializeField] GameObject checkRadioElementJobBt;
    [SerializeField] GameObject goodImage;
    [SerializeField] GameObject badImage;
    [SerializeField] SlotInfo slotInfo;
    [SerializeField] Transform leftDipstick;
    [SerializeField] Transform rightDipstick;
    [SerializeField] List<CapsuleCollider> dipstickColliders;
    [SerializeField] List<GameObject> longRopeList;
    private Vector3 startLeftDipstickPosition;
    private Vector3 startRightDipstickPosition;
    private Quaternion startLeftDipstickRotation;
    private Quaternion startRightDipstickRotation;
    private string defText;

    private void Start()
    {
        startLeftDipstickPosition = leftDipstick.localPosition;
        startRightDipstickPosition = rightDipstick.localPosition;
        defText = displayText.text;
    }

    public bool IsTypeSelect()
    {
        return isTypeSelect;
    }

    public void SelectTypeWork()
    {
        if (isTypeSelect)
        {
            buttonTextChange.text = "Режим работы";
            playerController.enabled = true;

            diskOutline.enabled = false;
            isTypeSelect= false;
        }
        else
        {
            transform.LookAt(playerController.transform.position);

            playerController.enabled = false;
            diskOutline.enabled = true;
            isTypeSelect = true;
            buttonTextChange.text = "Применить режим";
        }
        popupMenuCustom.ClosePopupMenu();
    }

    private void Update()
    {
        if(isTypeSelect)
        {
            if(Input.mouseScrollDelta.y > 0)
            {
                currentType += step;
            }
            if(Input.mouseScrollDelta.y < 0)
            {
                currentType-= step;
            }
            SetTypeMultimeter();
        }

        if(isdetectionActive)
        {
            DetectionSlotToCheckJobRadioelement();
        }
    }
    private void DetectionSlotToCheckJobRadioelement()
    {
        RaycastHit hit;

        if(Physics.Raycast(raycastPoint.position, raycastPoint.TransformDirection(Vector3.forward), out hit, disanceRay, layerMask))
        {
            if(hit.collider.GetComponent<SlotInfo>())
            {
                slotInfo = hit.collider.GetComponent<SlotInfo>();
                checkRadioElementJobBt.SetActive(true);
            }
            else
            {
                slotInfo = null;
                checkRadioElementJobBt.SetActive(false);
            }
        }
        else
        {
            slotInfo = null;
            checkRadioElementJobBt.SetActive(false);
        }
    }
    public void CheckComponent()
    {
        if(slotInfo!=null)
        {
            //slotInfo.ReturnStateRadioComponent(); //Проверка компонента на работоспособность. На будущее

            dragAndDrop.SetIsDragState(false);
            foreach(CapsuleCollider capsule in dipstickColliders)
            {
                capsule.enabled = false;
            }
            foreach (GameObject item in longRopeList)
            {
                item.SetActive(true);
            }
            PrefabRisistNominalSetting prefabSetting = slotInfo.ReturnRadioelementInSlot().GetComponent<PrefabRisistNominalSetting>();
            leftDipstick.position = prefabSetting.ReturnLeftPoint().position;
            leftDipstick.rotation = prefabSetting.ReturnLeftPoint().rotation;

            rightDipstick.position = prefabSetting.ReturnRightPoint().position;
            rightDipstick.rotation = prefabSetting.ReturnRightPoint().rotation;

            if(prefabSetting.IsCapasitor())
            {
                displayText.text = "";
            }
            else if(prefabSetting.IsNotSetNominal())
            {
                displayText.text = prefabSetting.resistNominal.ToString();
            }
            else if(prefabSetting.IsCapasitor()==false && prefabSetting.IsNotSetNominal()==false)
            {
                Debug.Log(prefabSetting.nominalText);
                displayText.text = prefabSetting.nominalText;
            }

            StartCoroutine(DisplayCheck());
        }
    }

    IEnumerator DisplayCheck()
    {
        if (slotInfo.ReturnStateRadioComponent())
        {
            goodImage.SetActive(true);
        }
        else
        {
            badImage.SetActive(true);
        }
        
        yield return new WaitForSeconds(1.5f);

        goodImage.SetActive(false);
        badImage.SetActive(false);
        displayText.text = defText;

        leftDipstick.localPosition = startLeftDipstickPosition;
        leftDipstick.rotation = startLeftDipstickRotation;
        rightDipstick.localPosition = startRightDipstickPosition;
        rightDipstick.rotation = startRightDipstickRotation;

        foreach (GameObject item in longRopeList)
        {
            item.SetActive(false);
        }
        foreach (CapsuleCollider collider in dipstickColliders)
        {
            collider.enabled = true;
        }
        dragAndDrop.SetIsDragState(true);
    }
    private void SetTypeMultimeter()
    {
        if (currentType<0)
        {
            currentType = eulerVectors.Count - 1;   
        }
        else if(currentType>eulerVectors.Count-1)
        {
            currentType = 0;
        }

        switch (currentType) //расширять по мере добавления позиций
        {
            case 0:
                diskType.localEulerAngles = eulerVectors[0];
                multimeterIsEnabled = false;
                multimeterUI.SetActive(false);
                break;

            case 1:
                diskType.localEulerAngles = eulerVectors[1];
                multimeterIsEnabled = true;
                multimeterUI.SetActive(true);
                break;
        }
    }

    public void ActiveOrtoBt(bool isGo)
    {
        ortoViewBt.SetActive(isGo);
    }
    public void OrtoViewButtonsDisable(bool isActive)
    {
        if(dragAndRotation.enabled)
        {
            popupMenuCustom.RotationSwap();
        }

        rotationBt.SetActive(!isActive);
        setTypeBt.SetActive(!isActive);
        transform.GetComponent<Rigidbody>().freezeRotation= isActive;

        if (isActive)
        {
            isdetectionActive = true;
            transform.localEulerAngles = new Vector3(-90, 180, 0);
            corpusOffset.localPosition = new Vector3(0, -offset, 0);
        }
        else
        {
            isdetectionActive = false;
            corpusOffset.localPosition = Vector3.zero;
        }
    }
}
