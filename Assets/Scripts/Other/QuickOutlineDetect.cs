using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuickOutlineDetect : MonoBehaviour
{
    [Header("OutlineController")]
    [SerializeField] QuickOutlineController currentOutlineController;
    [SerializeField] QuickOutlineController prevOutlineController;
    [SerializeField] GameObject currentObject;

    [Header("Detect Setting")]
    [SerializeField] bool isDetectionEnable = true; 
    [SerializeField] float distanceRay=10f;
    [SerializeField] LayerMask layerMask;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if(isDetectionEnable)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, distanceRay, layerMask))
            {
                currentObject = hit.collider.gameObject;

                if (currentObject.GetComponent<QuickOutlineController>())
                {
                    currentOutlineController = currentObject.GetComponent<QuickOutlineController>();

                    if(prevOutlineController == null)
                    {
                        prevOutlineController = currentOutlineController;
                    }
                    
                    if(prevOutlineController != currentOutlineController)
                    {
                        prevOutlineController.DisableOutline();
                        prevOutlineController = currentOutlineController;
                    }
                }

                if(currentOutlineController != null && currentObject.GetComponent<QuickOutlineController>())
                {
                    currentOutlineController.EnableOutline();
                }
                else if(currentOutlineController != null && currentObject.GetComponent<QuickOutlineController>() == false)
                {
                    currentOutlineController.DisableOutline();
                }
            }
            else
            {
                if(currentOutlineController!=null)
                {
                    currentOutlineController.DisableOutline();
                    currentOutlineController = null;
                    currentObject= null;
                }
            }
        }
    }

    public void DetectionEnable()
    {
        isDetectionEnable = true;
    }
    public void DetectionDisable()
    {
        isDetectionEnable = false;

        if (currentOutlineController!=null)
        {
            currentOutlineController.DisableOutline();
        }
    }
}
