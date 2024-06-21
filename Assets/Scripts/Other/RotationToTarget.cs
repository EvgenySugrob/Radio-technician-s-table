using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationToTarget : MonoBehaviour
{
    [SerializeField] Transform target;
  
    void Update()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, target.position.y, transform.position.z);
        transform.LookAt(targetPosition);
    }
}
