using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    List<GameObject> collectables = new List<GameObject>();

    private void Start()
    {
        collectables.AddRange(GameObject.FindGameObjectsWithTag("Collectable"));
        Debug.Log($"Elements in 'collectables: {collectables.Count}");
    }
}
