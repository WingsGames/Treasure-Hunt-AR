using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticVars : MonoBehaviour
{
    // Start is called before the first frame update
    public static int Hearts, Diamonds;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SaveHeartsAndDiamonds()
    {
        Debug.Log("SaveHeartsAndDiamonds");
        PlayerPrefs.SetInt("Hearts", Hearts);
        PlayerPrefs.SetInt("Diamonds", Diamonds);
        PlayerPrefs.Save();
        Debug.Log("Hearts saved: " + PlayerPrefs.GetInt("Hearts"));

    }

    public static void ReadPrefs()
    {
        Hearts = PlayerPrefs.GetInt("Hearts");
        Diamonds = PlayerPrefs.GetInt("Diamonds");
        Debug.Log("Hearts: " + Hearts);
    }
}
