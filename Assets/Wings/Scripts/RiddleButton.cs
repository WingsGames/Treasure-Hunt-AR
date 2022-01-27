using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RiddleButton : MonoBehaviour
{
    // Start is called before the first frame update
    TargetImageWings TargetImageWings;
    Button button;
    public GameObject planeDetector;
    void Start()
    {
        TargetImageWings = FindObjectOfType<TargetImageWings>();
        button = GetComponent<Button>();
        button.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(TargetImageWings != null)
        {
            if(TargetImageWings.objectSpawner.playerImage != null)
            {
                gameObject.SetActive(false);
            }

            if(TargetImageWings.scroll.activeSelf && button.interactable) //disable button
            {
                button.interactable = false;
                Debug.Log("!interactable");
                planeDetector.SetActive(false);
            }
            if(!TargetImageWings.scroll.activeSelf && !button.interactable)//enanle button
            {
                button.interactable = true;
                Debug.Log("interactable");
                planeDetector.SetActive(true);
            }
        }

    }

    public void Pressed()
    {
        TargetImageWings.AskRiddle();
    }
}
