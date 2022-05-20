using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    public RawImage img;

    public PlayerController controller;

    // Update is called once per frame
    void Update()
    {
        if(controller == null) controller = FindObjectOfType<PlayerController>();

        float hp = controller.HealthLeft;
        float scale = hp/controller.MaxHP ; //50
    }
}
