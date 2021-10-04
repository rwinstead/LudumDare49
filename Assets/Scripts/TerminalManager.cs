using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TerminalManager : MonoBehaviour
{
    public TextMeshPro terminalHeader;
    public TextMeshPro terminalText;
    public TextMeshPro terminalText_hacked;

    public GameObject monitorAlarm;

    public GameObject gameManagerHolder;
    private GameManager gManager = GameManager.instance;

    public int numAlerts;

    private bool tutorialFinished = false;
    private int tutorialScreen = 0;

    public GameObject navTablet;

    public CheckForLoss checker;

    private string[] HappyMessages = new string[] { "The bees are happy. :-)", "And all was right with the world", "Take a break, you deserve it.", "They should give you a raise.", "First you set yourself to rights. And then your house. And then your corner of the sky. And after that... Well, then she didn't rightly know what happened next.", "Have you tried turning off and on again?" };


    // Start is called before the first frame update
    void Start()
    {
        gManager = gameManagerHolder.GetComponent<GameManager>();
        updatePanel();

        GameManager.evt_beginReactorTask += updatePanel;
        GameManager.evt_beginCoolantTask += updatePanel;
        GameManager.evt_beginWasteTask += updatePanel;
        GameManager.evt_beginMainframeTask += BeginMainframeTask;

        ReactorManager.evt_endReactorTask += updatePanel;
        PipeManager.evt_endCoolantTask += updatePanel;
        WasteManager.evt_endWasteTask += updatePanel;
        MainframeTaskManager.evt_endMainframeTask += EndMainframeTask;

        navTablet.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnDestroy()
    {
        GameManager.evt_beginReactorTask -= updatePanel;
        GameManager.evt_beginCoolantTask -= updatePanel;
        GameManager.evt_beginWasteTask -= updatePanel;
        GameManager.evt_beginMainframeTask -= BeginMainframeTask;

        ReactorManager.evt_endReactorTask -= updatePanel;
        PipeManager.evt_endCoolantTask -= updatePanel;
        WasteManager.evt_endWasteTask -= updatePanel;
        MainframeTaskManager.evt_endMainframeTask -= EndMainframeTask;

        //tutorialScreen = -1;
    }

    // Update is called once per frame
    void Update()
    {

        //hacking
        //terminalText.gameObject.SetActive(false);
        //terminalText_hacked.gameObject.SetActive(true);
        //StartCoroutine(typewriter(terminalText_hacked, 0.005f, 100));

        //monitorAlarm.GetComponent<Animator>().Play("Base Layer.Alarming");
        //

    }

    private void OnMouseDown()
    {
        updatePanel();
    }

    public void BeginReactorTask()
    {
        updatePanel();
    }

    public void BeginCoolantTask()
    {
        updatePanel();
    }

    public void BeginWasteTask()
    {
        updatePanel();
    }

    public void BeginMainframeTask()
    {
        terminalText.gameObject.SetActive(false);
        terminalText_hacked.gameObject.SetActive(true);
        StartCoroutine(typewriter(terminalText_hacked, 0.005f, 100));
    }

    public void EndMainframeTask()
    {
        terminalText.gameObject.SetActive(true);
        terminalText_hacked.gameObject.SetActive(false);
        updatePanel();
    }

    public void showCredits()
    {
        StopAllCoroutines();
        string textBlock = " This game was built in 72 hours for Ludum Dare 49 in Oct 2021 \n\n Contributors:\n Ryan Winstead and Josh Todd\n\n Music is 'Together We Are Stronger' by Komiku under CC0\n Thanks for Playing!";
        terminalText.SetText(textBlock);
        StartCoroutine(typewriter(terminalText, 0, 0, false));
    }

    public void updatePanel()
    {
        StopAllCoroutines();
        numAlerts = 0;
        string textBlock = "";

        if (gManager.ReactorTaskActive) { numAlerts++; }
        if (gManager.CoolantTaskActive) { numAlerts++; }
        if (gManager.WasteTaskActive) { numAlerts++; }
        if (gManager.StorageTaskActive) { numAlerts++; }

        if (!tutorialFinished)
        {
            if (tutorialScreen == 0) { textBlock = "You have 1 New Message: \n\nWelcome to your first night at the plant! Things have been totallyyyy stable around here so things should be a breeze.\n\n\nClick to continue..."; }
            if (tutorialScreen == 1) { textBlock = "But she's an old plant, and things do, very ocassionally, go wrong. Just to be sure, I'll run you though the basics. \n\n\n\n\nClick to continue..."; }
            if (tutorialScreen == 2) { textBlock = "See that panel in the top left? You need to make sure your output is always meeting demand. Demand changes throughout the night, so make sure you're comfortably clear. It'll turn red if things are going south. \n\n\n\nClick to continue..."; }
            if (tutorialScreen == 3) { textBlock = "At the bottom is your control console. That's where the magic happens. Use the arrows on the right to regulate the coolant flow. Less coolant flow means hotter temperatures. Hotter temps means more output power. Go ahead and play with them. \n\n\n\nClick to continue..."; }
            if (tutorialScreen == 4) { textBlock = "However! If things get too hot, well, she'll blow. Kabloowie. Make sure that doesn't happen by keeping an eye on the temperature gauge in the top right of the screen. \n\nNow that lever there on the left of the control panel raises and lowers the control rods. When lowered, control rods lessen radiation, but they also decrease power output. Try 'er out.\n\nClick to continue..."; }
            if (tutorialScreen == 5) { textBlock = "Oh, right. Radiation. Bad stuff. It creeps up as the plant creates waste. Make sure to go to the waste disposal room and dump the trash regularly. If shit totally hits the fan, slam the Shields button there. It'll cut the radiation completely for 10 seconds, but you can only use it once so it's for emergencies only.  \n\n\nClick to continue..."; }
            if (tutorialScreen == 6) { textBlock = "To navigate the plant, click on the tablet in the bottom right. If anything goes wrong, you'll need to travel there and fix it yourself. \n\nWell, that's all I've got for you. You're on the clock now. Your replacement will be there in 10 hours! I'm sure you'll do just dandy. \n\n**End of Message.**"; }
            if (tutorialScreen == 6)
            {
                tutorialFinished = true;
                this.gameObject.GetComponent<BoxCollider2D>().enabled  = false;
                navTablet.GetComponent<BoxCollider2D>().enabled = true;
                StartCoroutine(tutorialFinishedDelay());
            }
            //if (tutorialScreen == 8) { textBlock = "Click to continue..."; }
            tutorialScreen++;
        }
        else
        {
            if (numAlerts > 0)
            {
                monitorAlarm.GetComponent<Animator>().Play("Base Layer.Alarming");
                textBlock = "************************ WARNING: " + numAlerts + " Plant automation systems are offline. **************************\n\n";

                if (gManager.ReactorTaskActive) { textBlock += " * Reactor is running low on fuel and core temperature is plummeting. First, acquire fuel from uranium storage room, and then refuel the in reactor.\n"; }
                if (gManager.CoolantTaskActive) { textBlock += " * A pipe in the Coolant System has broken, it must be replaced.\n"; }
                if (gManager.WasteTaskActive) { textBlock += " * Waste Buildup is causing dangerous levels of radiation, dispose of waste immediately.\n"; }
                if (gManager.StorageTaskActive) { textBlock += "This shouldn't even happen."; }

            }
            if (numAlerts == 0)
            {

                textBlock = "The reactor is stable.\n\n";
                textBlock += HappyMessages[UnityEngine.Random.Range(0, HappyMessages.Length)];

                monitorAlarm.GetComponent<Animator>().Play("Base Layer.Off");
            }
        }

        terminalText.SetText(textBlock);
        StartCoroutine(typewriter(terminalText,0,0,false));

    }

    IEnumerator typewriter(TextMeshPro tmpText, float speed = 0.00f, int startIndex = 0, bool colliderLock=false)
    {
        if (colliderLock) { this.gameObject.GetComponent<BoxCollider2D>().enabled = false; }
        tmpText.ForceMeshUpdate();

        int totalVisibleCharacters = tmpText.textInfo.characterCount;
        int counter = startIndex;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);

            tmpText.maxVisibleCharacters = visibleCount;

            counter += 1;

            if (visibleCount >= totalVisibleCharacters)
            {
                break;
            }

            yield return new WaitForSeconds(speed);
        }
        if (colliderLock) { this.gameObject.GetComponent<BoxCollider2D>().enabled = true; }
    }

    IEnumerator tutorialFinishedDelay()
    {
        checker.enabled = true;
        GameManager.instance.TutorialCleared();
        yield return new WaitForSeconds(17f);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        GameManager.instance.StartTasks();
        //gManager.BeginCoolantTask();
    }
}
