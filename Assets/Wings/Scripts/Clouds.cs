using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    public static Animator animatorFadeInOut;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        animatorFadeInOut = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void FadeInClouds()
    {
        Debug.Log("Clouds In");
        animatorFadeInOut.SetTrigger("FadeIn");
    }

    public static void FadeOutClouds()
    {
        Debug.Log("Clouds Out");
        animatorFadeInOut.SetTrigger("FadeOut");
    }
}
