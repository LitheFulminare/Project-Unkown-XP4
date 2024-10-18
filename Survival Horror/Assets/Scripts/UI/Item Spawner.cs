using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    //public Sprite pistolAmmoImg;
    //public Sprite pistolImg;
    //public Sprite syringeImg;

    // called by InventoryController when Tab is pressed
    public void showItems(CollectableSO[] itemList, int[] itemCount)
    {
        Debug.Log("showItem was called");

        for (int i = 0; i < itemList.Length; i++)
        {
            // sets the icon and the amount matching the list on the 'ShowIcon' sript
            // if itemCount[i] is '0' no text is shown
            if (itemList[i] != null) GameObject.Find($"Icon ({i})").SendMessage("ChangeIcon", itemList[i]);
            // else { ClearIcon(); }

            GameObject.Find($"Icon ({i})").SendMessage("ChangeText", itemCount[i]);
        }
    }
}
