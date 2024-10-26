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

    [SerializeField] GameObject documentTexts;

    private void Start()
    {
        ButtonClick(Tabs.Inventory);
    }

    private void ButtonClick(Tabs tabName)
    {
        documentTexts.SetActive(false);
        slots.SetActive(false);

        if (tabName == Tabs.Inventory)
        {
            ShowInventory();
            return;
        }       

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
        documentTexts.SetActive(true);
        canvas.sprite = documentsImage;
    }

    public void InventoryButtonClick() => ButtonClick(Tabs.Inventory);
    public void DocumentsButtonClick() => ButtonClick(Tabs.Documents);
    public void MapButtonClick() => ButtonClick(Tabs.Map);
}
