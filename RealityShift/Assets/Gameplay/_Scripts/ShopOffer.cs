using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopOffer : MonoBehaviour
{
    public int hm;
    public TextMeshPro HMT;
    public Attribute a;
    public TextMeshPro DT;


    void Start()
    {
        HMT.text = hm.ToString();
        string att = "";

        if(a.type == AType.Speed)
        {
            att = "Speed increase by";
        }else if(a.type == AType.IncresedShiftTime)
        {
            att = "Shift timer increased by";
        }else if(a.type == AType.TotalLife)
        {
            att = "Total life increased by";
        }


        DT.text = att + a.value;
    }
}
