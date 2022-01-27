using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float scrollPosition, scaleFactor, delta;
    public float globalScale;
    public int posNum;
    public float childCount;
    RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        scrollPosition = scrollRect.horizontalNormalizedPosition;
        delta = (posNum-1) * (1f/(childCount-1f));
        scaleFactor = 1f - ((scrollPosition - delta) * 3f);
        
        if( scaleFactor > 0.5 && scaleFactor < 1 )
            rectTransform.localScale = new Vector3(scaleFactor*0.37f, scaleFactor*2f, 2) * globalScale;
        if(scaleFactor > 1)
        {
            rectTransform.localScale = new Vector3((2-scaleFactor) * 0.37f, (2-scaleFactor) * 2f, 2) * globalScale;
        }
    }
}
