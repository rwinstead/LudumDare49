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

    private string[] HappyMessages = new string[] { "You go girl!", "The bees are happy. :-)", "All is right with the world", "Take a break, you deserve it.", "They should give you a raise.", "First you set yourself to rights. And then your house. And then your corner of the sky. And after that... Well, then she didn't rightly know what happened next." };

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
        string textBlock = "This game was built in 72 hours for Ludum Dare 49 in Oct 2021 \n\n Contributors:\n Ryan Winstead\n Josh Todd\n\nThanks for Playing!";
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
            if (tutorialScreen == 0) { textBlock = "You have 2 New Messages: \n\nHey! It's been quiet today, so we all decided to go home early. What could go wrong? The reactor is stable and should be fine on it's own, but if anything breaks you'll have to fix it yourself. Hope you don't mind. Oh, and please try not to blow anything up.\n\nManagement                                                                     Click to continue..."; }
            if (tutorialScreen == 1) { textBlock = "Message 2 of 2: \n\n They did WHAT? I TOLD THEM the new guy was starting today! Ugh, those morons never listen.\nLook, I'm sure it'll be fine, I'll run you though the basics. \n\n\nClick to continue..."; }
            if (tutorialScreen == 2) { textBlock = "See that panel in the top left? You want to match your output to demand as close as possible. Produce too much power and you'll fry the substation.  Produce too little and you'll cause a blackout. \n\n\n\n\nClick to continue..."; }
            if (tutorialScreen == 3) { textBlock = "Use the buttons on the right to regulate the coolant flow.  Too cold and the reactor will shut down, too hot and it'll go into meltdown. \n\n\n\n\nClick to continue..."; }
            if (tutorialScreen == 4) { textBlock = "Use the lever on the left to control the output of the reactor.  Just be careful not to let radiation levels too high.  \n\n\n\n\nClick to continue..."; }
            if (tutorialScreen == 5) { textBlock = "If that happens use the button to raise your radiation shielding, but you can only use it once so it's for emergencies only.  \n\n\n\n\nClick to continue..."; }
            if (tutorialScreen == 6) { textBlock = "All the other systems in the plant are automated so that should be about it.  Although if something does break down use the tablet on the desk to navigate the plant. \n\nYour replacement will be there in 10 hours!  I'm sure you'll do fine."; }
            if (tutorialScreen == 6)
            {
                tutorialFinished = true;
                this.gameObject.GetComponent<BoxCollider2D>().enabled  = false;
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

                if (gManager.ReactorTaskActive) { textBlock += " * Reactor is running low on fuel.  Acquire from storage and refuel the reactor.\n"; }
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
        GameManager.instance.TutorialCleared();
        yield return new WaitForSeconds(20f);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //gManager.BeginCoolantTask();
    }
}
