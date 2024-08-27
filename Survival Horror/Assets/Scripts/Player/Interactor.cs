using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F was pressed");
        }
    }
}
