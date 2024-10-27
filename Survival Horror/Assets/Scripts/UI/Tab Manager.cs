using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public enum Tabs
{
    Inventory,
    Documents,
    Map
}

public class TabManager : MonoBehaviour
{
    public delegate void ResetTabs();
    public static ResetTabs ShowInventoryTab;

    [SerializeField] private Sprite inventoryBackground;
    [SerializeField] private Sprite documentBackgroud;
    [SerializeField] private Sprite mapBackground;

    [SerializeField] Image canvas;

    [SerializeField] GameObject slots;

    [SerializeField] GameObject documentTexts;

    public static Tabs currentTab;

    private void OnEnable()
    {
        ShowInventoryTab += InventoryButtonClick;
    }

    private void OnDisable()
    {
        ShowInventoryTab -= InventoryButtonClick;
    }

    private void Start()
    {
        ButtonClick(Tabs.Inventory);
    }

    private void ButtonClick(Tabs tabName)
    {
        currentTab = tabName;
        documentTexts.SetActive(false);
        slots.SetActive(false);
        DocumentImageManager.HideImage();

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

        if (tabName == Tabs.Map)
        {
            //ShowMap();
            return;
        }
    }

    private void ShowInventory()
    {
        slots.SetActive(true);
        canvas.sprite = inventoryBackground;
    }

    private void ShowDocuments()
    {
        documentTexts.SetActive(true);
        canvas.sprite = documentBackgroud;
        DocumentTextManager.updateText();
        if (PlayerVars.documentList.Count > 0) DocumentImageManager.UpdateImage(PlayerVars.documentList[0]);
    }

    public void InventoryButtonClick() => ButtonClick(Tabs.Inventory);
    public void DocumentsButtonClick() => ButtonClick(Tabs.Documents);
    public void MapButtonClick() => ButtonClick(Tabs.Map);
}
