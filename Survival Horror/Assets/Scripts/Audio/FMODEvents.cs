using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Select SFX")]
    [field: SerializeField] public EventReference buttonSelected { get; private set; }
    [field: SerializeField] public EventReference open { get; private set; }

    public static FMODEvents instance {  get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            Debug.LogError("Found more than one FMODEvents instance in this scene.");
        }
        instance = this;
    }
}
