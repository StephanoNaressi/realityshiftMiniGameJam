using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeShiftScript : MonoBehaviour
{
    [SerializeField]
    Material shaderM;
    bool isShifted = false;
    [SerializeField]
    GameObject hiddenLayer;

    int ShiftCount;
    [SerializeField]
    float timer;
    float startingTimer;
    float shiftCount;
    [SerializeField]
    TrailRenderer trail;
    private void Start()
    {
        trail.enabled = false;
        shiftCount = 0f;
        startingTimer = timer;
        isShifted = false;
        shaderM.SetFloat("_IsShift", 0f);
        shaderM.SetFloat("_TimesShifted", 0f);
    }
    private void Update()
    {
        if (Input.GetButtonDown("realityShift"))
        {
            TimeShift();
            
        }
        if(isShifted == true)
        {
            timerF();
        }

        
    }
    bool timerF()
    {

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            
        }
        else
        {
            TimeShift();
            timer = startingTimer;
            shiftCount+= 0.2f;
            shaderM.SetFloat("_TimesShifted", shiftCount);
            return false;
        }
        return true;
        
    }
    void TimeShift()
    {
        if (isShifted)
        {
            //Reality has been shifted
            shaderM.SetFloat("_IsShift", 0f);
            //Set reality to normal
            trail.enabled = false;
            isShifted = false;
            hiddenLayer.SetActive(false);
        }
        else
        {
            //Break the reality
            shaderM.SetFloat("_IsShift", 1f);
            
            trail.enabled = true;
            //Add reality logic here!
            isShifted = true;
            hiddenLayer.SetActive(true);
        }
        
    }
}
