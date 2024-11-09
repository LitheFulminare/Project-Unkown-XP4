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
        Debug.Log($"Exit button pressed");
        Application.Quit();
    }
}
