using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BustPuzzleManager : MonoBehaviour
{
    public delegate void PlaceBust(BustInteractable stand, CollectableSO bustPlaced);
    public static PlaceBust placeBust;

    private BustInteractable[] pedestalList;

    private static bool listInitialized = false;

    //private Dictionary<BustInteractable, CollectableSO> pedestalDictionary = new Dictionary<BustInteractable, CollectableSO>();

    // the goal is to compare the Interactable's 'requiredItem' field to the current bust whenever the player places a bust

    // when one bust changes -> send data to DataPersistency
    // on start -> take this data back

    [Header("Stand 1")]
    [SerializeField] private BustInteractable stand1;
    private CollectableSO currentBust1;

    [Header("Stand 2")]
    [SerializeField] private BustInteractable stand2;
    private CollectableSO currentBust2;

    [Header("Stand 3")]
    [SerializeField] private BustInteractable stand3;
    private CollectableSO currentBust3;

    [Header("Stand 4")]
    [SerializeField] private BustInteractable stand4;
    private CollectableSO currentBust4;

    [Header("Stand 5")]
    [SerializeField] private BustInteractable stand5;
    private CollectableSO currentBust5;

    private void OnEnable()
    {
        placeBust += PlaceBustOnPedestal;
    }

    private void OnDisable()
    {
        placeBust -= PlaceBustOnPedestal;
    }

    private void Start()
    {
        pedestalList = GetComponentsInChildren<BustInteractable>();

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
                pedestal.UsedItem(WorldVars.BustRoomData[pedestal]);
                Debug.Log($"Calling {pedestal} to add {WorldVars.BustRoomData[pedestal]}");
                //if (WorldVars.BustRoomData[pedestal] != null)
                //{
                    
                //}
                //else
                //{
                //    Debug.Log($"Did not call {pedestal}, the CollectableSO was null");
                //}   
            }
            

            //if (WorldVars.BustRoomData.ContainsKey(pedestal))
            //{
            //    Debug.Log("found a key in BustRoomData");
            //    pedestal.UsedItem(WorldVars.BustRoomData[pedestal]);
            //}
            //else
            //{
            //    Debug.Log($"");
            //}
        }
    }

    private void PlaceBustOnPedestal(BustInteractable stand, CollectableSO bustPlaced)
    {
        Debug.Log($"Placed {bustPlaced} on stand {stand}");
        //Debug.Log($"Stand 1: {stand1}");
        if (stand1 == stand)
        {
            //Debug.Log("They are equal");
        }
        switch (stand)
        {
            case var _ when stand == stand1: stand1 = stand; break;
            case var _ when stand == stand2: stand1 = stand; break;
            case var _ when stand == stand3: stand1 = stand; break;
            case var _ when stand == stand4: stand1 = stand; break;
            case var _ when stand == stand5: stand1 = stand; break;
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

        Debug.Log($"entries in BustRoomData dictionary: {WorldVars.BustRoomData.Count}");
        //WorldVars.BustRoomData.Add(stand, bustPlaced);
        CheckPuzzle();
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
