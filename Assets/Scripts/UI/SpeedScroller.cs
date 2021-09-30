﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedScroller : MonoBehaviour
{
    [SerializeField]
    private Movement movementScript;
    [SerializeField]
    private TMP_InputField speedInput;

    private Scrollbar scrollbar;
    private float referenceSpeed;
    
    public float ReferenceSpeed
    {
        get { return referenceSpeed; }
    }

    private void Start()
    {
        scrollbar = GetComponent<Scrollbar>();
        referenceSpeed = movementScript.SpeedFactor;
        speedInput.text = referenceSpeed.ToString();
        scrollbar.value = movementScript.SpeedFactor/(2*referenceSpeed);
    }

    // The speed scroller can cycle between 0 to 2 times the reference speed
    public void UpdateSpeed(float value)
    {
        float result = value * 2f * referenceSpeed;
        movementScript.UpdateSpeedFactor(result);
        speedInput.text = result.ToString();
    }

    // Use the input to change the speed value
    public void UpdateSpeedThroughInput(string value)
    {
        if(IsDigitsOnly(value))
        {
            float result = float.Parse(value) / (2 * referenceSpeed);
            Debug.Log("The returned value is: " + float.Parse(value) + " and the result is: " + result);
            if (result <= 1f && result >= 0f)
            {
                scrollbar.value = result;
            }
            else if (result > 1f)
            {
                scrollbar.value = 1f;
            }
            else if (result < 0f)
            {
                scrollbar.value = 0f;
            }
        }
        else
        {
            Debug.LogWarning("Characters entered. Please enter numbers only");
        }
    }

    private bool IsDigitsOnly(string entry)
    {
        foreach(char c in entry)
        {
            if(c < '0' || c > '9')
            {
                return false;
            }
        }
        return true;
    }
}
