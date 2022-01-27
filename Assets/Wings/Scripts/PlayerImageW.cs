using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImageW : MonoBehaviour
{
    public bool Collide;
    public GameObject movingPart;
    // Start is called before the first frame update
    void Start()
    {
        //movingPart.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Plane")
        {
            Collide = true;
            movingPart.SetActive(true);
        }
    }
}
