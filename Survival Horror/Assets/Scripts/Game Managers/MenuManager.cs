using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string firstScene;
    [SerializeField] private SceneChanger sceneChanger;

    public void PlayButtonClick()
    {
        SceneManager.LoadScene(firstScene);
    }
}
