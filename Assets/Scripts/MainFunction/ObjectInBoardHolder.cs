using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInBoardHolder : MonoBehaviour
{
    [SerializeField] Transform holderPoint;
    [SerializeField] BoardFixInHolder boardFixInHolder;

    [SerializeField] DragAndDrop dragAndDrop;
    private Rigidbody rb;
    private BoxCollider boxCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void ObjcetInHolder()
    {
        dragAndDrop.ClearHand();

        rb.isKinematic = true;
        boxCollider.enabled = false;

        transform.position = holderPoint.position;
        transform.rotation = holderPoint.rotation;
        transform.parent = holderPoint.parent;

        boardFixInHolder.DisableTriggerZone(false);
    }
}
