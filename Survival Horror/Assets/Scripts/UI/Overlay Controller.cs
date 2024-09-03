using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// interfaces
// https://learn.unity.com/tutorial/interfaces#

// events
// https://learn.unity.com/tutorial/events-uh#
// https://gamedevbeginner.com/events-and-delegates-in-unity/

//public interface IButtonConfirm
//{
//    bool Confirm(bool confirmPressed);
//}



public class OverlayController : MonoBehaviour
{
    public delegate void PickupOverlay(GameObject collectedItem);
    public static PickupOverlay showUI; // this is used to show the overlay when an item is picked

    public GameObject canvas;

    public GameObject collectedItem;

    private void Start()
    {
        canvas.SetActive(false);

        showUI = setActive;
    }

    // shows the pickup overlay
    public void setActive(GameObject collectedItem)
    {
        canvas.SetActive(true);
        this.collectedItem = collectedItem;
    }

    // called by the 'confirm' and 'deny' buttons the pickup screen overlay
    // 'confirm' returns true and 'deny' returns false
    public void ButtonAction(bool playerAction)
    {

        if (playerAction)
        {        
            // search object by name and then call the function to be collected
            GameObject itemToDestroy = GameObject.Find(collectedItem.name);
            itemToDestroy.SendMessage("collected");
        }
    }
}
