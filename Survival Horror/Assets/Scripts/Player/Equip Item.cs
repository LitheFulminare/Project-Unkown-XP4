using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EquipItem : MonoBehaviour
{
    // delegates that will handle equipping item
    public delegate void OnEquipItem(Items item);
    public static OnEquipItem equipItem;

    [SerializeField] private GameObject revolver;

    private static Items equippedItem = Items.empty;

    private void OnEnable()
    {
        equipItem += Equip;
    }

    private void OnDisable()
    {
        equipItem -= Equip;
    }

    private void Start()
    {
        if (equippedItem != Items.empty)
        {
            Equip(equippedItem);
        }
    }

    public void Equip(Items item)
    {
        // checks if item isn't already equipped
        // also needs to destroy the equipped item instance when equipping another item
        if (item != equippedItem)
        {
            // instantiates the item and sets it as the 'equippedItem'
            // also does some error checks
            switch (item)
            {
                case Items.pistol: if (revolver != null) { Instantiate(revolver); equippedItem = item; } else { Debug.LogError("Could not instantiate the item, reference is null"); } break;

                default: Debug.LogError("'Item' parameter could not be resolved as an equippable item"); break;
            }
        }          
    }
}
