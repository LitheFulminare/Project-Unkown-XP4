using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Textpopup : MonoBehaviour
{
    [SerializeField] private Text text;

    public void SetText(bool rightItem)
    {
        if (!rightItem)
        {
            text.text = "This cannot be used here";
        }
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
