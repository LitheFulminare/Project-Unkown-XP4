using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using static UnityEditor.Progress;

public class InventoryController : MonoBehaviour
{
    public delegate void ItemReceiver(Items item);
    public static ItemReceiver itemReceiver; // used to add item to inventory

    public Items[] itemList = new Items[6]; // list of the items
    public int[] itemCount = new int[6]; // list of how many items the player has

    public GameObject canvas;

    public ItemSpawner itemSpawner; // what calls each icon to show items in inventory

    private void Start()
    {
        itemReceiver = addToInventory; // called by the collectable
        canvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            toggleInventory();
        }

    }

    // toggles the visibility
    private void toggleInventory()
    {
        if (canvas.activeSelf == true)
        {
            canvas.SetActive(false);
        }
        else
        {
            canvas.SetActive(true);
            itemSpawner.showItems(itemList, itemCount); // what calls each icon to show items in inventory
        }
    }

    

    // called by collectable, receives the item type
    private void addToInventory(Items item)
    {

        // searches for each of the inventory slots
        for (int i = 0; i < itemList.Length; i++)
        {

            // checks if there is and empty space or the same item
            if (itemList[i] == Items.empty || itemList[i] == item) 
            {

                // replaces the space with the item received and adds to the stack
                itemList[i] = item; 
                switch (item) 
                {
                    case Items.pistolAmmo:
                        //Debug.Log("Pistol ammo was collected");
                        itemCount[i] += 6;
                        break;

                    case Items.syringe:
                        //Debug.Log("Syringe was collected");
                        itemCount[i] += 1;
                        break;

                    default:
                        Debug.Log("Invalid Item");
                        break;
                }
                break; 
            }
        }        
    }
}
