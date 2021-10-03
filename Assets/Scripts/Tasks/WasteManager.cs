using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteManager : MonoBehaviour
{

    public int barrelCount = 0;

    public int barrelGoal = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Barrel"))
        {
            Destroy(collision.gameObject);
            barrelCount++;
        }
    }
}
