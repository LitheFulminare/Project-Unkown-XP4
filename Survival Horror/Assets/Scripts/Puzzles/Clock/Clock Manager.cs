using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockManager : MonoBehaviour
{
    [SerializeField] Image puzzleImage;

    public List<ClockInteractable> clockList = new List<ClockInteractable>();

    private void Start()
    {
        //clockList.AddRange(FindObjectsOfType<ClockInteractable>());
        //puzzleImage.color = Color.clear;
    }

    public void Interact(DocumentSO documentSO)
    {
        //puzzleImage.sprite = documentSO.inspectImage;
    }

    // prob will be called when the player exits the screen
    public void PlayerConfirmed(ClockInteractable clock)
    {
        foreach (ClockInteractable obj in clockList)
        {
            obj.CheckIfTimeMatches();
        }
    }
}
