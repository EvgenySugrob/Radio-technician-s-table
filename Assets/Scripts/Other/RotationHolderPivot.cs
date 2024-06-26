using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHolderPivot : MonoBehaviour
{
    [SerializeField] BoardFixInHolder boardFixInHolder;
    [SerializeField] GameObject rotationHolderPivotBt;
    [SerializeField] bool isChangeRotation;

    [Header("Pivots")]
    [SerializeField] Transform changePivot;
    [SerializeField] Transform firstPivot;
    [SerializeField] Transform secondPivot;

    Quaternion startPivotRotation;
    Quaternion changePivotRotation;

    private void Start()
    {
        startPivotRotation = firstPivot.rotation;
        changePivotRotation = changePivot.rotation;
    }

    private void Update()
    {
        rotationHolderPivotBt.SetActive(boardFixInHolder.IsBoardLock());
    }

    public void ChangeSideBoard()
    {
        if (isChangeRotation)
        {
            firstPivot.rotation = startPivotRotation;
            secondPivot.rotation = startPivotRotation;
            isChangeRotation= false;
        }
        else
        {
            firstPivot.rotation = changePivotRotation;
            secondPivot.rotation = changePivotRotation;
            isChangeRotation= true;
        }
    }
    public bool IsChangeRotation()
    { return isChangeRotation; }
}
