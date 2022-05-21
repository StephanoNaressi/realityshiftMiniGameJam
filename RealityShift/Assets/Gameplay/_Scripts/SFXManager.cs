using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    
    public AudioClip[] clips;
    public void PlaySound(int s)
    {
        createASource(s);
    }
    void createASource(int s)
    {

           
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = clips[s];
                audioSource.Play();
                Destroy(audioSource, clips[s].length);

    }
}
