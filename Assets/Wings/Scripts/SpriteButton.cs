using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpriteButton : MonoBehaviour
{
    public UnityEvent Clicked;

    private void OnMouseDown()
    {
        Debug.Log("Pressed");
        Clicked.Invoke();
    }


}
