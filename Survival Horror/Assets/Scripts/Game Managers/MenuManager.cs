using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string firstScene;

    public void PlayButtonClick()
    {
        SceneManager.LoadScene(firstScene);
    }

    public void ExitButtonClick()
    {
        // closing the game while running the executable or in-engine is different

        #if UNITY_STANDALONE
                Application.Quit();
        #endif
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
