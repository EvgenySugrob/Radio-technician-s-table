using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardFixInHolder : MonoBehaviour
{
    [SerializeField] GameObject lockBoardBt;

    [SerializeField] BoxCollider triggerZone;
    [SerializeField] BoxCollider boardHolderCollider;
    [SerializeField] bool boardIsLock;

    private void Start()
    {
        triggerZone = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ForHolder")
        {
            lockBoardBt.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "ForHolder")
        {
            lockBoardBt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ForHolder")
        {
            lockBoardBt.SetActive(false);
        }
    }

    public void DisableTriggerZone(bool isOn)
    {
        triggerZone.enabled = isOn;
        boardHolderCollider.enabled = !isOn;
        boardIsLock = !isOn;
    }

    public bool IsBoardLock()
    {
        return boardIsLock;
    }
}
