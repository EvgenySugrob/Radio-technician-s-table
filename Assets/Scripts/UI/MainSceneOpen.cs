using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneOpen : MonoBehaviour
{
    [SerializeField] int mainSceneNum;

    public void LoadSceneMain()
    {
        SceneManager.LoadScene(mainSceneNum);
    }
}
