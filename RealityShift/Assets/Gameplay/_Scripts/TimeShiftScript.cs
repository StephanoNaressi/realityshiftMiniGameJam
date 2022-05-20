using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeShiftScript : MonoBehaviour
{
    [SerializeField]
    Material shaderM;
    bool isShifted = false;
    private void Update()
    {
        if (Input.GetButtonDown("realityShift"))
        {
            TimeShift();
        }
    }
    void TimeShift()
    {
        if (isShifted)
        {
            shaderM.SetFloat("_IsShift", 0f);
            isShifted = false;
        }
        else
        {
            shaderM.SetFloat("_IsShift", 1f);
            isShifted = true;
        }
        
    }
}
