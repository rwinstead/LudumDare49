using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskIndicator : MonoBehaviour
{
    public GameObject activeIndicator;
    public GameObject completedIndicator;

    public enum Task {Reactor,Coolant,Waste}

    public Task thisTask;
    
    // Start is called before the first frame update
    void Start()
    {

        if (thisTask == Task.Reactor)
        {
            GameManager.evt_beginReactorTask += taskActive;
            ReactorManager.evt_endReactorTask += taskCompleted;
        }
        if (thisTask == Task.Coolant)
        {
            GameManager.evt_beginCoolantTask += taskActive;
            PipeManager.evt_endCoolantTask += taskCompleted;
        }
        if (thisTask == Task.Waste)
        {
            GameManager.evt_beginWasteTask += taskActive;
            WasteManager.evt_endWasteTask += taskCompleted;
        }
        
    }

    private void OnDestroy()
    {
        if (thisTask == Task.Reactor)
        {
            GameManager.evt_beginReactorTask -= taskActive;
            ReactorManager.evt_endReactorTask -= taskCompleted;
        }
        if (thisTask == Task.Coolant)
        {
            GameManager.evt_beginCoolantTask -= taskActive;
            PipeManager.evt_endCoolantTask -= taskCompleted;
        }
        if (thisTask == Task.Waste)
        {
            GameManager.evt_beginWasteTask -= taskActive;
            WasteManager.evt_endWasteTask -= taskCompleted;
        }
    }

    // Update is called once per frame

    public void taskActive()
    {
        activeIndicator.SetActive(true);
        completedIndicator.SetActive(false);
    }
    public void taskCompleted()
    {
        activeIndicator.SetActive(false);
        completedIndicator.SetActive(true);
    }


}
