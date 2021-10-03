using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBarrelPos : MonoBehaviour
{
    Vector3 spawnPos;
    private void OnEnable()
    {
        spawnPos = transform.position;
    }

    public void resetPos()
    {
        transform.position = spawnPos;
    }
}
