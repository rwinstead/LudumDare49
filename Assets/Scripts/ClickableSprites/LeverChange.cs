using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverChange : MonoBehaviour
{

    bool hasStoredMousePos = false;

    Vector3 mousePos;

    public LeverManager levermanager;

    private void OnMouseDown()
    {
        if (!hasStoredMousePos)
        {
            mousePos = Input.mousePosition;
            hasStoredMousePos = true;
            StartCoroutine("CheckMousePos");
        }
    }

    IEnumerator CheckMousePos()
    {
        yield return new WaitForSeconds(.15f);
        if (mousePos.y < Input.mousePosition.y)
        {
            bool movedown = false;
            UpdateLeverState(movedown);
        }

        else if (mousePos.y > Input.mousePosition.y)
        {
            bool movedown = true;
            UpdateLeverState(movedown);
        }

        else
        {
            //Do nothing -- this is when the mouse hasn't moved
        }

        hasStoredMousePos = false;
    }

    public void UpdateLeverState(bool moveDown)
    {
        levermanager.UpdateLeverState(moveDown);
    }
}
