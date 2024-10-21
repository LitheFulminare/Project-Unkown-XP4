using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Animator animator;
    int isWalkingForwardHash;
    int isWalkingBackwardsHash;

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

        if (!isWalkingForward && pressingForward)
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
        if (!isWalkingBackwards && pressingBackward)
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
}
