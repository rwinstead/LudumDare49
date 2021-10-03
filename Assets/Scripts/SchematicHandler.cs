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
    public string playerTargetRoom;

    public bool transitioning;
    private Animator playerAnimator;


    // Start is called before the first frame update
    void Start()
    {
        btn_ControlRoom.GetComponent<Button>().Select();
        playerAnimator = playerIcon.GetComponent<Animator>();
        playerCurrentRoom = "ControlRoom";
        playerTargetRoom = "";

        //btn_CoolantSystem.GetComponent<Button>().interactable = false;

    }

    void Update()
    {
        if (transitioning)
        {
            if (playerTargetRoom == "ControlRoom")
            {
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    Debug.Log("Switching to Control Room");
                    transitioning = false;
                }
            }
            if (playerTargetRoom == "ReactorRoom")
            {
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle_Reactor"))
                {
                    Debug.Log("Switching to Reactor");
                    transitioning = false;
                }
            }
            if (playerTargetRoom == "CoolantSystem")
            {
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle_Coolant"))
                {
                    Debug.Log("Switching to Coolant System");
                    transitioning = false;
                }
            }
            if (playerTargetRoom == "WasteDisposal")
            {
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle_Waste"))
                {
                    Debug.Log("Switching to Waste Disposal Room");
                    transitioning = false;
                }
            }
            if (playerTargetRoom == "UraniumStorage")
            {
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle_Uranium"))
                {
                    Debug.Log("Switching to Uranium Storage");
                    transitioning = false;
                }
            }
            if (playerTargetRoom == "Mainframe")
            {
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle_Mainframe"))
                {
                    Debug.Log("Switching to Mainframe");
                    transitioning = false;
                }
            }
        }
    }



    public void btn_clicked(GameObject button)
    {
        transitioning = true;

       btn_ControlRoom.GetComponent<Button>().interactable = false;
       btn_ReactorRoom.GetComponent<Button>().interactable = false;
       btn_Mainframe.GetComponent<Button>().interactable = false;
       btn_UraniumStorage.GetComponent<Button>().interactable = false;
       btn_CoolantSystem.GetComponent<Button>().interactable = false;
       btn_WasteDisposal.GetComponent<Button>().interactable = false;

        button.GetComponent<Button>().interactable = true;
        button.GetComponent<Button>().Select();


        if (playerCurrentRoom == button.name) 
        { 
            Debug.Log("Player already in that room."); 
        }
        else 
        {
            playerTargetRoom = button.name;
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

    public void OnOpenSchematic()
    {
        // playerCurrentRoom = "";

        btn_ControlRoom.GetComponent<Button>().interactable = true;
        btn_ReactorRoom.GetComponent<Button>().interactable = true;
        btn_Mainframe.GetComponent<Button>().interactable = true;
        btn_UraniumStorage.GetComponent<Button>().interactable = true;
        btn_CoolantSystem.GetComponent<Button>().interactable = true;
        btn_WasteDisposal.GetComponent<Button>().interactable = true;

    }
}
