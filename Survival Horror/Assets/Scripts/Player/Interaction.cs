using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    // https://discussions.unity.com/t/help-with-calling-function-from-another-script-solved/869015

    public PlayerVars playerVars;

    public void interact(Items item)
    {
        if (item == Items.pistolAmmo)
        {
            Debug.Log("Pistol ammo was collected");
        }
        Debug.Log("interaction happened");
    }
}
