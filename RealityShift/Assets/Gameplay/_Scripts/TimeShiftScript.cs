using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class TimeShiftScript : MonoBehaviour
{
    [SerializeField]
    Material shaderM;
    bool isShifted = false;
    [SerializeField]
    GameObject hiddenLayer;

    int ShiftCount;
    [SerializeField]
    public float timer;
    float startingTimer;
    float shiftCount;
    [SerializeField]
    TrailRenderer trail;
    [SerializeField]
    TextMeshProUGUI counterText;

    [SerializeField]
    Sprite[] cooldownBar;
    [SerializeField]
    GameObject cooldownSprite;
    public int shiftLives;
    private void Start()
    {
        
        trail.enabled = false;
        shiftCount = 0f;
        startingTimer = timer;
        isShifted = false;
        shaderM.SetFloat("_IsShift", 0f);
        shaderM.SetFloat("_TimesShifted", 0f);
        counterText.text = shiftLives.ToString();
    }
    private void Update()
    {
        if((shiftCount * 10 / 2) >= shiftLives)
        {
            print("YOUDIED");
            SceneManager.LoadScene("GameOver");
        }
        if (Input.GetButtonDown("realityShift"))
        {
            TimeShift();
            
        }
        if(isShifted == true)
        {
            timerF();
        }
        //((int)timer).ToString();
        cooldownSprite.GetComponent<Image>().sprite = cooldownBar[Mathf.Clamp((int)timer, 0, 7)] ;
       
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
            counterText.text = Mathf.RoundToInt((shiftLives-(shiftCount * 10 / 2))).ToString();
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
            gameObject.GetComponent<SFXManager>().PlaySound(3);
            trail.enabled = false;
            isShifted = false;
            hiddenLayer.SetActive(false);
            
        }
        else
        {
            //Break the reality
            shaderM.SetFloat("_IsShift", 1f);
            gameObject.GetComponent<SFXManager>().PlaySound(2);
            trail.enabled = true;
            //Add reality logic here!
            isShifted = true;
            hiddenLayer.SetActive(true);
        }
        
    }
}
