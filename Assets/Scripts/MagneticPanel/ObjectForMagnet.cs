using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectForMagnet : MonoBehaviour, IMagnet
{
    [SerializeField] MainMagnet mainMagnet;
    [SerializeField] Rigidbody rb;

    private Quaternion startRotation;
    public bool onMagnet { get; set; }

    private void Awake()
    {
        mainMagnet = FindObjectOfType<MainMagnet>();
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        startRotation = transform.rotation;
    }

    public void Magnet()
    {
        rb.isKinematic= true;
        transform.rotation = startRotation;
    }

    public void Take()
    {
        rb.isKinematic= false;
    }
}
