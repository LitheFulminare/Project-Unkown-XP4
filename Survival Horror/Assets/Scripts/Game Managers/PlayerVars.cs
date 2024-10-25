using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerVars : MonoBehaviour
{
    public delegate void OnPlayerRestrained();
    public static OnPlayerRestrained onPlayerRestrained;

    public delegate void OnPlayerFreed();
    public static OnPlayerFreed onPlayerFreed;

    // these only get updated when 'SceneChanger' calls InventoryController
    public static CollectableSO[] itemList;
    public static int[] itemCount;

    // the public 'AddDocument' adds stuff here
    public static List<DocumentSO> documentList { get; private set; } = new List<DocumentSO>();

    // get by player to know if it's allowed to move or not
    // can only be set by the public static funcion "BlockPlayer"
    public static bool playerBlocked { get; private set; } = false;

    // set by 'SceneChanger'
    public static Vector3 spawnPosition;

    [SerializeField] InventoryController inventoryController;

    private void Start()
    {
        // loads the inventory data if there is any
        if (itemList != null && itemCount != null) inventoryController.LoadInventory(itemList, itemCount);
    }

    // used by UI elements and some other things to block player movement
    public static void BlockPlayer(bool value)
    {
        playerBlocked = value;

        // call functions subscribed to these events
        if (value)
        {
            onPlayerRestrained.Invoke();
        }
        else
        {
            onPlayerFreed.Invoke();
        }
    }

    // Called by 'SceneChanger' after loading the new scene
    public static void UpdateSpawnPosition(Vector3 newPosition)
    {
        spawnPosition = newPosition;
    }

    // rn is only called by 'Interact' method on 'InteractableDocument'
    // I could just add directly to the list but here I can see all the references
    public static void AddDocument(DocumentSO document)
    {
        if (!documentList.Contains(document))
        {
            documentList.Add(document);
        }
    }
}
