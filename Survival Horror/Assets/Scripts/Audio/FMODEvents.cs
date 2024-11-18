using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }

    [field: Header("UI SFX")]
    [field: SerializeField] public EventReference buttonSelected { get; private set; }
    [field: SerializeField] public EventReference open { get; private set; }

    [field: Header("UI SFX")]
    [field: SerializeField] public EventReference flickeringLight { get; private set; }

    public static FMODEvents instance {  get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMODEvents instance in this scene.");
        }
        instance = this;
    }
}
