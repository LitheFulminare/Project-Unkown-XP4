using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private DocumentSO document;

    void Start()
    {
        Debug.Log(document.description[0]);
    }
}
