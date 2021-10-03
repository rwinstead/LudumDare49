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


    public string playerCurrentRoom = "Control Room";

    public bool Reactor_alarming = false;
    public bool Coolant_alarming = false;
    public bool Waste_alarming = false;
    public bool Storage_alarming = false;
    public bool Mainframe_alarming = false;


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

    private void Update()
    {
        temperatureText.text = temperature + "°C";
        temperatureNeedle.num = temperature;

        radiationText.text = radiation + " rads";
        radiationNeedle.num = radiation;
    }

}
