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

    public List<GameObject> barrels = new List<GameObject>();

    private int barrelIndex = 0;

    void Start()
    {
        GameManager.evt_beginWasteTask += BeginWasteTask;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Barrel"))
        {
            collision.gameObject.GetComponent<ResetBarrelPos>().resetPos();
            collision.gameObject.SetActive(false);
            barrelCount--;
            if(barrelCount == 0)
            {
                evt_endWasteTask?.Invoke();
            }
        }
    }

    public void spawnBarrel()
    {   if (barrelIndex < barrels.Count)
        {
            if(barrels[barrelIndex].activeSelf)
            {
                barrelIndex++;
                spawnBarrel();
            }

            else
            {
                barrels[barrelIndex].SetActive(true);
                barrelIndex++;
            }

            if (barrelIndex > 9) barrelIndex = 0;
               
        }
    }

    public void BeginWasteTask()
    {
        //Instantiate<GameObject>(barrelPrefab, barrelSpawnLoc.transform.position, Quaternion.identity);
        //barrelCount++;
    }
}
