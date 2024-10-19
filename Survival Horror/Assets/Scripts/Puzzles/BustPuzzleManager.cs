using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BustPuzzleManager : MonoBehaviour
{
    public delegate void PlaceBust();
    public static PlaceBust placeBust;

    // the goal is to compare the Interactable's 'requiredItem' field to the current bust whenever the player places a bust

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

    private void PlaceBustOnPedestal()
    {
        Debug.Log("Placed {bust} on stand {stand}");
    }
}
