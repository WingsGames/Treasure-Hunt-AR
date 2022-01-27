using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] walkSteps, runSteps, jumpSteps, landSteps;
    public AudioSource AudioSource;

    private void Start()
    {
        if(AudioSource == null)
            AudioSource = GetComponent<AudioSource>();
    }
    public void WalkStep(int ID)
    {
        AudioSource.clip = walkSteps[ID];
        AudioSource.Play();
    }
    public void RunStep(int ID)
    {
        AudioSource.clip = runSteps[ID];
        AudioSource.Play();
    }
    public void JumpStep(int ID)
    {
        AudioSource.clip = jumpSteps[ID];
        AudioSource.Play();
    }
    public void LandStep(int ID)
    {
        AudioSource.clip = landSteps[ID];
        AudioSource.Play();
    }
}
