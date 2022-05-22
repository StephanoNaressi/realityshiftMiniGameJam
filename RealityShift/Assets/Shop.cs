using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public PlayerController controller;

    void Buy(ShopOffer item)
    {
        if(controller.Coins < item.hm) return;
       Attribute a = item.a;
       if(a.type == AType.Speed)
       {
           controller.moveSpeed += a.value;
       } else if(a.type == AType.IncresedShiftTime)
       {
           controller.sc.timer += a.value;
       } else if(a.type == AType.TotalLife)
       {
           controller.MaxHP += a.value;
           controller.HealthLeft += a.value;
       }
    }
}

public class Attribute
{
    public float value;
    public AType type;
}

public enum AType{
    Speed,IncresedShiftTime,TotalLife
}