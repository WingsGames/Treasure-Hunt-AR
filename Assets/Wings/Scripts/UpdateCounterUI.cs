using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateCounterUI : MonoBehaviour
{
    // Start is called before the first frame update
    Text text;
    public bool hearts, diamnods;
    void Start()
    {
        text = gameObject.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hearts)
            text.text = StaticVars.Hearts.ToString();
        if(diamnods)
            text.text = StaticVars.Diamonds.ToString();
    }
}
