using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public void UpdatePosition(Vector3 spawnPosition)
    {
        gameObject.transform.position = spawnPosition;
    }
}
