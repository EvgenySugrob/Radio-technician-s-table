using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBoardInBath : MonoBehaviour
{
    [SerializeField] PopupMenuObjectType popupMenuObjectType;
    [SerializeField] PopupMenuCustom popupMenuCustom;
    [SerializeField] Transform pivotFreeBoard;
    [SerializeField] Transform board;
    [SerializeField] UltrasonicBathBoardDetection ultrasonicBathBoardDetection;
    [SerializeField] DragAndDrop dragAndDrop;
    [SerializeField] QuickOutlineController quickOutlineController;

    public void OpenMenu()
    {
        popupMenuObjectType.Show();
    }

    public void RemoveBoardBath()
    {
        popupMenuCustom.ClosePopupMenu();

        board.position = pivotFreeBoard.position;
        board.rotation = pivotFreeBoard.rotation;
        board.GetComponent<BoxCollider>().enabled = true;

        ultrasonicBathBoardDetection.DisableTriggerZone(true);
        quickOutlineController.DisableOutline();

        StartCoroutine(WaitingBeforePlacingInHand());
    }

    IEnumerator WaitingBeforePlacingInHand()
    {
        yield return new WaitForSeconds(1f);
        dragAndDrop.SetDraggedObject(board.gameObject);
    }
}
