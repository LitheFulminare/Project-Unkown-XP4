using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Sprite pistolAmmoImg;
    public Sprite pistolImg;
    public Sprite syringeImg;

    // called by InventoryController when Tab is pressed
    public void showItems(Items[] itemList, int[] itemCount)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            // sets the icon and the amount matching the list on the 'ShowIcon' sript
            // if itemList[i] is 'Items.Empty' nothing is shown
            // if itemCount[i] is '0' no text is shown
            GameObject.Find($"Icon ({i})").SendMessage("ChangeIcon", itemList[i]);
            GameObject.Find($"Icon ({i})").SendMessage("ChangeText", itemCount[i]);
        }
    }
}
