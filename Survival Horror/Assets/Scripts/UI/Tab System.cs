using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum Tabs
{
    Inventory,
    Documents,
    Map
}

public class TabSystem : MonoBehaviour
{
    private void ButtonClick(Tabs tabName)
    {
        Debug.Log($"Button Click was called with parameter {tabName.ToString()}");
    }

    public void InventoryButtonClick() => ButtonClick(Tabs.Inventory);
    public void DocumentsButtonClick() => ButtonClick(Tabs.Documents);
    public void MapButtonClick() => ButtonClick(Tabs.Map);
}
