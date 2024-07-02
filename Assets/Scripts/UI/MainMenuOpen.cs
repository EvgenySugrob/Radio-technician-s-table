using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuOpen : MonoBehaviour
{
    [SerializeField] int mainMenuSceneNum;

    public void LoadSceneMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneNum);
    }
}
