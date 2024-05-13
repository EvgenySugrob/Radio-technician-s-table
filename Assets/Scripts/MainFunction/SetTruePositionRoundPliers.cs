using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTruePositionRoundPliers : MonoBehaviour
{
    [SerializeField] bool isLeftSide;
    [SerializeField] Vector3 angleRotation;

    private Vector3 anglePliers = new Vector3(90,-90,0);

    [SerializeField] CapsuleCollider capsuleCollider;
    [SerializeField] BoxCollider boxCollider;
    [SerializeField] MeshCollider meshCollider;
    [SerializeField] BoxCollider modelingBoxCollider;

    private void Start()
    {
        capsuleCollider = transform.parent.GetComponent<CapsuleCollider>();
        meshCollider= transform.parent.GetComponent<MeshCollider>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public bool LeftSideCheck()
    {
        return isLeftSide;
    }

    public Vector3 GetAngleRotation()
    {
        return angleRotation;
    }

    public Vector3 GetPliersAngle()
    {
        return anglePliers;
    }

    public void AfterModeling()
    {
        capsuleCollider.enabled = false;
        boxCollider.enabled = false;
        //meshCollider.enabled = true;
    }
}
