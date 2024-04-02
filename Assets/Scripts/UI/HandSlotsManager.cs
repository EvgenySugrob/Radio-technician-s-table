using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSlotsManager : MonoBehaviour
{
    [Header("Physic component")]
    [SerializeField] List<HandSlot> handSlotList;

    public void CheckHandSlot(GameObject gameObject)
    {
        foreach (HandSlot slot in handSlotList)
        {
            if(slot.IsSlotFree())
            {
                slot.SetObject(gameObject);
                break;
            }
            else
            {
                Debug.Log("В рот себе это засунь");
            }
        }
    }
}
