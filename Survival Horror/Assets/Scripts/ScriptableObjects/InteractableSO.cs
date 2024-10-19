using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interactable", menuName = "ScriptableObject/Interactable")]
public class InteractableSO : ScriptableObject
{
    public string description; // ex.: 'There a small pedestal'
    public string prompt; // ex.: 'Use item?'

    public string altDescription; // ex.: 'There is a {currentItem.name} on the pedestal'
    public string altPrompt; // ex.: 'Take it back'?
}
