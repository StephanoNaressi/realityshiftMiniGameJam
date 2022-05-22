using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    public TextMeshPro DisplayText;

    public PlayerController controller;

    public string iname;

    public Type type;

    void Start()
    {
        DisplayText.text = iname;
    }

    void PickUp()
    {
        controller.PickUp(this);
        //print("Destroyin");
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D s)
    {
        if(s.gameObject.tag == "Player")
        {
            //Do picking up

            PickUp();

            //print("Picking");
        }
    }
}

public enum Type
{
    Gold, Core
}