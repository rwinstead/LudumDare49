using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteManager : MonoBehaviour
{

    public int barrelCount = 0;
    public int barrelGoal = 0;

    public GameObject barrelPrefab;
    public GameObject barrelSpawnLoc;
    
    public static Action evt_endWasteTask;

    void Start()
    {
        GameManager.evt_beginWasteTask += BeginWasteTask;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Barrel"))
        {
            Destroy(collision.gameObject);
            barrelCount--;
            if(barrelCount == 0)
            {
                evt_endWasteTask?.Invoke();
            }
        }
    }

    public void BeginWasteTask()
    {
        Instantiate<GameObject>(barrelPrefab, barrelSpawnLoc.transform.position, Quaternion.identity);
        barrelCount++;
    }
}
