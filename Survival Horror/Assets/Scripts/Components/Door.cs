using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    [SerializeField] SceneChanger sceneChanger;
    public bool active = false;

    // type the name of the scene the player should go to
    [SerializeField] string destination;
    

    // called by "Interactable" and if the door is unlocked
    public void Use()
    {
        sceneChanger.LoadScene(destination);
        //Debug.Log("Player interacted with door and went to another room");
    }

    // probably wont be used
    // 'puzzleComplete' in 'Interactable" is a better alternative
    public void SetActive(bool parameter)
    {
        active = parameter;
    }
}

