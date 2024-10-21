using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{


    Animator animator;
    int isWalkingForwardHash;
    int isWalkingBackwardsHash;

    private void OnEnable()
    {
        PlayerVars.onPlayerRestrained += FreezeAnimation;
        PlayerVars.onPlayerFreed += ResumeAnimation;
    }

    private void OnDisable()
    {
        PlayerVars.onPlayerRestrained -= FreezeAnimation;
        PlayerVars.onPlayerFreed -= ResumeAnimation;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingForwardHash = Animator.StringToHash("isWalkingForward");
        isWalkingBackwardsHash = Animator.StringToHash("isWalkingBackwards");
    }

    private void Update()
    {
        bool isWalkingForward = animator.GetBool(isWalkingForwardHash);
        bool isWalkingBackwards = animator.GetBool(isWalkingBackwardsHash);

        bool pressingForward = Input.GetKey("w");
        bool pressingBackward = Input.GetKey("s");

        if (!isWalkingForward && pressingForward && !PlayerVars.playerBlocked)
        {
            animator.SetBool(isWalkingForwardHash, true);
            animator.SetFloat("Multiplier", 1);
        }
        if (isWalkingForward && !pressingForward)
        {
            animator.SetBool(isWalkingForwardHash, false);
            animator.SetFloat("Multiplier", 1);
        }

        // Walking backward
        if (!isWalkingBackwards && pressingBackward && !PlayerVars.playerBlocked)
        {
            animator.SetBool(isWalkingBackwardsHash, true);
            animator.SetFloat("Multiplier", -1);
        }
        if (isWalkingBackwards && !pressingBackward)
        {
            animator.SetBool(isWalkingBackwardsHash, false);
            animator.SetFloat("Multiplier", -1);
        }
    }

    // instead of returning to idle, I could freeze
    private void FreezeAnimation()
    {
        if (animator != null)
        {
            animator.speed = 0;
        }       
    }

    private void ResumeAnimation()
    {
        if (animator != null)
        {
            animator.speed = 1;
        }
    }
}
