using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BustPuzzleManager : MonoBehaviour
{
    // the goal is to compare the Interactable's 'requiredItem' field to the current bust whenever the player places a bust

    [Header("Stand 1")]
    [SerializeField] private Interactable stand1;
    private CollectableSO currentBust1;

    [Header("Stand 2")]
    [SerializeField] private Interactable stand2;
    private CollectableSO currentBust2;

    [Header("Stand 3")]
    [SerializeField] private Interactable stand3;
    private CollectableSO currentBust3;

    [Header("Stand 4")]
    [SerializeField] private Interactable stand4;
    private CollectableSO currentBust4;

    [Header("Stand 5")]
    [SerializeField] private Interactable stand5;
    private CollectableSO currentBust5;
}
