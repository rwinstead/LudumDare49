using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CheckForLoss : MonoBehaviour
{

    public int radiationCap = 399;

    public int demandDiffCap = 200;

    public int tempCap = 999;

    public GameObject loseScreen;

    public TextMeshProUGUI loseText;

    public Image loseImage;

    public Sprite kaboom;

    public Sprite electricity;


    private void Update()
    {
        if(GameManager.instance.radiation >= radiationCap)
        {
            loseText.text = "Oh dear. The radiation cooked you through.";
            GameOver();
        }

        if(GameManager.instance.demand - GameManager.instance.output > demandDiffCap)
        {
            StartCoroutine("DemandGracePeriod");
        }

        if(GameManager.instance.temperature > tempCap)
        {
            loseText.text = "KABOOM! Things got a bit too hot. There's nothing left for miles.";
            loseImage.sprite = kaboom;
            GameOver();
        }

    }

    public void GameOver()
    {
        loseScreen.SetActive(true);
    }

    IEnumerator DemandGracePeriod()
    {
        yield return new WaitForSeconds(5);
        if (GameManager.instance.demand - GameManager.instance.output > demandDiffCap)
        {
            loseText.text = "The local city experienced an extended power blackout and are demanding heads roll. You're first.";
            loseImage.sprite = electricity;
            GameOver();
        }
    }

}
