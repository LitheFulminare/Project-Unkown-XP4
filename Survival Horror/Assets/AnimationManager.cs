using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;

    private void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
    }

    private void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool pressingForward = Input.GetKey("w");

        if (!isWalking && pressingForward)
        {
            animator.SetBool(isWalkingHash, true);
        }
        
        if (isWalking && !pressingForward)
        {
            animator.SetBool(isWalkingHash, false);
        }
    }
}
