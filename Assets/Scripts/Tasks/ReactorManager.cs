using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReactorManager : MonoBehaviour
{

    public GameObject rod1;
    public GameObject rod2;
    public GameObject rod3;

    public UraniumStorageManager storageManager;


    public TextMeshPro activeRodsText = null;

    public int rodCount = 0;

    public SpriteRenderer rend;

    public SpriteRenderer reactorRend;

    public List<Sprite> reactorSymbols = new List<Sprite>();
    public List<Sprite> reactors = new List<Sprite>();

    public List<Transform> originalRodLocations = new List<Transform>();

    private bool taskActive = false;

    public static Action evt_endReactorTask;

    void Start()
    {
        GameManager.evt_beginReactorTask += BeginReactorTask;
    }

    private void OnDestroy()
    {
        GameManager.evt_beginReactorTask -= BeginReactorTask;
    }

    public void IncreaseRodCount()
    {
        rodCount++;
        activeRodsText.text = "Active rods: " + rodCount;
    }

    public void DecreaseRodCount()
    {
        rodCount--;
        activeRodsText.text = "Active rods: " + rodCount;
    }

    private void Update()
    {
        reactorRend.sprite = reactors[0];

        if (storageManager.unitsPickedUp > 0)
        {
            rod1.SetActive(true);
        }

        if (storageManager.unitsPickedUp > 1)
        {
            rod2.SetActive(true);
        }

        if (storageManager.unitsPickedUp > 2)
        {
            rod3.SetActive(true);
        }

        if(rodCount == 0)
        {
            rend.sprite = reactorSymbols[3];
        }

        else if(rodCount == 1)
        {
            rend.sprite = reactorSymbols[0];
        }

        else if (rodCount == 2)
        {
            rend.sprite = reactorSymbols[1];
        }

        else if (rodCount == 3)
        {
            rend.sprite = reactorSymbols[2];
            reactorRend.sprite = reactors[1];
            if (taskActive)
            {
                evt_endReactorTask?.Invoke();
                taskActive = false;
            }
            
        }

    }

    public void BeginReactorTask()
    {
        rod1.transform.position = originalRodLocations[0].position;
        rod2.transform.position = originalRodLocations[1].position;
        rod3.transform.position = originalRodLocations[2].position;
        activeRodsText.text = "Active rods: 0";
        rod1.SetActive(false);
        rod2.SetActive(false);
        rod3.SetActive(false);
        taskActive = true;
    }


}
