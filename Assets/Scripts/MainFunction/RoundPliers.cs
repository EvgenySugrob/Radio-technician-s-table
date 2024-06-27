using Deform;
using Obi;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class RoundPliers : MonoBehaviour
{
    [Header("Main function")]
    [SerializeField] public GameObject currentHitObject;
    [SerializeField] private bool isTruePosition;
    [SerializeField] GameObject modelingActiveButton;
    [SerializeField] GameObject sliderBend;
    [SerializeField] GameObject buttonBack;
    

    [Header("DragDrop/Rotation")]
    [SerializeField] DragAndDrop dragAndDrop;
    [SerializeField] DragAndRotation dragAndRotation;

    [Header("Left/Right part")]
    [SerializeField] Transform leftPart;
    [SerializeField] Transform rightPart;
    [SerializeField] float leftPartAngle;
    [SerializeField] float rightPartAngle;

    [SerializeField] float speedToLerp;

    private BendDeformer bendDeformer;
    private Vector3 leftPartStartRotation;
    private Vector3 rightPartStartRotation;

    private Quaternion startRotationPliersAngle;
    private Quaternion pliersStartConnectAngle;
    private Quaternion correctPliersAngleRotation;
    private bool isLeftSide;
    private bool isModelling;
    float currentValue = 0; //начальное значение слайдера
    private Slider modelingSlider;
    private SetTruePositionRoundPliers setTruePositionRoundPliers;

    float vectorDifferenceX;

    private void Start()
    {
        leftPart = transform.GetChild(0).transform;
        rightPart = transform.GetChild(1).transform;

        leftPartStartRotation = leftPart.localEulerAngles;
        rightPartStartRotation = rightPart.localEulerAngles;
        modelingSlider = sliderBend.GetComponent<Slider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Deformable>() || other.GetComponent<SetTruePositionRoundPliers>())
        {
            modelingActiveButton.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Deformable>())
        {
            Debug.Log("Exit " + other.name);
            modelingActiveButton.SetActive(false);

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<SetTruePositionRoundPliers>() && other.tag == "Leg")
        {
            Debug.Log("Leg" + other.name);
            isTruePosition = true;
            modelingActiveButton.SetActive(true);
            currentHitObject = other.gameObject;
        }
        else if(other.GetComponent<SetTruePositionRoundPliers>() == false)
        {
            isTruePosition = false;
            currentHitObject= null;
        }
        else
        {
            isTruePosition = false;
        }
    }

    public void StartModelingRadioLegs()
    {
        if (isTruePosition)
        {
            FreezeObjectModelingLegs();
            transform.position = currentHitObject.transform.GetChild(0).position;
        }
        else
        {
            Debug.Log("Неверное место формовки");
        }
    }

    private void FreezeObjectModelingLegs()
    {
        transform.TryGetComponent<IDrag>(out var drag);
        setTruePositionRoundPliers = currentHitObject.GetComponent<SetTruePositionRoundPliers>();


        if (!drag.isFreeze)
        {
            isModelling = true;
            isLeftSide = setTruePositionRoundPliers.LeftSideCheck();
            correctPliersAngleRotation = Quaternion.Euler(setTruePositionRoundPliers.GetAngleRotation());

            bendDeformer = currentHitObject.GetComponent<BendDeformer>();
            drag.onFreeze(true);
            dragAndDrop.ClearHand();
            CorrectDisplaySliderBar();
            sliderBend.SetActive(true);
            pliersStartConnectAngle = transform.rotation;

            SetPliersConnectAngle();
            PliersPartCorrectAngle(true);

        }
    }
    private void UnfreezeObjectModelingLegs()
    {
        transform.TryGetComponent<IDrag>(out var drag);
        bendDeformer = null;
        drag.onFreeze(false);
        dragAndDrop.SetDraggedObject(transform.gameObject);
        sliderBend.SetActive(false);
        isModelling = false;
        currentValue = 0;
        modelingSlider.value = currentValue;
        setTruePositionRoundPliers.AfterModeling();
        modelingActiveButton.SetActive(false);
        PliersPartCorrectAngle(false);
    }
    private void CorrectDisplaySliderBar()
    {
        if(isLeftSide)
        {
            sliderBend.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            sliderBend.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 180);
        }
        
    }
    private void SetPliersConnectAngle()
    {
        Vector3 plierAngle = currentHitObject.GetComponent<SetTruePositionRoundPliers>().GetPliersAngle();
        transform.eulerAngles = plierAngle;
        startRotationPliersAngle = Quaternion.Euler(plierAngle);
    }

    private void PliersPartCorrectAngle(bool isActive)
    {
        if (isActive)
        {
            leftPart.localEulerAngles = new Vector3(0, leftPartAngle, 0);
            rightPart.localEulerAngles = new Vector3(0, rightPartAngle, 0);
        }
        else 
        { 
            leftPart.localEulerAngles = leftPartStartRotation;
            rightPart.localEulerAngles = rightPartStartRotation;
        }
        
    }

    public void AngleModelingSet(float value)
    {
        if (isModelling)
        {
            if (currentValue < value)
            {
                bendDeformer.Angle = 90f * value;
                transform.rotation = Quaternion.Lerp(transform.rotation, correctPliersAngleRotation, speedToLerp * value);
                currentValue = value;
            }
            else
            {
                modelingSlider.value = currentValue;
            }

            if (modelingSlider.value == modelingSlider.maxValue)
            {
                UnfreezeObjectModelingLegs();
            }
        } 
    }
}
