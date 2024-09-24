using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] InventoryController inventoryController;
    
    [SerializeField] List<GameObject> spawns = new List<GameObject>();

    public static int spawnIndex = 0; // used to match where the player spawns with which door the player entered

    private void Start()
    {
        StartCoroutine(SpawnPlayer());
        //PlayerVars.spawnPosition = spawns[spawnIndex].transform.position;
    }

    public void LoadScene(string sceneName, int doorCode)
    {
        // saves the inventory data to PlayerVars
        if (inventoryController != null) { inventoryController.SaveInventory(); }       

        spawnIndex = doorCode; // used to match where the player spawns with which door the player entered
        SceneManager.LoadScene(sceneName);

        // PlayerVars then loads inventory data on start
    }

    IEnumerator SpawnPlayer()
    {
        // gets the player object and updates its position to match with the door
        // throws an error if fails to find it
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerVars.BlockMovement(true);
            Debug.Log($"Sending player to door of index {spawnIndex}");
            Debug.Log($"Going to: {spawns[spawnIndex].name}");
            Debug.Log($"Its global position is {spawns[spawnIndex].transform.position}");
            player.SendMessage("ForceUpdatePosition", spawns[spawnIndex].transform.position);
        }
        else
        {
            Debug.LogError("Player could not be found by using the tag 'Player'");
        }
        yield return null;
    }
}
