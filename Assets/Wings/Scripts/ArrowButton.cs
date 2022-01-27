using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowButton : MonoBehaviour
{
    bool pointerDown;
    public ScrollRect scrollRect;
    public float addValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pointerDown)
        {
            scrollRect.horizontalNormalizedPosition += addValue;
            Debug.Log("moving bar " + addValue);
        }
           
    }

    public void OnPointerDown()
    {
        pointerDown = true;
    }

    public void OnPointerUp()
    {
        pointerDown = false;
    }
}
