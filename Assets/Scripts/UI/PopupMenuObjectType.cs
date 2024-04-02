using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeInterectableObject
{
    FullInterectable,
    HalfInterectable,
    None
}

public class PopupMenuObjectType : MonoBehaviour
{
    [SerializeField] PopupMenuCustom popupMenuCustom;
    [SerializeField] TypeInterectableObject typeInterectableObject;

    private void Awake()
    {
        popupMenuCustom = FindObjectOfType<PopupMenuCustom>();
    }

    public void Show()
    {
        popupMenuCustom.OpenPopupMenu(gameObject, typeInterectableObject);
    }
}
