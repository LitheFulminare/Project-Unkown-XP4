using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Sprite pistolAmmoImg;
    public Sprite syringeImg;

    // called by InventoryController when Tab is pressed
    public void showItems(Items[] itemList)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            // checkes if there's an actual item that should be shown
            if (itemList[i] != Items.empty)
            {
                // calls each slot icon and passes what item should be displayed
                GameObject.Find($"Icon ({i})").SendMessage("ChangeIcon", itemList[i]); // also pass how many of these items
            }
        }
    }
}
