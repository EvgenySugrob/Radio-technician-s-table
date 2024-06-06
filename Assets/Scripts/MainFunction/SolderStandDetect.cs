using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolderStandDetect : MonoBehaviour
{
    [SerializeField]private bool startDetection;
    [SerializeField] private BoxCollider standCollider;

    [Header("PopupMenuUI")]
    [SerializeField] GameObject returnToStandBt;
    private BoxCollider detectionSolderZone;

    private void Start()
    {
        detectionSolderZone = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (startDetection && other.GetComponent<SolderInteract>())
        {
            Debug.Log("Solder Enter");
            returnToStandBt.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (startDetection && other.GetComponent<SolderInteract>())
        {
            Debug.Log("Solder Stay");
            returnToStandBt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (startDetection && other.GetComponent<SolderInteract>())
        {
            Debug.Log("Solder Exit");
            returnToStandBt.SetActive(false);
        }
    }

    public void EnableDisableZone(bool isOn)
    {
        startDetection = isOn;
        if (isOn == false)
        {
            returnToStandBt.SetActive(false);
        }
        else
        {
            standCollider.enabled = false;
            StartCoroutine(WaitStandColliderEnableAgain());
        }
    }
    IEnumerator WaitStandColliderEnableAgain()
    {
        yield return new WaitForSeconds(0.5f);
        standCollider.enabled = true;
    }
}
