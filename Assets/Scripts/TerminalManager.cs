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

    private string lastTerminalContents = "";

    public GameObject gameManagerHolder;
    private GameManager gManager = GameManager.instance;

    // Start is called before the first frame update
    void Start()
    {
        gManager = gameManagerHolder.GetComponent<GameManager>();
        StartCoroutine(typewriter(terminalText, 0.03f));

        GameManager.evt_beginReactorTask += BeginReactorTask;
        GameManager.evt_beginCoolantTask += BeginCoolantTask;
        GameManager.evt_beginWasteTask += BeginWasteTask;
        GameManager.evt_beginMainframeTask += BeginMainframeTask;
    }

    // Update is called once per frame
    void Update()
    {

        //hacking
        //terminalText.gameObject.SetActive(false);
        //terminalText_hacked.gameObject.SetActive(true);
        //StartCoroutine(typewriter(terminalText_hacked, 0.005f, 100));

        //monitorAlarm.GetComponent<Animator>().Play("Base Layer.Alarming");
        //monitorAlarm.GetComponent<Animator>().Play("Base Layer.Off");

    }

    public void BeginReactorTask()
    {

    }

    public void BeginCoolantTask()
    {

    }

    public void BeginWasteTask()
    {

    }

    public void BeginMainframeTask()
    {
        terminalText.gameObject.SetActive(false);
        terminalText_hacked.gameObject.SetActive(true);
        StartCoroutine(typewriter(terminalText_hacked, 0.005f, 100));
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

            if (visibleCount >= totalVisibleCharacters)
            {
                break;
            }

            yield return new WaitForSeconds(speed);
        }
    }
}
