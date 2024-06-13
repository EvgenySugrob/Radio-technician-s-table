using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolderGetParent : MonoBehaviour
{
    [SerializeField] GameObject parentSolderPart;

    public GameObject GetParentSolder()
    { return parentSolderPart; }
}
