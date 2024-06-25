using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioComponentChoice : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] GameObject radioComponentPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] List<Transform> spawnPoints;

    [Header("UI window")]
    [SerializeField] GameObject choiceWindow;

    private void Awake()
    {
        Transform parent = spawnPoint.parent;
        for (int i = 0; i < parent.childCount; i++)
        {
            spawnPoints.Add(parent.GetChild(i));
        }
    }

    public void SpawnObject()
    {
        int idPoint = Random.Range(0, spawnPoints.Count);
        Instantiate(radioComponentPrefab, spawnPoints[idPoint].position, spawnPoints[idPoint].rotation);
        choiceWindow.SetActive(false);
    }
}
