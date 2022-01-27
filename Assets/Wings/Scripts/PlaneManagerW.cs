using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManagerW : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent.transform.parent.GetComponent<ImageTackingW>().MainSurface == null)
        {
            transform.parent.transform.parent.GetComponent<ImageTackingW>().MainSurface = gameObject;
            Debug.Log("Setting main surface");
            enabled = false;
        }
        else
        {
            Debug.Log("Hidding surface");
            gameObject.SetActive(false);
        }

    }

    public void HideInfinite()
    {
        //transform.GetChild(0).gameObject.SetActive(false);
    }
}
