using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Animator animator;

    int isWalkingForwardHash;
    int isWalkingBackwardsHash;
    int isTurningRightHash;
    int isTurningLeftHash;

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

        isTurningRightHash = Animator.StringToHash("isTurningRight");
        isTurningLeftHash = Animator.StringToHash("isTurningLeft");
    }

    private void Update()
    {
        // parameters for the animations
        bool isWalkingForward = animator.GetBool(isWalkingForwardHash);
        bool isWalkingBackwards = animator.GetBool(isWalkingBackwardsHash);
        bool isTurningRight = animator.GetBool(isTurningRightHash);
        bool isTurningLeft = animator.GetBool(isTurningLeftHash);

        // keyboard keys
        bool pressingForward = Input.GetKey(KeyCode.W);
        bool pressingBackward = Input.GetKey(KeyCode.S);
        bool pressingLeft = Input.GetKey(KeyCode.A);
        bool pressingRight = Input.GetKey(KeyCode.D);

        // walk forward
        if (!isWalkingForward && pressingForward)
        {
            animator.SetBool(isWalkingForwardHash, true);
        }
        if (isWalkingForward && !pressingForward)
        {
            animator.SetBool(isWalkingForwardHash, false);
        }

        // walking backwards
        if (!isWalkingBackwards && pressingBackward)
        {
            animator.SetBool(isWalkingBackwardsHash, true);
        }
        if (isWalkingBackwards && !pressingBackward)
        {
            animator.SetBool(isWalkingBackwardsHash, false);
        }

        // turn right
        if (!isTurningRight && pressingRight)
        {
            animator.SetBool(isTurningRightHash, true);
        }
        if (isTurningRight && !pressingRight || isWalkingForward)
        {
            animator.SetBool(isTurningRightHash, false);
        }

        // turn left
        if (!isTurningLeft && pressingLeft)
        {
            animator.SetBool(isTurningLeftHash, true);
        }
        if (isTurningLeft && !pressingLeft || isWalkingForward)
        {
            animator.SetBool(isTurningLeftHash, false);
        }
    }

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
