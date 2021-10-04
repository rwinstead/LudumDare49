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

    private string[] Usernames = new string[] { "BobbyBoi", "JimBob3", "Maurice_Moss", "RuyLopez", "MelonLord", "twinkle.toes", "Anonymoose", "Leorio!", "Admin", "root" };
    private string[] Passwords = new string[] { "ihatePasswrds", "Potatoads87!", "Password123", "LudumDare49", "NotTellingYou", "R$P5#oDW", "d$!P@Za8", "P@$$w()rd", "iHaetpasswords" };

    public GameObject step1;
    public GameObject step2;
    public GameObject step3;

    public static Action evt_endMainframeTask;

    void Start()
    {
        GameManager.evt_beginMainframeTask += BeginMainframeTask;

        BeginMainframeTask();

    }

    void Update()
    {
        if (Input.GetKeyDown("tab") && login.isFocused)
        {
            password.ActivateInputField();
        }

        if (Input.GetKeyDown(KeyCode.Return) && step1.activeSelf)
        {
            checkCredentials();
        }
    }

    public void checkCredentials()
    {
        if (login.text == correctLogin && password.text == correctPass)
        {
            step1.SetActive(false);
            step2.SetActive(true);
            wipe.ActivateInputField();
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

        login.text = "";
        password.text = "";
        wipe.text = "";

        StickyLogin.SetText(correctLogin);
        StickyPassword.SetText(correctPass);




    }

    private void OnDestroy()
    {
        GameManager.evt_beginMainframeTask -= BeginMainframeTask;
    }

}
