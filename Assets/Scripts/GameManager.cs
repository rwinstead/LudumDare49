using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int radiation = 20;
    public TextMeshPro radiationText;
    public RotateGaugeNeedle radiationNeedle;

    public int temperature = 500;
    public TextMeshPro temperatureText;
    public RotateGaugeNeedle temperatureNeedle;

    public int tempNoiseFactor = 5;
    public int radNoiseFactor = 1;

    public int waterflow = 1000;
    public TextMeshPro waterflowText;

    public LeverManager.leverState leverstate = LeverManager.leverState.Middle;

    public bool shieldsActive = false;

    public GameObject NavTablet;
    public Camera mainCamera;

    public enum Room { ControlRoom, ReactorRoom, CoolantSystem, WasteDisposal, UraniumStorage, Mainframe }

    public Room playerCurrentRoom = Room.ControlRoom;

    public bool ReactorTaskActive = false;
    public bool CoolantTaskActive = false;
    public bool WasteTaskActive = false;
    public bool StorageTaskActive = false;
    public bool MainframeTaskActive = false;

    public GameObject cameraAnchor_ControlRoom;
    public GameObject cameraAnchor_ReactorRoom;
    public GameObject cameraAnchor_CoolantSystem;
    public GameObject cameraAnchor_WasteDisposal;
    public GameObject cameraAnchor_UraniumStorage;
    public GameObject cameraAnchor_Mainframe;


    public static Action evt_beginMainframeTask;
    public static Action evt_beginCoolantTask;
    public static Action evt_beginWasteTask;
    public static Action evt_beginReactorTask;

    public GameObject TerminalManagerHolder;
    private TerminalManager Terminal;

    private bool reducingTemp = false;
    private float reductionFactor = 0f;

    public Transform outputBar;
    public Transform demandBar;

    public int output;
    public int demand;

    public int wasteBuildupTime = 25;
    public int wasteBarrels = 0;
    public int DemandChangeTime = 15;
    public int demandIndex = 0;
    public GameObject winScreen;

    public int taskTime = 35;

    public SpriteRenderer outputBarSprite;

    public List<int> demandList = new List<int>();

    public List<string> TaskList = new List<string>();

    public UnityEvent spawnWasteBarrel;

    public bool shieldsUsed = false;

    public int maxTempOffset = 20;
    public int maxRadOffset = 10;
    public int maxDemandOffset = 100;
    public int maxOutputOffset = 100;

    private int tempOffset;
    private int radOffset;
    private int demandOffset;
    private int outputOffset;

    private void Awake()
    {
        UnityEngine.Random.seed = System.DateTime.Now.Millisecond;

        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

        Terminal = TerminalManagerHolder.GetComponent<TerminalManager>();

        PipeManager.evt_endCoolantTask += EndCoolantTask;
        MainframeTaskManager.evt_endMainframeTask += EndMainframeTask;
        WasteManager.evt_endWasteTask += EndWasteTask;
        ReactorManager.evt_endReactorTask += EndReactorTask;

        demand = demandList[0];

        StartCoroutine("triggerDataNoise");
    }

    private void OnDestroy()
    {
        PipeManager.evt_endCoolantTask -= EndCoolantTask;
        MainframeTaskManager.evt_endMainframeTask -= EndMainframeTask;
        WasteManager.evt_endWasteTask -= EndWasteTask;
        ReactorManager.evt_endReactorTask -= EndReactorTask;
    }

    public void TutorialCleared()
    {
        StartCoroutine("AccumulateWaste");
        StartCoroutine("DemandStart");
    }

    public void IncreaseWaterFlow()
    {
        waterflow += 100;
        waterflowText.text = waterflow + " m^3/s";
    }

    public void DecreaseWaterFlow()
    {
        if(waterflow >100)
        {
            waterflow -= 100;
            waterflowText.text = waterflow + " m^3/s";
        }
    }

    public void ActivateShields()
    {
        if(!shieldsUsed)
        {
            shieldsActive = true;
            StartCoroutine("StartShieldsTimer");
        }
    }

    public void OpenNavTablet()
    {
        Debug.Log("Open Nav Tablet!");
        NavTablet.SetActive(true);
        NavTablet.GetComponentInChildren<NavTabletHandler>().OnOpenTablet();

    }

    public void SetRoom(Room newRoom)
    {

        Debug.Log("GM Setting Up For " + newRoom);

        if (newRoom == Room.ControlRoom)
        {
            mainCamera.transform.position = cameraAnchor_ControlRoom.transform.position;
        }
        if (newRoom == Room.ReactorRoom)
        {
            mainCamera.transform.position = cameraAnchor_ReactorRoom.transform.position;
        }
        if (newRoom == Room.CoolantSystem)
        {
            mainCamera.transform.position = cameraAnchor_CoolantSystem.transform.position;
        }
        if (newRoom == Room.WasteDisposal)
        {
            mainCamera.transform.position = cameraAnchor_WasteDisposal.transform.position;
        }
        if (newRoom == Room.UraniumStorage)
        {
            mainCamera.transform.position = cameraAnchor_UraniumStorage.transform.position;
        }
        if (newRoom == Room.Mainframe)
        {
            mainCamera.transform.position = cameraAnchor_Mainframe.transform.position;
        }

    }

    private void Update()
    {
        temperatureText.text = temperature + "°C";
        temperatureNeedle.num = temperature;

        radiationText.text = radiation + " rads";
        radiationNeedle.num = radiation;

        CalculateData();



        temperature += tempOffset;
        radiation += radOffset;
        if (radiation < 0) { radiation = 0; }

        if(wasteBarrels  >= 7 && !WasteTaskActive)
        {
            BeginWasteTask();
        }

        if(demand - output >= 199)
        {
            outputBarSprite.color = new Color32(255, 59, 47, 255);
        }

        else
        {
            outputBarSprite.color = new Color32(62, 248, 73, 255);
        }


    }

    public void BeginMainframeTask()
    {
        Debug.Log("Begin Mainframe");
        MainframeTaskActive = true;
        evt_beginMainframeTask?.Invoke();
    }

    public void EndMainframeTask()
    {
        Debug.Log("End Mainframe");
        MainframeTaskActive = false;
        
    }

    public void BeginCoolantTask()
    {
        Debug.Log("Begin Coolant");
        CoolantTaskActive = true;
        evt_beginCoolantTask?.Invoke();
    }

    public void EndCoolantTask()
    {
        Debug.Log("End Coolant");
        CoolantTaskActive = false;
    }

    public void BeginWasteTask()
    {
        Debug.Log("Begin Waste");
        WasteTaskActive = true;
        evt_beginWasteTask?.Invoke();
    }

    public void EndWasteTask()
    {
        Debug.Log("End Waste");
        WasteTaskActive = false;
    }

    public void BeginReactorTask()
    {
        Debug.Log("Begin Reactor");
        ReactorTaskActive = true;
        evt_beginReactorTask?.Invoke();
    }

    public void EndReactorTask()
    {
        Debug.Log("End Reactor");
        ReactorTaskActive = false;
    }

    public void CalculateData()
    {
        CalculateTemperature();
        CalculateRadiation();
        CalculateOutput();
        CaclulateFinalDemand();
    }

    public void CalculateTemperature()
    {
        //Target should be ~400 degrees C

        int baseline = (450 * 1000) / waterflow;

        if (leverstate == LeverManager.leverState.Top) baseline += 125;
        else if (leverstate == LeverManager.leverState.Middle) baseline += 0;
        else if (leverstate == LeverManager.leverState.Bottom) baseline -= 125;

        baseline = baseline - Mathf.FloorToInt(baseline * reductionFactor);

        temperature = baseline;

        if(!ReactorTaskActive)
        {
            reducingTemp = false;
            reductionFactor = 0;
            CancelInvoke();
        }

        if(ReactorTaskActive && !reducingTemp)
        {
            reducingTemp = true;
            InvokeRepeating("ReduceTemp", 0, 10);
        }

    }

    void ReduceTemp()
    {
        reductionFactor += .05f; 
    }

    public void CalculateRadiation()
    {
        int baseline = 1 + wasteBarrels * 40;

        if (leverstate == LeverManager.leverState.Top) baseline += 35;
        else if (leverstate == LeverManager.leverState.Middle) baseline += 0;
        else if (leverstate == LeverManager.leverState.Bottom) baseline -= 35;

        if (shieldsActive) baseline = 0;

        radiation = baseline;
    }

    public void CalculateOutput()
    {
        int baseline = (temperature * 5) / 2;
        output = baseline;

        int outputMin = 1;
        int outputMax = 2500;
        float maxBarScale = 0.62f;
        float minBarScale = .05f;

        if (output > 2500) output = 2500;

        output += outputOffset;

        float ratio = Remap(output, outputMin, outputMax, minBarScale, maxBarScale);

        outputBar.localScale = new Vector3(ratio, outputBar.localScale.y, outputBar.localScale.z);

    }

    void CaclulateFinalDemand()
    {
        int baseline = demand;

        int demandMin = 1;
        int demandMax = 2500;

        float maxBarScale = 0.62f;
        float minBarScale = .05f;


        float ratio = Remap((demand + demandOffset), demandMin, demandMax, minBarScale, maxBarScale);

        demandBar.localScale = new Vector3(ratio, outputBar.localScale.y, outputBar.localScale.z);
    }

    public float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }

    IEnumerator AccumulateWaste()
    {
        while (true)
        {
            yield return new WaitForSeconds(wasteBuildupTime);
            if (!(wasteBarrels >= 10))
            {
                wasteBarrels += 1;
                spawnWasteBarrel?.Invoke();
            }

        }
    }
    IEnumerator DemandStart()
    {
        while (true)
        {
            yield return new WaitForSeconds(DemandChangeTime);
            if (demandIndex != demandList.Count - 1)
            {
                demandIndex++;
                demand = demandList[demandIndex];
            }

            else
            {
                winScreen.SetActive(true);
            }

        }
    }

    IEnumerator FireTask()
    {
        while (true)
        {
            yield return new WaitForSeconds(taskTime);
            ChooseTask();
        }
    }

    void ChooseTask()
    { 
        if(TaskList.Count <= 0)
        {
            TaskList.Add("Mainframe");
            TaskList.Add("Reactor");
            TaskList.Add("Coolant");
        }

        int task = UnityEngine.Random.Range(0, TaskList.Count);

        if (TaskList[task] == "Mainframe") BeginMainframeTask();
        else if (TaskList[task] == "Reactor") BeginReactorTask();
        else if (TaskList[task] == "Coolant") BeginCoolantTask(); 

        TaskList.RemoveAt(task);
       
    }

    IEnumerator StartShieldsTimer()
    {
        shieldsUsed = true;
        yield return new WaitForSeconds(10);
        shieldsActive = false;
    }

    public void StartTasks()
    {
        BeginCoolantTask();
        TaskList.RemoveAt(2);
        StartCoroutine("TasksDelay");
    }

    public void DataNoise()
    {


        tempOffset += UnityEngine.Random.Range(-4, 5);
        if (tempOffset > maxTempOffset) { tempOffset = maxTempOffset; }
        if (tempOffset < -maxTempOffset) { tempOffset = -maxTempOffset; }

        radOffset += UnityEngine.Random.Range(-2, 3);
        if (radOffset > maxRadOffset) { radOffset = maxRadOffset; }
        if (radOffset < -maxRadOffset) { radOffset = -maxRadOffset; }

        demandOffset += UnityEngine.Random.Range(-25, 31);
        if (demandOffset > maxDemandOffset) { demandOffset = maxDemandOffset; }
        if (demandOffset < -maxDemandOffset) { demandOffset = -maxDemandOffset; }

        outputOffset += UnityEngine.Random.Range(-25, 31);
        if (outputOffset > maxOutputOffset) { outputOffset = maxOutputOffset; }
        if (outputOffset < -maxOutputOffset) { outputOffset = -maxOutputOffset; }

       //Debug.Log("temp "+tempOffset);
       //Debug.Log("rad "+radOffset);
       //Debug.Log("output "+outputOffset);
       //Debug.Log("demand "+demandOffset);

    }


    IEnumerator TasksDelay()
    {
        yield return new WaitForSeconds(10);
        StartCoroutine("FireTask");
    }

    IEnumerator triggerDataNoise()
    {
        while (true)
        {
            DataNoise();
            yield return new WaitForSeconds(UnityEngine.Random.Range(0,0.3f));
        }
    }

}