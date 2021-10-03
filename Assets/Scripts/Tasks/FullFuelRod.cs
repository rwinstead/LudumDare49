using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullFuelRod : MonoBehaviour
{
    public UraniumStorageManager storageManager;

    private void OnMouseDown()
    {
        storageManager.PickedUpUnit();
        Destroy(gameObject);
    }

 

}
