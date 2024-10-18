using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static Action<bool> onPressedFire;
    public void OnPointerDown(PointerEventData eventData)
    {
        onPressedFire?.Invoke(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onPressedFire?.Invoke(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onPressedFire?.Invoke(true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            onPressedFire?.Invoke(false);
        }
    }
}
