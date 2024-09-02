using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public delegate void ItemReceiver(Items item);
    public static ItemReceiver itemReceiver;

    //public delegate void OpenInventory();
    //public static OpenInventory inventory;

    public Items[] itemList = new Items[6];
    public int[] itemCount = new int[6];

    public GameObject canvas;

    private void Start()
    {
        itemReceiver = addToInventory;
        //inventory = toggleInventory;

        canvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            toggleInventory();
        }

    }

    private void toggleInventory()
    {
        if (canvas.activeSelf == true)
        {
            canvas.SetActive(false);
        }
        else
        {
            canvas.SetActive(true);
        }
    }

    private void addToInventory(Items item)
    {
        Debug.Log("addToInventory(Items item)");

        for (int i = 0; i < itemList.Length; i++) // first checks if there is the item in the inventory and adds to the stack
        {
            if (itemList[i] == Items.empty) // checks if there is and empty space
            {
                itemList[i] = item; // replaces the empty space with the item received
                switch (item)
                {
                    case Items.pistolAmmo:
                        Debug.Log("Pistol ammo was collected");
                        itemCount[i] += 6;
                        break;

                    case Items.syringe:
                        Debug.Log("Syringe was collected");
                        itemCount[i] += 1;
                        break;

                    default:
                        Debug.Log("Invalid Item");
                        break;
                }
                break;
            }
        }

        //itemList.Append(item); this adds to the 7th space, which doesnt exist

        
    }
}
