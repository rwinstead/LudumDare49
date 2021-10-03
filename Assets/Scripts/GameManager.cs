using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public TextMeshPro terminalText;
    public TextMeshPro terminalText_hacked;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

        PipeManager.evt_endCoolantTask += EndCoolantTask;
        MainframeTaskManager.evt_endMainframeTask += EndMainframeTask;
        WasteManager.evt_endWasteTask += EndWasteTask;
        ReactorManager.evt_endReactorTask += EndReactorTask;

    }

    private void Start()
    {
        terminalText.ForceMeshUpdate();
        StartCoroutine(typewriter(terminalText,0.03f));
    }

    public void IncreaseWaterFlow()
    {
        waterflow += 100;
        waterflowText.text = waterflow + " m^3/s";
    }

    public void DecreaseWaterFlow()
    {
        waterflow -= 100;
        waterflowText.text = waterflow + " m^3/s";
    }

    public void ActivateShields()
    {
        shieldsActive = true;
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

        //temperature = Mathf.RoundToInt(temperature + (tempNoiseFactor * Mathf.PerlinNoise(Time.time * 8f, -10.0f)) - (tempNoiseFactor / 2));
        //radiation = Mathf.RoundToInt(radiation + (radNoiseFactor * Mathf.PerlinNoise(Time.time * 8f, 12.0f)) - (radNoiseFactor / 2));
        if (radiation < 0) { radiation = 0; }
    }

    public void BeginMainframeTask()
    {
        Debug.Log("Begin Mainframe");
        MainframeTaskActive = true;
        evt_beginMainframeTask?.Invoke();
        terminalText.gameObject.SetActive(false);
        terminalText_hacked.gameObject.SetActive(true);
        StartCoroutine(typewriter(terminalText_hacked, 0.005f,100));
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
        StartCoroutine(typewriter(terminalText, 0.03f));
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
    }

    public void CalculateTemperature()
    {
        //Target should be ~400 degrees C

        int baseline = (400 + 1000) / waterflow;

        if (leverstate == LeverManager.leverState.Top) baseline += 125;
        else if (leverstate == LeverManager.leverState.Middle) baseline += 0;
        else if (leverstate == LeverManager.leverState.Bottom) baseline -= 125;

        temperature = baseline;

    }

    public void CalculateRadiation()
    {

    }

    public void CalculateOutput()
    {

    }

    IEnumerator typewriter(TextMeshPro tmpText, float speed = 0.05f, int startIndex = 0)
    {

        tmpText.ForceMeshUpdate();

        int totalVisibleCharacters = tmpText.textInfo.characterCount;
        int counter = startIndex;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);

            tmpText.maxVisibleCharacters = visibleCount;

            counter += 1;

            if(visibleCount >= totalVisibleCharacters)
            {
                break;
            }

            yield return new WaitForSeconds(speed);
        }
    }


}