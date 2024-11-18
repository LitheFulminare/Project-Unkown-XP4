using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string firstScene;

    private void Start()
    {
        AudioManager.instance.InitializeMenuSong(FMODEvents.instance.menuMusic);
    }

    public void PlayButtonClick()
    {
        PlayClickSound();

        SceneManager.LoadScene(firstScene);
    }

    public void ExitButtonClick()
    {
        PlayClickSound();

        // closing the game while running the executable or in-engine is different

        #if UNITY_STANDALONE
                Application.Quit();
        #endif
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private void PlayClickSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonSelected, this.transform.position);
    }
}
