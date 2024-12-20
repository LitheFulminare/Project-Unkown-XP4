using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] InventoryController inventoryController;
    
    [SerializeField] List<GameObject> spawns = new List<GameObject>();

    public static int spawnIndex = 0; // used to match where the player spawns with which door the player entered

    private void Start()
    {
        //StartCoroutine(SpawnPlayer());

        //Debug.Log($"Going to {spawns[spawnIndex].name}, coordinates {spawns[spawnIndex].transform.position}");
        if (spawns.Count == 0) return;
        PlayerVars.UpdateSpawnPosition(spawns[spawnIndex].transform.position);

        // gets the player reference and sends them to the right spawn position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("SceneChanger could not find a valid 'player' reference");
            return;
        }
        
        TankMovement playerMovementController = player.GetComponent<TankMovement>();
        CharacterController characterController = player.GetComponent<CharacterController>();

        if (playerMovementController == null)
        {
            Debug.LogError("SceneChanger could not find a valid 'TankControls' reference");
            return;
        }

        if (spawns == null)
        {
            Debug.LogError("Spawn List was not initialized in time and major bugs will occurr");
            return;
        }

        if (spawns[spawnIndex] == null)
        {
            Debug.LogError($"Spawn at index {spawnIndex} was not initialized in time and major bugs will occurr");
            return;
        }

        if (characterController == null)
        {
            Debug.LogError("SceneChanger could not find a valid 'CharacterController' reference");
            return;
        }

        characterController.enabled = false;
        player.transform.position = spawns[spawnIndex].transform.position;
        player.transform.rotation = spawns[spawnIndex].transform.rotation;
        characterController.enabled = true;
        PlayerVars.BlockPlayer(false);
    }

    public void LoadScene(string sceneName, int doorCode)
    {
        PlayerVars.BlockPlayer(true);

        // saves the inventory data on PlayerVars
        if (inventoryController != null) { inventoryController.SaveInventory(); }
        else { Debug.LogError("SceneChanger could not find 'inventoryController'"); }

        DataPersistency.savePersistencyList();

        spawnIndex = doorCode; // used to match where the player spawns with which door the player entered
        SceneManager.LoadScene(sceneName);

        // PlayerVars then loads inventory data on start
    }


    // not in use
    //IEnumerator SpawnPlayer()
    //{
    //    // gets the player object and updates its position to match with the door
    //    // throws an error if fails to find it
    //    GameObject player = GameObject.FindGameObjectWithTag("Player");
    //    if (player != null)
    //    {
    //        PlayerVars.BlockPlayer(true);
    //        Debug.Log($"Sending player to door of index {spawnIndex}");
    //        Debug.Log($"Going to: {spawns[spawnIndex].name}");
    //        Debug.Log($"Its global position is {spawns[spawnIndex].transform.position}");
    //        player.SendMessage("ForceUpdatePosition", spawns[spawnIndex].transform.position);
    //    }
    //    else
    //    {
    //        Debug.LogError("Player could not be found by using the tag 'Player'");
    //    }
    //    yield return null;
    //}
}
