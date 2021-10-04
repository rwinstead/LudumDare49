using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public GameObject goodPipe;
    public GameObject badPipe;

    public GameObject goodPipe_spawnPos;
    public GameObject badPipe_spawnPos;

    public bool isPipeBroken = false;
    
    public static Action evt_endCoolantTask;

    void Start()
    {
        GameManager.evt_beginCoolantTask += BeginCoolantTask;


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Pipe"))
        {
            if (Time.time > 5f)
            {
                Debug.Log("pipe is here");
                isPipeBroken = false;
                evt_endCoolantTask?.Invoke();
            }
        }
    }

    public void BeginCoolantTask()
    {
        isPipeBroken = true;

        goodPipe.transform.position = goodPipe_spawnPos.transform.position;
        badPipe.transform.position = badPipe_spawnPos.transform.position;

    }


}
