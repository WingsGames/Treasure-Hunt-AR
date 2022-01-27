using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCanvas : MonoBehaviour
{
    ObjectSpawner objectSpawner;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        objectSpawner = FindObjectOfType<ObjectSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        return;
        //if(objectSpawner != null)
        //{
        //    if (!objectSpawner.planeDetected && canvas.activeSelf)
        //    {
        //        canvas.SetActive(false);
        //    }
        //                                                                            // removed this making unhappy game play
        //    if (objectSpawner.planeDetected && !canvas.activeSelf)// && (objectSpawner.playerImage.GetComponent<PlayerImageW>().Collide))
        //    {
        //        canvas.SetActive(true);
        //    }

        //    if (objectSpawner.modelPlaced)
        //    {
        //        gameObject.SetActive(false);
        //    }
        //}
        //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
