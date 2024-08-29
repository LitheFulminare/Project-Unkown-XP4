using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// interfaces
// https://learn.unity.com/tutorial/interfaces#

// events
// https://learn.unity.com/tutorial/events-uh#

//public interface IButtonConfirm
//{
//    bool Confirm(bool confirmPressed);
//}

public class OverlayController : MonoBehaviour
{
    public GameObject canvas;

    private void Start()
    {
        canvas.SetActive(false);
    }

    public void setActive()
    {
        canvas.SetActive(true);
    }

    // called by the 'confirm' and 'deny' buttons the pickup screen overlay
    // 'confirm' returns true and 'deny' returns false
    public void ButtonAction(bool confirm)
    {
        Debug.Log(confirm);
    }
}
