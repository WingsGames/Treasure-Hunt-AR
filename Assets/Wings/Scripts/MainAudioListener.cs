using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAudioListener : MonoBehaviour
{
    public static AudioListener mainAudioListener;
    // Start is called before the first frame update
    void Start()
    {
        if(mainAudioListener == null)
        {
            mainAudioListener = GetComponent<AudioListener>();
            DontDestroyOnLoad(this);

        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame

}
