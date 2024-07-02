using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CottonSwabSpawn : MonoBehaviour
{
    [Header("DragAndDrop and Player")]
    [SerializeField] DragAndDrop dragAndDrop;
    [SerializeField] Transform spawnPoint;
    [SerializeField] PressButtonRotationModeSwap pressButtonRotationModeSwap;
    private float distanceToPlayer = 0.28f;

    [Header("SpawnSwab")]
    [SerializeField] GameObject swabPrefab;
    private GameObject currentSwab;

    private void Awake()
    {
        spawnPoint = transform.GetChild(0).transform;
    }

    public void SpawnCottonSwab()
    {
        if (currentSwab == null)
        {
            GameObject swab = Instantiate(swabPrefab, spawnPoint.position, swabPrefab.transform.rotation);

            currentSwab = swab;
            currentSwab.GetComponent<CottonSwabControl>().SpawnEnableTriggerZone();
            dragAndDrop.SetDraggedObject(swab);
            dragAndDrop.SetCustomCurrentDistanceToObject(distanceToPlayer);
            pressButtonRotationModeSwap.SetDraggedObject(swab);
        }
        else
        {
            Debug.Log("Палочка уже есть");
        }
    }

    public void RemoveSpawnSwab()
    {
        dragAndDrop.ClearHand();
        Destroy(currentSwab);
        currentSwab = null;
    }

    public GameObject ReturnCurrenSwab()
    {
        return currentSwab;
    }
    public void StartFluxing()
    {
        currentSwab.GetComponent<CottonSwabControl>().StartFluxing();
    }
}
