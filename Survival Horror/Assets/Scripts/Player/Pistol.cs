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

    private void Fire(InputAction.CallbackContext obj)
    {
        if (_bulletsLoaded > 0)
        {
            _bulletsLoaded--;
            Debug.Log($"Player Fired, {_bulletsLoaded} left in the magazine");
        }
        else
        {
            Debug.Log("No bullets left");
        }
    }

    private void Reload(InputAction.CallbackContext obj)
    {
        int _ammoNeeded = _maxAmmo - _bulletsLoaded;

        if (_ammoNeeded != 0)
        {
            Debug.Log($"Ammo needed to fully reload: {_ammoNeeded}");
            _bulletsLoaded = _maxAmmo;
        }
        else
        {
            Debug.Log($"Magazine is already full");
        }
    }
}
