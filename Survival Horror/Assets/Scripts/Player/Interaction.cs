using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    // https://discussions.unity.com/t/help-with-calling-function-from-another-script-solved/869015

    public PlayerVars playerVars;

    public void interact(Items item) // receives an item from the Item enum from the Collectable script
    {
        switch (item)
        {
            case Items.pistolAmmo:
                Debug.Log("Pistol ammo was collected");
                break;

            case Items.syringe:
                Debug.Log("Syringe was collected");
                break;

            default:
                Debug.Log("Invalid Item");
                break;
        }

        // old item collection script
         
        //if (item == Items.pistolAmmo)
        //{
        //    Debug.Log("Pistol ammo was collected");
        //}
        //Debug.Log("interaction happened");
    }
}
