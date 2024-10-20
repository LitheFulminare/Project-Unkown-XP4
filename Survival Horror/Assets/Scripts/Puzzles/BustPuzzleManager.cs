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

        // need to optimize this
        stand1.UsedItem(currentBust1);
        stand2.UsedItem(currentBust2);
        stand3.UsedItem(currentBust3);
        stand4.UsedItem(currentBust4);
        stand5.UsedItem(currentBust5);  
    }

    public void SaveAndLoadItems()
    {
        Debug.LogWarning("Useless function called, remove this and its references");
    }

    private void PlaceBustOnPedestal(BustInteractable stand, CollectableSO bustPlaced)
    {
        switch (stand)
        {
            case var _ when stand == stand1: currentBust1 = stand.currentItem; break;
            case var _ when stand == stand2: currentBust2 = stand.currentItem; break;
            case var _ when stand == stand3: currentBust3 = stand.currentItem; break;
            case var _ when stand == stand4: currentBust4 = stand.currentItem; break;
            case var _ when stand == stand5: currentBust5 = stand.currentItem; break;
        }

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

        foreach (BustInteractable pedestal in pedestalList)
        {
            if (!pedestal.CheckIfItemsMatch())
            {
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
