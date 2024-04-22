using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioComponentChoice : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] GameObject radioComponentPrefab;
    [SerializeField] Transform spawnPoint;

    [Header("UI window")]
    [SerializeField] GameObject choiceWindow;

    public void SpawnObject()
    {
        Instantiate(radioComponentPrefab, spawnPoint.position, spawnPoint.rotation);
        choiceWindow.SetActive(false);
    }
}
