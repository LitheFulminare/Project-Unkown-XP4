using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Sprite pistolAmmoImg;
    public Sprite syringeImg;

    // called by InventoryController when Tab is pressed
    public void showItems(Items[] itemList, int[] itemCount)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            GameObject.Find($"Icon ({i})").SendMessage("ChangeIcon", itemList[i]);
            if (itemCount[i] !=0)
            {
                GameObject.Find($"Icon ({i})").SendMessage("ChangeText", itemCount[i]);
            }
            
        }
    }
}
