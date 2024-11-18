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
    // used to always go to the inventory tab when the player open the menu
    public delegate void ResetTabs();
    public static ResetTabs ShowInventoryTab;

    [SerializeField] private Sprite inventoryBackground;
    [SerializeField] private Sprite documentBackgroud;
    [SerializeField] private Sprite mapBackground;

    [SerializeField] private DocumentSO mapSO;
    [SerializeField] private GameObject documentInspector;


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

        if (tabName == Tabs.Inventory)
        {
            documentTexts.SetActive(false);
            slots.SetActive(false);
            DocumentImageManager.HideImage();
            ShowInventory();
            return;
        }       

        if (tabName == Tabs.Documents)
        {
            documentTexts.SetActive(false);
            slots.SetActive(false);
            DocumentImageManager.HideImage();
            ShowDocuments();
            return;
        }

        if (tabName == Tabs.Map)
        {
            ShowMap();
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

    private void ShowMap()
    {
        if (mapSO == null) return;

        Instantiate(documentInspector);
        DocumentInspector.setDocument(mapSO);
    }

    // these funcs are called by their respective buttons
    // did this cuz you can't directly pass a enum as a parameter on a button function
    public void InventoryButtonClick() => ButtonClick(Tabs.Inventory);
    public void DocumentsButtonClick() => ButtonClick(Tabs.Documents);
    public void MapButtonClick() => ButtonClick(Tabs.Map);
}
