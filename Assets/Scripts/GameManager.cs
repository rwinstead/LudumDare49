using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int radiation = 0;
    public TextMeshPro radiationText;
    public RotateGaugeNeedle radiationNeedle;

    public int temperature = 0;
    public TextMeshPro temperatureText;
    public RotateGaugeNeedle temperatureNeedle;

    public int waterflow = 1000;
    public TextMeshPro waterflowText;

    public LeverManager.leverState leverstate = LeverManager.leverState.Middle;

    public bool shieldsActive = false;

    public GameObject NavTablet;
    public Camera mainCamera;

    public enum Room {ControlRoom, ReactorRoom, CoolantSystem, WasteDisposal, UraniumStorage, Mainframe}
    
    public Room playerCurrentRoom = Room.ControlRoom;

    public bool Reactor_alarming = false;
    public bool Coolant_alarming = false;
    public bool Waste_alarming = false;
    public bool Storage_alarming = false;
    public bool Mainframe_alarming = false;

    public GameObject cameraAnchor_ControlRoom;
    public GameObject cameraAnchor_ReactorRoom;
    public GameObject cameraAnchor_CoolantSystem;
    public GameObject cameraAnchor_WasteDisposal;
    public GameObject cameraAnchor_UraniumStorage;
    public GameObject cameraAnchor_Mainframe;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
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
    }

}
