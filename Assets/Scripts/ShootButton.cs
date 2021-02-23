using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class ShootButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isPressed = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        FindObjectOfType<Player>().FireFromButton();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        FindObjectOfType<Player>().StopFiring();
    }

    public bool isButtonPressed()
    {
        return isPressed;
    }
}
