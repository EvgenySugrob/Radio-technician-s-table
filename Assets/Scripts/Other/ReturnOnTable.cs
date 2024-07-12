using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnOnTable : MonoBehaviour
{
    [SerializeField] Transform pointReturn;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        
        if (other.GetComponent<Rigidbody>())
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.position = pointReturn.position;
        }
    }
}
