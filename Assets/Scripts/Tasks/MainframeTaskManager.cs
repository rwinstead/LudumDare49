using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainframeTaskManager : MonoBehaviour
{

    public TMP_InputField login;
    public TMP_InputField password;

    public TMP_InputField wipe;

    public TMP_Text StickyLogin;
    public TMP_Text StickyPassword;

    public string correctLogin = null;
    public string correctPass = null;

    private string[] Usernames = new string[] { "BobbyBoi", "JimBob3", "Maurice_Moss", "RuyLopez", "MelonLord", "twinkle.toes" };
    private string[] Passwords = new string[] { "ihatePasswrds", "iLuvChickn", "Potatoads87!", "Password123", "LudenDare49" };

    public GameObject step1;
    public GameObject step2;
    public GameObject step3;

    public static Action evt_endMainframeTask;

    void Start()
    {
        GameManager.evt_beginMainframeTask += BeginMainframeTask;

        BeginMainframeTask();

    }

    private void OnDestroy()
    {
        GameManager.evt_beginMainframeTask -= BeginMainframeTask;
    }

    public void checkCredentials()
    {
        if (login.text == correctLogin && password.text == correctPass)
        {
            step1.SetActive(false);
            step2.SetActive(true);
        }
    }

    public void checkServerWipeReply()
    {
        if(wipe.text.ToLower() == "y")
        {
            step2.SetActive(false);
            step3.SetActive(true);
            evt_endMainframeTask?.Invoke();
        }
    }

    public void BeginMainframeTask()
    {
        step1.SetActive(true);
        step2.SetActive(false);
        step3.SetActive(false);

        correctLogin = Usernames[UnityEngine.Random.Range(0, Usernames.Length)];
        correctPass = Passwords[UnityEngine.Random.Range(0, Passwords.Length)];

        StickyLogin.SetText(correctLogin);
        StickyPassword.SetText(correctPass);




    }

}
