using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningMessage : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject root;
    void Start()
    {
        if(PlayerPrefs.GetString("AcceptNote") == "true")
        {
            root.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AcceptNote()
    {
        PlayerPrefs.SetString("AcceptNote", "true");
        PlayerPrefs.Save();
        root.SetActive(false);
    }
}
