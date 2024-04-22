using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilmResistChoice : MonoBehaviour
{
    [Header("ResistPrefab")]
    [SerializeField] PrefabRisistNominalSetting resistPrefab;

    [Header("Marking and choise resist Window")]
    [SerializeField] GameObject markingWindow;
    [SerializeField] GameObject choiceWindow;

    public void DelegatePrefabParams()
    {
        resistPrefab.GetParamToMarkingWindow();
        markingWindow.SetActive(true);
        choiceWindow.SetActive(false);
    }
}
