using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TestDragItem : MonoBehaviour, IDrag
{
    private Rigidbody rb;

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
        rb.useGravity = false;
    }
}
