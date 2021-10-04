using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UraniumStorageManager : MonoBehaviour
{
    public int unitsPickedUp = 0;

    public GameObject rod1;
    public GameObject rod2;
    public GameObject rod3;

    void Start()
    {
        GameManager.evt_beginReactorTask += BeginReactorTask;
    }

    private void OnDestroy()
    {
        GameManager.evt_beginReactorTask -= BeginReactorTask;
    }

    public void PickedUpUnit()
    {
        unitsPickedUp++;
    }

    public void BeginReactorTask()
    {
        rod1.SetActive(true);
        rod2.SetActive(true);
        rod3.SetActive(true);
    }
}
