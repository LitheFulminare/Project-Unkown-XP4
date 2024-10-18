using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item")]
public class CollectableSO : ScriptableObject
{
    // general
    public string ingameName; // what appears on the prompt when the player tries to collect the item
    public string inventoryName; // what appears on the inventory
    public bool isDocument; // when true, will go to the 'documents' tab instead of the regular inventory

    // specific to collectable items, not documents
    public int stack; // qtd gained when collected. 0 won't show any number on the inventory
    public Sprite iconImg;
    public GameObject inspectModel; // what appears when inspected
}
