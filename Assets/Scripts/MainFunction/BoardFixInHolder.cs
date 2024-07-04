using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardFixInHolder : MonoBehaviour
{
    [SerializeField] GameObject lockBoardBt;

    [SerializeField] BoxCollider triggerZone;
    [SerializeField] BoxCollider boardHolderCollider;
    [SerializeField] bool boardIsLock;

    [Header("removeBoard")]
    [SerializeField] Transform removePoint;
    [SerializeField] Transform board;
    [SerializeField] PopupMenuCustom popupMenuCustom;
    [SerializeField] GameObject removeBoardBt;
    [SerializeField] DragAndDrop dragAndDrop;

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

    public void RemoveBoard()
    {
        popupMenuCustom.ClosePopupMenu();
        removeBoardBt.SetActive(false);
        dragAndDrop.ClearHand();

        board.parent = null;
        board.position = removePoint.position;
        board.rotation = removePoint.rotation;
        board.GetComponent<BoxCollider>().enabled = true;

        DisableTriggerZone(true);

        StartCoroutine(WaitingBeforePlacingInHand());
    }

    IEnumerator WaitingBeforePlacingInHand()
    {
        yield return new WaitForSeconds(1f);
        dragAndDrop.SetDraggedObject(board.gameObject);
    }
}
