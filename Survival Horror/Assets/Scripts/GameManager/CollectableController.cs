using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    //public List<GameObject> collectables = new List<GameObject>();
    //private bool itemsAdded = false;

    //private void Start()
    //{
    //    // find a way to save these. maybe add this to DontDestroyOnLoad? 
    //    // i dont wanna make this static as every room should have independent lists
    //    // but the data shouldnt be lost when switching scenes
    //    if (!itemsAdded)
    //    {
    //        collectables.AddRange(GameObject.FindGameObjectsWithTag("Collectable"));
    //        itemsAdded = true;
    //        Debug.Log("Collectables added for the first time");
    //    }
        
    //    Debug.Log($"Elements in 'collectables': {collectables.Count}");
    //}
}
