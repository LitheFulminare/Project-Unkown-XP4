using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BustPuzzleManager : MonoBehaviour
{
    [Header("Stand 1")]
    [SerializeField] private Interactable stand1;
    [SerializeField] private CollectableSO requiredItem1;

    [Header("Stand 2")]
    [SerializeField] private Interactable stand2;
    [SerializeField] private CollectableSO requiredItem2;

    [Header("Stand 3")]
    [SerializeField] private Interactable stand3;
    [SerializeField] private CollectableSO requiredItem3;

    [Header("Stand 4")]
    [SerializeField] private Interactable stand4;
    [SerializeField] private CollectableSO requiredItem4;

    [Header("Stand 5")]
    [SerializeField] private Interactable stand5;
    [SerializeField] private CollectableSO requiredItem5;
}
