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
        // foreach (BustInteractable pedestal in pedestalList)
        // add to the dictionary 




        //for (int i = 0; i < WorldVars.BustRoomData.Count; i++)
        //{
        //    Debug.Log($"{WorldVars.BustRoomData.Values}");
        //}

        // handles persistent data
        // calls the pedestals to load what busts were previously on them befere exiting the scene
        foreach (BustInteractable pedestal in pedestalList)
        {
            if (WorldVars.BustRoomData[pedestal] != null)
            {
                pedestal.UsedItem(WorldVars.BustRoomData[pedestal]);
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
        //Debug.Log($"Placed {bustPlaced} on stand {stand}");

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
