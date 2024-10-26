using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public enum Tabs
{
    Inventory,
    Documents,
    Map
}

public class TabSystem : MonoBehaviour
{
    [SerializeField] Sprite inventoryImage;
    [SerializeField] Sprite documentsImage;
    [SerializeField] Sprite mapImage;

    [SerializeField] Image canvas;

    [SerializeField] GameObject slots;

    private void Start()
    {
        ButtonClick(Tabs.Inventory);
    }

    private void ButtonClick(Tabs tabName)
    {
        Debug.Log($"Button Click was called with parameter {tabName.ToString()}");

        if (tabName == Tabs.Inventory)
        {
            ShowInventory();
            return;
        }

        slots.SetActive(false);

        if (tabName == Tabs.Documents)
        {
            ShowDocuments();
            return;
        }
    }

    private void ShowInventory()
    {
        slots.SetActive(true);
        canvas.sprite = inventoryImage;
    }

    private void ShowDocuments()
    {
        canvas.sprite = documentsImage;
    }

    public void InventoryButtonClick() => ButtonClick(Tabs.Inventory);
    public void DocumentsButtonClick() => ButtonClick(Tabs.Documents);
    public void MapButtonClick() => ButtonClick(Tabs.Map);
}
