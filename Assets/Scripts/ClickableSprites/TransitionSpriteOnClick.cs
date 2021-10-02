using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TransitionSpriteOnClick : MonoBehaviour
{
    public Sprite defaultSprite = null;
    public Sprite transitionSprite = null;

    public float transitionTime = .35f;

    bool isPressed = false;

    private SpriteRenderer rend;

    public UnityEvent updateGameManager;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if(!isPressed)
        {
            isPressed = true;
            StartCoroutine("TransitionSprite");
        }
    }

    IEnumerator TransitionSprite()
    {
        rend.sprite = transitionSprite;
        updateGameManager?.Invoke();
        yield return new WaitForSeconds(transitionTime);
        rend.sprite = defaultSprite;
        isPressed = false;
    }

}
