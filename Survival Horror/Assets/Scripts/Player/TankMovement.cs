using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class TankMovement : MonoBehaviour
{
    // https://www.youtube.com/watch?v=OEMk8xVDk2I

    private CharacterController characterController;

    public float speed = 250;
    public float turnSpeed = 180f;

    private float sprintMultiplier = 1.7f;

    private float sprintingSpeed;
    private bool isSprinting = false;

    private EventInstance playerFootsteps;


    private bool isThermalActive = false;
    private List<GameObject> thermalObjects = new List<GameObject>();

    void Start()
    {
        thermalObjects.AddRange(GameObject.FindGameObjectsWithTag("Thermal Effect"));
        foreach (GameObject obj in thermalObjects)
        {
            obj.SetActive(false);
        }

        playerFootsteps = AudioManager.instance.CreateInstance(FMODEvents.instance.playerFootsteps);

        characterController = GetComponent<CharacterController>();

        sprintingSpeed = speed * sprintMultiplier;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (GameObject obj in thermalObjects)
            {
                obj.SetActive(!isThermalActive);
            }

            isThermalActive = !isThermalActive;
        }

        UpdateMovement();

        UpdateAudio();
    }

    private void UpdateMovement()
    {
        isSprinting = false;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }

        if (!PlayerVars.playerBlocked)
        {
            float vel = speed;

            if (isSprinting) vel = sprintingSpeed;

            Vector3 moveDir;

            transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
            //moveDir = transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime;   

            moveDir = Input.GetAxis("Vertical") * vel * Time.deltaTime * transform.forward; // apparently this has better performance

            characterController.Move(moveDir * Time.deltaTime - Vector3.up * 0.1f);
        }
    }

    private void UpdateAudio()
    {
        // i find it better to check if the player is pressing a button then checking the character velocity
        if (IsPressingWalkButton() && !PlayerVars.playerBlocked)
        {
            if (isSprinting && !Input.GetKey(KeyCode.S))
            {
                playerFootsteps.setParameterByName("running", 1f);
            } 
            else playerFootsteps.setParameterByName("running", 0f);


            PLAYBACK_STATE playbackState;
            playerFootsteps.getPlaybackState(out playbackState);

            if (playbackState.Equals(PLAYBACK_STATE.STOPPED) || playbackState.Equals(PLAYBACK_STATE.STOPPING))
            {
                playerFootsteps.start();
            }
        }

        else
        {
            StopFootstepSound();
        }
    }

    public void StopFootstepSound()
    {
        playerFootsteps.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    private bool IsPressingWalkButton()
    {
        return 
            Input.GetKey(KeyCode.W) || 
            Input.GetKey(KeyCode.A) || 
            Input.GetKey(KeyCode.S) || 
            Input.GetKey(KeyCode.D);
    }
}
