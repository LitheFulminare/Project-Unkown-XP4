using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Animator animator;
    int isWalkingForwardHash;
    //int isWalkingBackWardsHash;

    private void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingForwardHash = Animator.StringToHash("isWalkingForward");
    }

    private void Update()
    {
        bool isWalkingForward = animator.GetBool(isWalkingForwardHash);
        //bool isWalkingBackwards = animator.GetBool(isWalkingBackWardsHash);

        bool pressingForward = Input.GetKey("w");
        bool pressingBackward = Input.GetKey("s");

        if (!isWalkingForward && pressingForward)
        {
            animator.SetBool(isWalkingForwardHash, true);
            animator.speed = 1;
        }

        if (!isWalkingForward && pressingBackward)
        {
            animator.SetBool(isWalkingForwardHash, true);
            animator.speed = -1;
        }
        
        if (isWalkingForward && !pressingForward)
        {
            animator.SetBool(isWalkingForwardHash, false);
            animator.speed = 1;
        }
    }
}
