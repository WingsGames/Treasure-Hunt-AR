using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public bool played, isOpen, treasureCollected;
    public AudioClip open, close;
    AudioSource audioSource;
    public AudioSource diamonds;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (played) return;
        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                // Create a particle if hit
                if (Physics.Raycast(ray))
                {
                    if (!isOpen)
                    {
                        
                        played = true;
                        isOpen = true;
                        GetComponent<Animation>().Play("ChestOpen");
                        audioSource.clip = open;
                        audioSource.Play();
                        StartCoroutine(ReleaseTimer());
                    }
                    else
                    {
                        isOpen = false;
                        GetComponent<Animation>().Play("ChestClose");
                        audioSource.clip = close;
                        audioSource.Play();
                    }
                    
                }
            }
        }

        if(played && !GetComponent<Animation>().isPlaying)
        {
            played = false;
            
        }

        IEnumerator ReleaseTimer()
        {
            if (!treasureCollected)
            {
                treasureCollected = true;
                yield return new WaitForSeconds(8);

            }
            else
            {
                diamonds.volume = 0;
                yield return new WaitForSeconds(3);
            }
            played = false;
        }
    }
}
