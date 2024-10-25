using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : MonoBehaviour
{
    public List<ClockInteractable> clockList = new List<ClockInteractable>();

    private void Start()
    {
        clockList.AddRange(FindObjectsOfType<ClockInteractable>());
    }

    public void PlayerConfirmed(ClockInteractable clock)
    {
        foreach (ClockInteractable obj in clockList)
        {
            obj.CheckIfTimeMatches();
        }
    }
}
