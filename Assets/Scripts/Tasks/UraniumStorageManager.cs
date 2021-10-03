using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UraniumStorageManager : MonoBehaviour
{
    public int unitsPickedUp = 0;

    public void PickedUpUnit()
    {
        unitsPickedUp++;
    }
}
