using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] InventoryController inventoryController;  

    public void LoadScene(string sceneName)
    {
        // saves the inventory data to PlayerVars
        inventoryController.SaveInventory();

        SceneManager.LoadScene(sceneName);

        // PlayerVars then loads inventory data on start
    }
}
