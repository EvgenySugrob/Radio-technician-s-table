using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationItem : MonoBehaviour,IRotation
{
    public bool isRotation { get; set; }
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void onStartRotation()
    {
        rb.isKinematic = true;
    }

    public void onStopRotation()
    {
       
    }
}
