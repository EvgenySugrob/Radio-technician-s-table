using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMagnet : MonoBehaviour
{
    [SerializeField] List<ObjectForMagnet> objectsOnMagnet;
    [SerializeField] Transform pivotY;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IMagnet>() != null)
        {
            ObjectForMagnet objectMagnet = other.GetComponent<ObjectForMagnet>();
            
            if(objectsOnMagnet.Contains(objectMagnet)==false)
            {
                objectsOnMagnet.Add(objectMagnet);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<IMagnet>()!=null)
        {
            ObjectForMagnet objectMagnet = other.GetComponent<ObjectForMagnet>();
            if(objectsOnMagnet.Contains(objectMagnet))
            {
                objectsOnMagnet.Remove(objectMagnet);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        other.TryGetComponent<IDrag>(out var dragItem);
        other.TryGetComponent<IRotation>(out var rotationItem);
        other.TryGetComponent<IMagnet>(out var magnetItem);

        if(dragItem.isMovebale == false && rotationItem.isRotation == false)
        {
            magnetItem.onMagnet = true;
            magnetItem.Magnet();
            other.transform.position = new Vector3(other.transform.position.x,pivotY.position.y,pivotY.position.z);
        }
    }
}
