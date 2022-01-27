using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OpenCloseBar : MonoBehaviour
{
    public bool isOpen;
    public Animator barAnimator;
    public Text plusMinusText;
    public MovementManager movementManager;
    void Start()
    {
        movementManager = FindObjectOfType<MovementManager>();
        movementManager.panelOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonPressed()
    {
        return; //disabled since panel is now always open
        //isOpen = !isOpen;
        //if (isOpen)
        //{
        //    plusMinusText.text = "-";
        //    barAnimator.SetTrigger("OpenBar");
        //    movementManager.panelOpen = true;
        //}
        //else
        //{
        //    plusMinusText.text = "+";
        //    barAnimator.SetTrigger("CloseBar");
        //    movementManager.panelOpen = false;
        //}
    }
}
