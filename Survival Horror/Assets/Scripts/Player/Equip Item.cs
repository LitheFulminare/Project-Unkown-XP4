using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EquipItem : MonoBehaviour
{
    [SerializeField] private GameObject revolver;

    private static Items equippedItem = Items.empty;

    private void Start()
    {
        if (equippedItem != Items.empty)
        {
            Equip(equippedItem);
        }
    }

    public void Equip(Items item)
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
