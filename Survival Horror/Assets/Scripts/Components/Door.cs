using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    public bool active = false;

    // called by "Interactable" and if the door is unlocked
    public void Use()
    {
        Debug.Log("Player interacted with door and went to another room");
    }

    // probably wont be used
    // 'puzzleComplete' in 'Interactable" is a better alternative
    public void SetActive(bool parameter)
    {
        active = parameter;
    }
}

