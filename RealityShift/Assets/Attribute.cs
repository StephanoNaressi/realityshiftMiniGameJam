using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Attribute : MonoBehaviour
{
    public float value;
    public AType type;
}

public enum AType{
    Speed,IncresedShiftTime,TotalLife
}