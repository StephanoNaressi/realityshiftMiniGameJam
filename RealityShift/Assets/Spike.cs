using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    /// PLEASE DO NOT CHANGE MY CODE ///
    /// IF YOU SEE ANY ERRORS, PLEASE INFORM ME ///

    public float Damage;

    public Animator SpikeController;

    private bool damaged;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(!damaged){
            //Do emotional damage
                PlayerController c = other.gameObject.GetComponent<PlayerController>();
                c.AddHealth(-Damage);
                damaged = true;
            }

            SpikeController.SetBool("Spike",true);
        }
    }

    public void OnCollisionExit2D(Collision2D exit)
    {
        if(exit.gameObject.tag == "Player")
        {
            SpikeController.SetBool("Spike", false);
            damaged = false;

            //print("left + " + SpikeController.GetBool("Spike"));
        }
    }
}
