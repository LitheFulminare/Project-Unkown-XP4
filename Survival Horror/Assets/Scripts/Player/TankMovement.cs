using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class TankMovement : MonoBehaviour
{
    // https://www.youtube.com/watch?v=OEMk8xVDk2I

    private CharacterController controller;

    public float speed = 250;
    public float turnSpeed = 180f;

    private float sprintMultiplier = 1.7f;

    private float sprintingSpeed;

    private EventInstance playerFootsteps;

    void Start()
    {
        playerFootsteps = AudioManager.instance.CreateInstance(FMODEvents.instance.playerFootsteps);

        controller = GetComponent<CharacterController>();

        sprintingSpeed = speed * sprintMultiplier;
    }

    void Update()
    {
        // handles the movement
        // currently this can be stopped by UI elements
        
        bool isSprinting = false;

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

            controller.Move(moveDir * Time.deltaTime - Vector3.up * 0.1f);
        }

        UpdateAudio();
    }

    private void UpdateAudio()
    {
        if (IsPressingWalkButton() && !PlayerVars.playerBlocked)
        {
            PLAYBACK_STATE playbackState;
            playerFootsteps.getPlaybackState(out playbackState);

            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
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
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
    }
}
