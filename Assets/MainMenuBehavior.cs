using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehavior : MonoBehaviour
{

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void EndGame()
    {
        //si estamoe en el editor vs si estamos en el ejecutable
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit()
        #endif

    }
}

