using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TerminalManager;
    
    private void OnMouseDown()
    {
        TerminalManager.GetComponent<TerminalManager>().showCredits();
    }
}
