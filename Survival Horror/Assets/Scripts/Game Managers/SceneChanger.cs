using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] InventoryController inventoryController;
    [SerializeField] CollectableManager collectableManager;
    [SerializeField] PuzzleManager puzzleManager;
    
    [SerializeField] List<GameObject> spawns = new List<GameObject>();

    public static int spawnIndex = 0; // used to match where the player spawns with which door the player entered

    private void Start()
    {
        //StartCoroutine(SpawnPlayer());

        //Debug.Log($"Going to {spawns[spawnIndex].name}, coordinates {spawns[spawnIndex].transform.position}");
        PlayerVars.UpdateSpawnPosition(spawns[spawnIndex].transform.position);

        // gets the player reference and sends them to the right spawn position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            TankMovement playerMovementController = player.GetComponent<TankMovement>();
            CharacterController characterController = player.GetComponent<CharacterController>();
            if (playerMovementController != null)
            {
                if (spawns != null)
                {
                    //Debug.Log($"Spawns registered in the Spawn List: {spawns.Count}");

                    if (spawns[spawnIndex] != null)
                    {
                        //player.SendMessage("SpawnPlayer", spawns[spawnIndex].transform.position);
                        //playerMovementController.SpawnPlayer(spawns[spawnIndex].transform.position);
                       
                        if (characterController != null)
                        {
                            characterController.enabled = false;
                            player.transform.position = spawns[spawnIndex].transform.position;
                            characterController.enabled = true;
                            PlayerVars.BlockPlayer(false);
                        }
                        else
                        {
                            Debug.LogError("SceneChanger could not find a valid 'CharacterController' reference");
                        }                       
                    }
                    else
                    {
                        Debug.LogError($"Spawn at index {spawnIndex} was not initialized in time and major bugs will occurr");
                    }
                }
                else
                {
                    Debug.LogError("Spawn List was not initialized in time and major bugs will occurr");
                }
            }
            else
            {
                Debug.LogError("SceneChanger could not find a valid 'TankControls' reference");
            }
        }
        else
        {
            Debug.LogError("SceneChanger could not find a valid 'player' reference");
        }
    }

    public void LoadScene(string sceneName, int doorCode)
    {
        PlayerVars.BlockPlayer(true);

        // saves the inventory data on PlayerVars
        if (inventoryController != null) { inventoryController.SaveInventory(); }
        else { Debug.LogError("SceneChanger could not find 'inventoryController'"); }

        // saves destroyed items on WorldVars
        if (collectableManager != null) { collectableManager.SaveList(); }
        else { Debug.LogError("SceneChanger could not find 'collectableManager'"); }

        // saves completed puzzles on WorldVars
        if (puzzleManager != null) { puzzleManager.SaveList(); }
        else { Debug.LogError("SceneChanger could not find 'puzzleManager"); }

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
