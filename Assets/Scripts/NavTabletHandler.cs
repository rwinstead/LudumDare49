using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavTabletHandler : MonoBehaviour
{
    public GameObject gameManagerHolder;
    public GameManager gManager;

    public GameObject btn_ControlRoom;
    public GameObject btn_ReactorRoom;
    public GameObject btn_Mainframe;
    public GameObject btn_UraniumStorage;
    public GameObject btn_CoolantSystem;
    public GameObject btn_WasteDisposal;

    public GameObject alm_ReactorRoom;
    public GameObject alm_CoolantSystem;
    public GameObject alm_WasteDisposal;
    public GameObject alm_UraniumStorage;
    public GameObject alm_Mainframe;

    public GameObject StorageManager;

    public GameObject playerIcon;

    public string playerCurrentRoom;
    public string playerTargetRoom;

    public bool transitioning;
    private Animator playerAnimator;

    private string lastAnimationState;



    // Start is called before the first frame update
    void Start()
    {
        btn_ControlRoom.GetComponent<Button>().Select();
        playerAnimator = playerIcon.GetComponent<Animator>();
        playerCurrentRoom = "ControlRoom";
        playerTargetRoom = "";
        gManager = gameManagerHolder.GetComponent<GameManager>();
        playerAnimator.keepAnimatorControllerStateOnDisable = true;

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
                    gManager.SetRoom(GameManager.Room.ControlRoom);
                    transitioning = false;
                }
            }
            if (playerTargetRoom == "ReactorRoom")
            {
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle_Reactor"))
                {
                    Debug.Log("Switching to Reactor");
                    gManager.SetRoom(GameManager.Room.ReactorRoom);
                    transitioning = false;
                }
            }
            if (playerTargetRoom == "CoolantSystem")
            {
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle_Coolant"))
                {
                    Debug.Log("Switching to Coolant System");
                    gManager.SetRoom(GameManager.Room.CoolantSystem);
                    transitioning = false;
                }
            }
            if (playerTargetRoom == "WasteDisposal")
            {
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle_Waste"))
                {
                    Debug.Log("Switching to Waste Disposal Room");
                    gManager.SetRoom(GameManager.Room.WasteDisposal);
                    transitioning = false;
                }
            }
            if (playerTargetRoom == "UraniumStorage")
            {
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle_Uranium"))
                {
                    Debug.Log("Switching to Uranium Storage");
                    gManager.SetRoom(GameManager.Room.UraniumStorage);
                    transitioning = false;
                }
            }
            if (playerTargetRoom == "Mainframe")
            {
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle_Mainframe"))
                {
                    Debug.Log("Switching to Mainframe");
                    gManager.SetRoom(GameManager.Room.Mainframe);
                    transitioning = false;
                }
            }

            if (!transitioning)
            {
                transform.parent.gameObject.SetActive(false);
            }
        
        }


    }

    public void btn_clicked(GameObject button)
    {
        playerTargetRoom = button.name;
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

    public void OnOpenTablet()
    {
        gManager = gameManagerHolder.GetComponent<GameManager>();

        btn_ControlRoom.GetComponent<Button>().interactable = true;
        btn_ReactorRoom.GetComponent<Button>().interactable = true;
        btn_Mainframe.GetComponent<Button>().interactable = true;
        btn_UraniumStorage.GetComponent<Button>().interactable = true;
        btn_CoolantSystem.GetComponent<Button>().interactable = true;
        btn_WasteDisposal.GetComponent<Button>().interactable = true;

        Debug.Log("Reactor alarm "+gManager.playerCurrentRoom);

        

        if (gManager.ReactorTaskActive)
        {
            alm_ReactorRoom.GetComponent<Animator>().Play("Base Layer.Off");
            alm_UraniumStorage.GetComponent<Animator>().Play("Base Layer.Off");

            if (StorageManager.GetComponent<UraniumStorageManager>().unitsPickedUp > 2)
            {
                alm_ReactorRoom.GetComponent<Animator>().Play("Base Layer.Alarming");
            }
            else
            {
                alm_UraniumStorage.GetComponent<Animator>().Play("Base Layer.Alarming");
            }

        }
        else
        {
            alm_ReactorRoom.GetComponent<Animator>().Play("Base Layer.Off");
        }
        if (gManager.CoolantTaskActive)
        {
            alm_CoolantSystem.GetComponent<Animator>().Play("Base Layer.Alarming");
        }
        else
        {
            alm_CoolantSystem.GetComponent<Animator>().Play("Base Layer.Off");
        }
        if (gManager.WasteTaskActive)
        {
            alm_WasteDisposal.GetComponent<Animator>().Play("Base Layer.Alarming");
        }
        else
        {
            alm_WasteDisposal.GetComponent<Animator>().Play("Base Layer.Off");
        }
        if (gManager.StorageTaskActive)
        {
            alm_UraniumStorage.GetComponent<Animator>().Play("Base Layer.Alarming");
        }
        else
        {
            alm_UraniumStorage.GetComponent<Animator>().Play("Base Layer.Off");
        }
        if (gManager.MainframeTaskActive)
        {
            alm_Mainframe.GetComponent<Animator>().Play("Base Layer.Alarming");
        }
        else
        {
            alm_Mainframe.GetComponent<Animator>().Play("Base Layer.Off");
        }


    }
}
