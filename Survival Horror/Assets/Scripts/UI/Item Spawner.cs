using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Sprite pistolAmmoImg;
    public Sprite syringeImg;

    public void showItems(Items[] itemList)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] != Items.empty)
            {
                GameObject icon = GameObject.Find($"Icon ({i})");
                icon.SendMessage("ChangeIcon", itemList[i]);
            }

            //switch (itemList[i])
            //{
            //    case Items.pistolAmmo:
            //        Debug.Log(itemList[i]);
            //        Debug.Log(i);
            //        GameObject icon = GameObject.Find($"Icon ({i})");
            //        icon.SendMessage("ChangeIcon", itemList[i]);

            //        break;
            //}
        }
    }
}
