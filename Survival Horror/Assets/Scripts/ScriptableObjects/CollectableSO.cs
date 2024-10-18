using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item")]
public class CollectableSO : ScriptableObject
{
    // should adapt this so that documents are their own thing

    // general
    [Header("General")]
    public string ingameName; // what appears on the prompt when the player tries to collect the item
    public string inventoryName; // what appears on the inventory
    public ItemType itemType; // wheter it's a document, a weapon, consumable, etc 

    // specific to collectable items, not documents
    [Header("Collectable Only")]
    public int stack; // qtd gained when collected. 0 won't show any number on the inventory
    public Sprite iconImg;
    public GameObject inspectModel; // what appears when inspected
}
