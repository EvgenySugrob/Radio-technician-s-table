using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TestDragItem : MonoBehaviour, IDrag
{
    private Rigidbody rb;
    public bool isFreeze { get; set; }

    public bool isMovebale { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void onEndDrag()
    {

        rb.useGravity = true;
        rb.velocity = Vector3.zero;

    }

    public void onStartDrag()
    {
        if (!isFreeze)
        {
            rb.isKinematic = false;
        }
        rb.useGravity = false;
    }

    public void onFreeze(bool isFrezeState)
    {
        isFreeze= isFrezeState;
        rb.isKinematic= isFreeze;
    }
}
