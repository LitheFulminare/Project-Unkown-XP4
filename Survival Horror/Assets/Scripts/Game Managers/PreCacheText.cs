using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreCacheText : MonoBehaviour
{
    private static bool fontCached = false;

    [SerializeField] private Font myFont;

    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
        CacheFont();
    }

    void CacheFont()
    {
        if (!fontCached)
        {
            string dummyText = "dummy Text";
            //GUIStyle style = new GUIStyle { font = myFont };
            text.text = dummyText;
            fontCached = true;
        }      
    }
}
