using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainframeTaskManager : MonoBehaviour
{

    public TMP_InputField login;
    public TMP_InputField password;

    public TMP_InputField wipe;

    public string correctLogin = null;
    public string correctPass = null;

    public GameObject step1;
    public GameObject step2;
    public GameObject step3;

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
        }
    }

}
