using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
    [SerializeField] Vector2 resolution = new Vector2(320,240);

    void Start()
    {
        // Switch to 640 x 480 full-screen
        Screen.SetResolution((int)resolution.x, (int)resolution.y, true);
    }
}
