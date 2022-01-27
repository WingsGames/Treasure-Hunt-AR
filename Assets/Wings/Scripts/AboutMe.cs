using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AboutMe : MonoBehaviour
{

    AudioSource audioSource;
    public AudioClip[] audioClips;
    int currentAudio;
    bool done, started, toHalf;
    public Animator buttonAnimator;
    GameManagerWings gameManagerWings;
    // Start is called before the first frame update
    void Start()
    {
        buttonAnimator.SetBool("Stop", false);
        gameManagerWings = FindObjectOfType<GameManagerWings>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.isPlaying) return;
        if(!done && !audioSource.isPlaying && started)
            buttonAnimator.SetBool("Stop", false);
        if(toHalf && !audioSource.isPlaying)
        {
            toHalf = false;
            gameManagerWings.fadeInSFX = true;
        }
    }

    public void onButtonPressed()
    {
        //if (done) return;
        buttonAnimator.SetBool("Stop", true);
        gameManagerWings.fadeOutFactor = 1;
        gameManagerWings.fadeToHalfSFX = true;
        toHalf = true;
        audioSource.clip = audioClips[currentAudio];
        audioSource.Play();
        currentAudio++;

        if(currentAudio > audioClips.Length-1)
        {
            currentAudio = 0;
            //done = true;
        }
    }

    IEnumerator PlayAnim()
    {
        yield return new WaitForSeconds(2);
        buttonAnimator.SetBool("Stop", false);
        started = true;
    }

    public void StartAnim()
    {
        if (!started)
        {
            StartCoroutine(PlayAnim());
        } 
    }
}
