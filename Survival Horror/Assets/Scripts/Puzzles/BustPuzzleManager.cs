using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BustPuzzleManager : MonoBehaviour
{
    public delegate void PlaceBust(BustInteractable stand, CollectableSO bustPlaced);
    public static PlaceBust placeBust;
    public static PlaceBust removeBust;

    private BustInteractable[] pedestalList;

    private static bool listInitialized = false;

    private static bool itemsInitialized = false;

    //private Dictionary<BustInteractable, CollectableSO> pedestalDictionary = new Dictionary<BustInteractable, CollectableSO>();

    // the goal is to compare the Interactable's 'requiredItem' field to the current bust whenever the player places a bust

    // when one bust changes -> send data to DataPersistency
    // on start -> take this data back

    [Header("Stand 1")]
    [SerializeField] private BustInteractable stand1;
    private static CollectableSO currentBust1;

    [Header("Stand 2")]
    [SerializeField] private BustInteractable stand2;
    private static CollectableSO currentBust2;

    [Header("Stand 3")]
    [SerializeField] private BustInteractable stand3;
    private static CollectableSO currentBust3;

    [Header("Stand 4")]
    [SerializeField] private BustInteractable stand4;
    private static CollectableSO currentBust4;

    [Header("Stand 5")]
    [SerializeField] private BustInteractable stand5;
    private static CollectableSO currentBust5;

    private void OnEnable()
    {
        placeBust += PlaceBustOnPedestal;
        removeBust += RemoveBust;
    }

    private void OnDisable()
    {
        placeBust -= PlaceBustOnPedestal;
        removeBust -= RemoveBust;
    }

    private void Start()
    {
        pedestalList = GetComponentsInChildren<BustInteractable>();

        // i could simply save the current bust here and ignore the non static ones lol
        SaveAndLoadItems();

        stand1.UsedItem(currentBust1);
        stand2.UsedItem(currentBust2);
        stand3.UsedItem(currentBust3);
        stand4.UsedItem(currentBust4);
        stand5.UsedItem(currentBust5);

        /*

        Debug.Log($"entries in BustRoomData dictionary: {WorldVars.BustRoomData.Count}");

        // I should initalize the data before the foreach
        if (!listInitialized)
        {
            foreach (BustInteractable pedestal in pedestalList)
            {
                WorldVars.BustRoomData.Add(pedestal, pedestal.currentItem);
            }

            listInitialized = true;
        }

        foreach (BustInteractable pedestal in pedestalList)
        {
            if (WorldVars.BustRoomData.ContainsKey(pedestal))
            {
                //pedestal.UsedItem(WorldVars.BustRoomData[pedestal]);
                Debug.Log($"Calling {pedestal} to add {WorldVars.BustRoomData[pedestal]}");
            }
        } 

        */
    }

    public void SaveAndLoadItems()
    {
        //if (!itemsInitialized)
        //{
        //    currentBust1 = stand1.currentItem;
        //    currentBust2 = stand2.currentItem;
        //    currentBust3 = stand3.currentItem;
        //    currentBust4 = stand4.currentItem;
        //    currentBust5 = stand5.currentItem;

        //    itemsInitialized = true;
        //}

        //else
        //{
        //    Debug.Log($"currentBust4: {currentBust4}");
        //    stand1.currentItem = currentBust1;
        //    stand2.currentItem = currentBust2;
        //    stand3.currentItem = currentBust3;
        //    stand4.currentItem = currentBust4;
        //    stand5.currentItem = currentBust5;
        //}
    }

    private void PlaceBustOnPedestal(BustInteractable stand, CollectableSO bustPlaced)
    {
        //Debug.Log($"Placed {bustPlaced} on stand {stand}");
        //Debug.Log($"Stand 1: {stand1}");

        switch (stand)
        {
            case var _ when stand == stand1: currentBust1 = stand.currentItem; break;
            case var _ when stand == stand2: currentBust2 = stand.currentItem; break;
            case var _ when stand == stand3: currentBust3 = stand.currentItem; break;
            case var _ when stand == stand4: currentBust4 = stand.currentItem; break;
            case var _ when stand == stand5: currentBust5 = stand.currentItem; break;
        }


        //if (pedestalDictionary.ContainsKey(stand))
        //{
        //    pedestalDictionary[stand] = bustPlaced;
        //}
        //else
        //{
        //    pedestalDictionary.Add(stand, bustPlaced);
        //}

        if (WorldVars.BustRoomData.ContainsKey(stand))
        {
            WorldVars.BustRoomData[stand] = bustPlaced;
        }
        else
        {
            WorldVars.BustRoomData.Add(stand, bustPlaced);
        }

        CheckPuzzle();
    }

    private void RemoveBust(BustInteractable stand, CollectableSO bust)
    {
        switch (stand)
        {
            case var _ when stand == stand1: currentBust1 = null; break;
            case var _ when stand == stand2: currentBust2 = null; break;
            case var _ when stand == stand3: currentBust3 = null; break;
            case var _ when stand == stand4: currentBust4 = null; break;
            case var _ when stand == stand5: currentBust5 = null; break;
        }
    }

    private void CheckPuzzle()
    {
        bool puzzleComplete = true;

        //for (int i = 0; i < pedestalData.Count; i++)
        //{
            
        //}

        foreach (BustInteractable pedestal in pedestalList)
        {
            if (!pedestal.CheckIfItemsMatch())
            {
                //WorldVars.BustRoomData[pedestal]

                puzzleComplete = false;
                break;
            }
        }

        if (puzzleComplete)
        {
            Debug.Log("All sculptures in place, the puzzle is solved!");
        }
    }
}
