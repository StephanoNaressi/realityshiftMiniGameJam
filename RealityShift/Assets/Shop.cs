using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public PlayerController controller;

    public ShopOffer Offer1;
    public ShopOffer Offer2;
    public ShopOffer Offer3;

    public void Buy(int aa)
    {
        ShopOffer item = null;
        if(aa == 1)
        {
            item = Offer1;
        }
        if(aa == 2)
        {
            item = Offer2;
        }
        if(aa == 3)
        {
            item = Offer3;
        }
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
