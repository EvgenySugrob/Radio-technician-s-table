using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DragItem : MonoBehaviour, IDrag
{
    [SerializeField] bool permanentKinematic;
    [SerializeField] bool unfreezeRotation;
    [SerializeField] float freezeAgainTimer = 2.5f;
    private Rigidbody rb;
    private Coroutine coroutine;
    public bool isFreeze { get; set; }

    public bool isMovebale { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void onEndDrag()
    {
        if(permanentKinematic)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
        else
        {
            rb.useGravity = true;
            if(unfreezeRotation)
            {
                rb.freezeRotation= false;
                coroutine = StartCoroutine(WaitFreezeAgain());
            }
        }

        rb.velocity = Vector3.zero;
    }
    IEnumerator WaitFreezeAgain()
    {
        yield return new WaitForSeconds(freezeAgainTimer);
        rb.freezeRotation = true;
        coroutine = null;
    }
    public void onStartDrag()
    {
        if(coroutine!= null)
        {
            StopCoroutine(coroutine);
            rb.freezeRotation = true;
        }
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
