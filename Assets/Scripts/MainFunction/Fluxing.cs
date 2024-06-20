using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fluxing : MonoBehaviour
{
    [SerializeField] CottonSwabControl cottonSwabControl;
    [SerializeField] GameObject fakeSwab;
    [SerializeField] Animator animator;

    [Header("shutdown during animation")]
    [SerializeField] BoxCollider capCollider;
    [SerializeField] DragAndDrop dragAndDrop;
    [SerializeField] DragAndRotation dragAndRotation;
    [SerializeField] PlayerController playerController;

    private bool isDragActive;
    public void StartFlux(CottonSwabControl swab)
    {
        DisableInputs();

        cottonSwabControl = swab;
        cottonSwabControl.gameObject.SetActive(false);
        capCollider.enabled = false;
        
        fakeSwab.SetActive(true);
        animator.SetBool("isPlayAnimation",true);

        StartCoroutine(WaitStopAnimation());
    }

    private void DisableInputs()
    {
        if (dragAndDrop.enabled)
        {
            isDragActive = true;
        }
        else
        {
            isDragActive= false;
        }
        dragAndDrop.enabled = false;
        dragAndRotation.enabled = false;
        playerController.enabled = false;
    }
    private void ActiveInputs()
    {
        if (isDragActive == true)
        {
            dragAndDrop.enabled = true;
        }
        else
        {
            dragAndRotation.enabled = true;
        }
        playerController.enabled = true;
    }

    IEnumerator WaitStopAnimation()
    {
        yield return new WaitForSeconds(3.5f);
        animator.SetBool("isPlayAnimation", false);

        fakeSwab.SetActive(false);
        cottonSwabControl.gameObject.SetActive(true);
        cottonSwabControl.EndFluxing();
        capCollider.enabled = true;

        ActiveInputs();
    }
}
