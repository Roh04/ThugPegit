using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampControl : MonoBehaviour
{
    public Light lampLigtht;
    private bool isLampOn = false; // Start with the light off
    private float onDuration = 4f; // Duration to keep the light on
    private float offDuration = 4f; // Duration to keep the light off

    void Start()
    {
        if (lampLigtht == null)
        {
            lampLigtht = GetComponent<Light>();
        }

        InvokeRepeating("ToggleLamp", 0f, offDuration + onDuration);
    }

    void ToggleLamp()
    {
        isLampOn = !isLampOn;
        lampLigtht.enabled = isLampOn;

        if (!isLampOn)
        {
            Invoke("TurnOnLamp", offDuration);
        }
    }

    void TurnOnLamp()
    {
        lampLigtht.enabled = true;
    }
}