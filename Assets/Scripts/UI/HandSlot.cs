using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSlot : MonoBehaviour
{
    [Header("StatusAndObhect")]
    [SerializeField] private GameObject objectInSlot;
    [SerializeField] bool slotIsFree = true;

    [Header("PivotAndSlotUI")]
    [SerializeField] GameObject uiSlot;
    [SerializeField] Transform objectPivotRotationInUI;

    private GameObject copyOriginObj;

    public void SetObject(GameObject gameObject)
    {
        objectInSlot = gameObject;
        slotIsFree = false;
        SpawnCopyObjInUI();
        objectInSlot.SetActive(false);
    }

    public void GetObject()
    {
        slotIsFree= true;
        DeletCopyObjInUI();
    }

    public bool IsSlotFree()
    {
        return slotIsFree;
    }

    private void SpawnCopyObjInUI()
    {
        GameObject obj = Instantiate(objectInSlot,objectPivotRotationInUI.position,objectPivotRotationInUI.rotation,objectPivotRotationInUI);
        copyOriginObj = obj;
        uiSlot.SetActive(true);
    }
    private void DeletCopyObjInUI()
    {
        objectInSlot.SetActive(true);
        Destroy(copyOriginObj);
        StartCoroutine(WaitCloseUISlot());
    }
    IEnumerator WaitCloseUISlot()
    {
        yield return new WaitForSeconds(0.5f);
        uiSlot.SetActive(false);
    }
}
