using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    // https://discussions.unity.com/t/help-with-calling-function-from-another-script-solved/869015

    public PlayerVars playerVars;

    public void interact()
    {
        Debug.Log("interaction happened");
    }
}
