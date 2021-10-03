using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodDetector : MonoBehaviour
{

    public ReactorManager reactor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Rod"))
        {
            reactor.IncreaseRodCount();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Rod"))
        {
            reactor.DecreaseRodCount();
        }
    }

}
