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
            //Reality has been shifted
            shaderM.SetFloat("_IsShift", 0f);
            //Set reality to normal
            isShifted = false;
        }
        else
        {
            //Break the reality
            shaderM.SetFloat("_IsShift", 1f);
            isShifted = true;
            //Add reality logic here!
        }
        
    }
}
