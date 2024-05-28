using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolderStartPosition : MonoBehaviour
{
    [SerializeField] Transform startPosition;

    private void Start()
    {
        transform.position = startPosition.position;
        transform.rotation = startPosition.rotation;
    }
}
