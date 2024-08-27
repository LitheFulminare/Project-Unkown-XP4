using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    // follow this tutorial to make the interactor system
    // https://www.youtube.com/watch?v=K06lVKiY-sY
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F was pressed");
        }
    }
}
