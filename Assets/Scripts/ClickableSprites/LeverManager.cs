using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverManager : MonoBehaviour
{
    public List<GameObject> leverSprites = new List<GameObject>();

    private GameObject activeSprite;

    public enum leverState
    {
        Top,
        Middle,
        Bottom
    }

    public leverState leverstate = leverState.Middle;

    private void Start()
    {
        activeSprite = leverSprites[1];
    }

    public void UpdateLeverState(bool moveDown)
    {
        if (moveDown)
        {
            if (leverstate == leverState.Top)
            {
                leverstate = leverState.Middle;
            }

            else if (leverstate == leverState.Middle)
            {
                leverstate = leverState.Bottom;
            }
        }

        if (!moveDown)
        {
            if (leverstate == leverState.Middle)
            {
                leverstate = leverState.Top;
            }

            else if (leverstate == leverState.Bottom)
            {
                leverstate = leverState.Middle;
            }
        }

        UpdateButtonSprite();

    }

    public void UpdateButtonSprite()
    {
        if(leverstate == leverState.Top && activeSprite != leverSprites[0])
        {
            activeSprite.SetActive(false);
            leverSprites[0].SetActive(true);
            activeSprite = leverSprites[0];
        }

        if (leverstate == leverState.Middle && activeSprite != leverSprites[1])
        {
            activeSprite.SetActive(false);
            leverSprites[1].SetActive(true);
            activeSprite = leverSprites[1];
        }

        if (leverstate == leverState.Bottom && activeSprite != leverSprites[2])
        {
            activeSprite.SetActive(false);
            leverSprites[2].SetActive(true);
            activeSprite = leverSprites[2];
        }
    }

}
