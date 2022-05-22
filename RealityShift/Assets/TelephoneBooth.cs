using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelephoneBooth : MonoBehaviour
{
    public int acutalLvl;
    void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.tag != "Player") return;

        if(!c.gameObject.GetComponent<PlayerController>().hasCore) return;

        FindObjectOfType<LevelManager>().LoadNextLevel(acutalLvl + 1);
    }
}
