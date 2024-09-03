using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIcon : MonoBehaviour
{
    public void ChangeIcon(Items item)
    {
        Debug.Log($"ChangeIcon was called, the parameter {item} was passed");
    }
}
