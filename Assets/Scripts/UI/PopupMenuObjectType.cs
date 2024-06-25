using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeInterectableObject
{
    FullInterectable,
    HalfInterectable,
    Tweezers,
    Pliers,
    SideCutters,
    None
}

public class PopupMenuObjectType : MonoBehaviour
{
    [SerializeField] PopupMenuCustom popupMenuCustom;
    [SerializeField] TypeInterectableObject typeInterectableObject;

    public void Show()
    {
        popupMenuCustom.OpenPopupMenu(gameObject, typeInterectableObject);
    }
    public void RotateObjectSpacePress()
    {
        popupMenuCustom.SpacePressRotationSwap(gameObject);
    }
}
