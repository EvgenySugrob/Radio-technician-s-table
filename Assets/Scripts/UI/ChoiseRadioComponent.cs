using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiseRadioComponent : MonoBehaviour
{
    [SerializeField] List<GameObject> componentWindowList;

    public void EnableComponentTypeWindow(int typeComponent)
    {
        foreach (GameObject componentWindow in componentWindowList)
        {
            componentWindow.SetActive(false);
        }

        transform.gameObject.SetActive(true);

        switch (typeComponent) 
        {
             case 0:
                componentWindowList[0].SetActive(true);
                break;
             case 1:
                componentWindowList[1].SetActive(true);
                break;
             case 2:
                componentWindowList[2].SetActive(true);
                break;
             case 3:
                componentWindowList[3].SetActive(true);
                break;
        }
    }
}
