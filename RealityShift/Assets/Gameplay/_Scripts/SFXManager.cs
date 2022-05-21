using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    
    public AudioClip[] clips;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySound(int s)
    {
        switch (s) { 
        
            case 0:
                //Jump
                createASource(s);
                break;
            case 1:
                //Landing
                createASource(s);
                break;
            case 2:
                //RealityShiftEnter
                createASource(s);
                break;
            case 3:
                //RealityShiftExit
                createASource(s);
                break;
            case 4:
                //RobotStatic
                createASource(s);
                break;
            case 5:
                //UI CANCEL
                createASource(s);
                break;
            case 6:
                //UI DEATH GAMEOVER
                createASource(s);
                break;
            case 7:
                //UIHOVER
                createASource(s);
                break;
            case 8:
                //UIPAUSE
                createASource(s);
                break;
            case 9:
                //UIRESUME
                createASource(s);
                break;
            case 10:
                //UISTARTSELECT
                createASource(s);
                break;

        }
    }
    void createASource(int s)
    {

           
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = clips[s];
                audioSource.Play();
                Destroy(audioSource, clips[s].length);

    }
}
