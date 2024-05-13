using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabRisistNominalSetting : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;

    [Header("Colors for resist")]
    [SerializeField] Material silverMat;
    [SerializeField] Material goldMat;
    [SerializeField] Material blackMat;
    [SerializeField] Material brownMat;
    [SerializeField] Material redMat;
    [SerializeField] Material orangeMat;
    [SerializeField] Material yellowMat;
    [SerializeField] Material greenMat;
    [SerializeField] Material blueMat;
    [SerializeField] Material violetMat;
    [SerializeField] Material grayMat;
    [SerializeField] Material whiteMat;

    [Header("IndexParamRisist")]
    [SerializeField] int firstIndexColor;
    [SerializeField] int secondIndexColor;
    [SerializeField] int thirdIndexColor;
    [SerializeField] string multiplicatorIndexColor;
    [SerializeField] int admittanceIndexColor;

    [Header("ResistorMarking")]
    [SerializeField] SelectingResistorMarkings selectingResistorMarkings;

    [Header("Rigidbody and colliders")]
    [SerializeField] Rigidbody rb;
    [SerializeField] List<MeshCollider> meshColliders;
    [SerializeField] List<BoxCollider> boxColliders;
    [SerializeField] List<CapsuleCollider> capsuleColliders; 
    [SerializeField] float connectionAngle;
    [SerializeField] bool forLittleTweezers;

    [SerializeField] bool modelingElement;

    private PrefabRisistNominalSetting prefabRisistNominal;
    private Quaternion startRotation;
    

    public double resistNominal { get; set; }

    

    private void Start()
    {
        startRotation = transform.localRotation;
        prefabRisistNominal = GetComponent<PrefabRisistNominalSetting>();
        rb= GetComponent<Rigidbody>();
    }

    public void SetFirstColor(int value)
    {
        Debug.Log(meshRenderer.materials[2].name);
        switch (value)
        {
            case 0:
                meshRenderer.materials[2].color = blackMat.color;
                break;
            case 1:
                meshRenderer.materials[2].color = brownMat.color;
                break;
            case 2:
                meshRenderer.materials[2].color = redMat.color;
                break;
            case 3:
                meshRenderer.materials[2].color = orangeMat.color;
                break;
            case 4:
                meshRenderer.materials[2].color = yellowMat.color;
                break;
            case 5:
                meshRenderer.materials[2].color = greenMat.color;
                break;
            case 6:
                meshRenderer.materials[2].color = blueMat.color;
                break;
            case 7:
                meshRenderer.materials[2].color = violetMat.color;
                break;
            case 8:
                meshRenderer.materials[2].color = grayMat.color;
                break;
            case 9:
                meshRenderer.materials[2].color = whiteMat.color;
                break;
        }
    }

    public void SetSecondColor(int value)
    {
        Debug.Log(meshRenderer.materials[3].name);
        switch (value)
        {
            case 0:
                meshRenderer.materials[3].color = blackMat.color;
                break;
            case 1:
                meshRenderer.materials[3].color = brownMat.color;
                break;
            case 2:
                meshRenderer.materials[3].color = redMat.color;
                break;
            case 3:
                meshRenderer.materials[3].color = orangeMat.color;
                break;
            case 4:
                meshRenderer.materials[3].color = yellowMat.color;
                break;
            case 5:
                meshRenderer.materials[3].color = greenMat.color;
                break;
            case 6:
                meshRenderer.materials[3].color = blueMat.color;
                break;
            case 7:
                meshRenderer.materials[3].color = violetMat.color;
                break;
            case 8:
                meshRenderer.materials[3].color = grayMat.color;
                break;
            case 9:
                meshRenderer.materials[3].color = whiteMat.color;
                break;
        }
    }

    public void SetThirdColor(int value)
    {
        Debug.Log(meshRenderer.materials[4].name);
        switch (value)
        {
            case 0:
                meshRenderer.materials[4].color = blackMat.color;
                break;
            case 1:
                meshRenderer.materials[4].color = brownMat.color;
                break;
            case 2:
                meshRenderer.materials[4].color = redMat.color;
                break;
            case 3:
                meshRenderer.materials[4].color = orangeMat.color;
                break;
            case 4:
                meshRenderer.materials[4].color = yellowMat.color;
                break;
            case 5:
                meshRenderer.materials[4].color = greenMat.color;
                break;
            case 6:
                meshRenderer.materials[4].color = blueMat.color;
                break;
            case 7:
                meshRenderer.materials[4].color = violetMat.color;
                break;
            case 8:
                meshRenderer.materials[4].color = grayMat.color;
                break;
            case 9:
                meshRenderer.materials[4].color = whiteMat.color;
                break;
        }

    }

    public void SetDecimalColor(string value)
    {
        Debug.Log(meshRenderer.materials[5].name);
        switch (value)
        {
            case "0,01":
                meshRenderer.materials[5].color = silverMat.color;
                break;
            case "0,1":
                meshRenderer.materials[5].color = goldMat.color;
                break;
            case "1":
                meshRenderer.materials[5].color = blackMat.color;
                break;
            case "10":
                meshRenderer.materials[5].color = brownMat.color;
                break;
            case "100":
                meshRenderer.materials[5].color = redMat.color;
                break;
            case "1000":
                meshRenderer.materials[5].color = orangeMat.color;
                break;
            case "10000":
                meshRenderer.materials[5].color = yellowMat.color;
                break;
            case "100000":
                meshRenderer.materials[5].color = greenMat.color;
                break;
            case "1000000":
                meshRenderer.materials[5].color = blueMat.color;
                break;
            case "10000000":
                meshRenderer.materials[5].color = violetMat.color;
                break;
            case "100000000":
                meshRenderer.materials[5].color = grayMat.color;
                break;
            case "1000000000":
                meshRenderer.materials[5].color = whiteMat.color;
                break;
        }
    }

    public void SetAdmittanceColor(int value)
    {
        Debug.Log(meshRenderer.materials[1].name);
        switch (value)
        {
            case 0:
                meshRenderer.materials[1].color = silverMat.color;
                break;
            case 1:
                meshRenderer.materials[1].color = goldMat.color;
                break;
            case 2:
                meshRenderer.materials[1].color = brownMat.color;
                break;
            case 3:
                meshRenderer.materials[1].color = redMat.color;
                break;
            case 4:
                meshRenderer.materials[1].color = greenMat.color;
                break;
            case 5:
                meshRenderer.materials[1].color = blueMat.color;
                break;
            case 6:
                meshRenderer.materials[1].color = violetMat.color;
                break;
            case 7:
                meshRenderer.materials[1].color = grayMat.color;
                break;
        }
    }

    public void GetParamToMarkingWindow()
    {
        selectingResistorMarkings.SetPrefabAndStartSetting(firstIndexColor,secondIndexColor,thirdIndexColor,multiplicatorIndexColor,admittanceIndexColor, prefabRisistNominal);
    }

    public void RigidbodyKinematic(bool isActive)
    {
        rb.isKinematic = isActive;
        foreach (MeshCollider collider in meshColliders)
        {
            collider.enabled = !isActive;
        }
        foreach(BoxCollider collider in boxColliders)
        {
            collider.enabled = !isActive;
        }
        foreach(CapsuleCollider collider in capsuleColliders)
        {
            collider.enabled = !isActive;
        }
    }

    public float GetConectionAngle()
    {
        return connectionAngle;
    }
    public bool GetTypeTweezers()
    {
        return forLittleTweezers;
    }

    public void SetDefaultRotation()
    {
        transform.localRotation = startRotation;
    }
}
