using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SchematicHandler : MonoBehaviour
{
    public GameObject btn_ControlRoom;
    public GameObject btn_ReactorRoom;
    public GameObject btn_Mainframe;
    public GameObject btn_UraniumStorage;
    public GameObject btn_CoolantSystem;
    public GameObject btn_WasteDisposal;
    
    public GameObject playerIcon;

    public string playerCurrentRoom;

    // Start is called before the first frame update
    void Start()
    {
        btn_ControlRoom.GetComponent<Button>().Select();
        playerCurrentRoom = "ControlRoom";

        //btn_CoolantSystem.GetComponent<Button>().interactable = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void btn_clicked(GameObject button)
    {
        print(button.name);

        if(playerCurrentRoom == button.name) 
        { 
            Debug.Log("Player already in that room."); 
        }
        else 
        { 
            Debug.Log("Player moving to " + button.name);
        
            if (playerCurrentRoom == "ControlRoom")
            {
                if (button.name == "ReactorRoom")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Reactor");
                }
                
                if (button.name == "CoolantSystem")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Coolant");
                }
                
                if (button.name == "WasteDisposal")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Waste");
                }
                
                if (button.name == "UraniumStorage")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Uranium");
                }

                if (button.name == "Mainframe")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Mainframe");
                }
            }

            if (playerCurrentRoom == "ReactorRoom")
            {
                if (button.name == "ControlRoom")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Control");
                }

                if (button.name == "CoolantSystem")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Coolant");
                }

                if (button.name == "WasteDisposal")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Waste");
                }

                if (button.name == "UraniumStorage")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Control");
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Uranium");
                }

                if (button.name == "Mainframe")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Control");
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Mainframe");
                }
            }

            if (playerCurrentRoom == "CoolantSystem")
            {
                if (button.name == "ControlRoom")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Control");
                }

                if (button.name == "ReactorRoom")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Reactor");
                }

                if (button.name == "WasteDisposal")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Waste");
                }

                if (button.name == "UraniumStorage")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Control");
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Uranium");
                }

                if (button.name == "Mainframe")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Control");
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Mainframe");
                }
            }

            if (playerCurrentRoom == "WasteDisposal")
            {
                if (button.name == "ControlRoom")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Control");
                }

                if (button.name == "ReactorRoom")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Reactor");
                }

                if (button.name == "CoolantSystem")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Coolant");
                }

                if (button.name == "UraniumStorage")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Control");
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Uranium");
                }

                if (button.name == "Mainframe")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Control");
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Mainframe");
                }
            }

            if (playerCurrentRoom == "UraniumStorage")
            {
                if (button.name == "ControlRoom")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Control");
                }

                if (button.name == "ReactorRoom")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Control");
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Reactor");
                }

                if (button.name == "CoolantSystem")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Control");
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Coolant");
                }

                if (button.name == "WasteDisposal")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Control");
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Waste");
                }

                if (button.name == "Mainframe")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Control");
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Mainframe");
                }
            }

            if (playerCurrentRoom == "Mainframe")
            {
                if (button.name == "ControlRoom")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Control");
                }

                if (button.name == "ReactorRoom")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Control");
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Reactor");
                }

                if (button.name == "CoolantSystem")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Control");
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Coolant");
                }

                if (button.name == "WasteDisposal")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Control");
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Waste");
                }

                if (button.name == "UraniumStorage")
                {
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Control");
                    playerIcon.GetComponent<Animator>().SetTrigger("Player_to_Uranium");
                }
            }

            playerCurrentRoom = button.name;
        }


    }

}
