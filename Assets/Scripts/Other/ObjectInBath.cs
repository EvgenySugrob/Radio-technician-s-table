using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInBath : MonoBehaviour
{
    [SerializeField] Transform bathHolderPoint;
    [SerializeField] UltrasonicBathBoardDetection bathBoardFixInHolder;

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

        transform.position = bathHolderPoint.position;
        transform.rotation = bathHolderPoint.rotation;
        transform.parent = bathHolderPoint.parent;

        bathBoardFixInHolder.DisableTriggerZone(false);
    }
}
