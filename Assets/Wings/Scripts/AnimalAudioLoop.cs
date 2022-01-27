using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAudioLoop : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    public AudioClip awakeClip;
    public AudioClip sourceClip;
    public bool playing;
    private void Update()
    {
        if(!audioSource.isPlaying && !playing)
        {
            playing = true;
            StopAllCoroutines();
            StartCoroutine(PlayLoop());
        }
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sourceClip = audioSource.clip;
    }
    // Update is called once per frame

    IEnumerator PlayLoop()
    {
        //yield return new WaitForSeconds(.1f);
        Debug.Log("Audio wake");
        
        
        audioSource.clip = awakeClip;
        audioSource.loop = false;
        audioSource.Play();
        yield return new WaitForSeconds(3);
        Debug.Log("Audio enviro");
        audioSource.Stop();
        audioSource.clip = sourceClip;
        yield return new WaitForSeconds(.1f);
        audioSource.loop = true;
        audioSource.Play();

    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

}
