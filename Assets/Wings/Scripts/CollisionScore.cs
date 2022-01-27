using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CollisionScore : MonoBehaviour
{
    public ScoreW scoreW;
    AudioSource audioSource;
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(StopW());
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    // Update is called once per frame
    void Update()
    {
        //if(!audioSource.isPlaying)
          //  audioSource = gameObject.GetComponent<AudioSource>();
    }
    void OnParticleCollision(GameObject other)
    {
        scoreW.AddScore();
        //audioSource.Play();
    }

    IEnumerator StopW()
    {
        if (GameManagerWings.maxImages == 0) GameManagerWings.maxImages = 5;
        Debug.Log("Stop part1");
        yield return new WaitForSeconds(10);//(GameManagerWings.maxImages);
        var yourParticleEmission = GetComponent<ParticleSystem>().emission;
        Debug.Log("Stop part2");
        yourParticleEmission.rateOverTime = 0;
    }
}
