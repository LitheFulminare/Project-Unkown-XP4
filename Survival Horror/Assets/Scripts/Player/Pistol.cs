using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class Pistol : MonoBehaviour
{
    private static int _maxAmmo = 6; // this probably won't change throughout the game
    private static int _bulletsLoaded = 6;

    public InventoryController inventoryController;

    [SerializeField] InputActionReference fire;
    [SerializeField] InputActionReference reload;

    private void OnEnable()
    {
        fire.action.started += Fire;
        reload.action.started += Reload;
    }

    private void OnDisable()
    {
        fire.action.started -= Fire;
        reload.action.started -= Reload;
    }

    private void Start()
    {
        inventoryController = GameObject.FindAnyObjectByType<InventoryController>();
        if (inventoryController == null) { Debug.LogError("'Pistol.cs' could not find 'Inventory Controller'"); }
    }

    // called when the player presses the Fire action
    // now the player only fires if they can move
    // should also add a "isAiming"
    private void Fire(InputAction.CallbackContext obj)
    {
        if (!PlayerVars.isMovementBlocked)
        {
            if (_bulletsLoaded > 0)
            {
                _bulletsLoaded--;
                // do fire stuff here
                Debug.Log($"Player Fired, {_bulletsLoaded} left in the magazine");
            }

            else
            {
                // play empty chamebr sound
                Debug.Log("No bullets left");
            }
        }       
    }

    // called when the player presses the Reload action
    private void Reload(InputAction.CallbackContext obj)
    {
        if (!PlayerVars.isMovementBlocked)
        {
            // checks if the player has ammo
            if (inventoryController.CheckIfPlayerHasItem(Items.pistolAmmo))
            {
                int _ammoNeeded = _maxAmmo - _bulletsLoaded;

                if (_ammoNeeded != 0)
                {
                    Debug.Log($"Ammo needed to fully reload: {_ammoNeeded}");
                    _bulletsLoaded += inventoryController.RetrieveItem(Items.pistolAmmo, _ammoNeeded);
                    Debug.Log($"Bullets loaded after reloading {_bulletsLoaded}");
                }
                else
                {
                    Debug.Log($"Magazine is already full");
                }
            }
            else
            {
                // either do nothing or the protagonist tried to reach something in the pocket and finds nothing
                Debug.Log("Player does not have pistol ammo in the inventory");
            }
        }            
    }

    public static int GetLoadedBullets() { return _bulletsLoaded; }
}
