using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
public class ObjectSpawner : MonoBehaviour
{
    public AnchorsW AnchorsExample;
    public PlacementIndicator placementIndicator;
    public Vector3 touchedPos;

    public Text DBtext;
    public GameObject playerImage; 
    public GameObject Model;
    int counter;
    
    public bool disable = true;
    public TargetImageWings targetImageWings; 
    [HideInInspector]
    public GameObject trackedImage;

    public GameObject moveControler;

    MovementManager movementManager;
    private bool activated;
    //bool setToAcive;
    public Image colorIndi;
    public float distanceToTarget;
    public bool modelPlaced, attachedToPlane;
    bool planeDetected;
    public Animator planeDetectorAnimator;
    public ARTrackedImageManager aRTrackedImageManager;
    //public string imageName;
    void Start()
    {
        aRTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        aRTrackedImageManager.enabled = false; //enables from TargetImageWings
        planeDetectorAnimator.SetLayerWeight(1, 0);
        StaticVars.ReadPrefs();
        placementIndicator = FindObjectOfType<PlacementIndicator>();
        Model.SetActive(false);

        movementManager = Model.GetComponent<MovementManager>();
        AnchorsExample = GameObject.Find("AnchorsExample").GetComponent<AnchorsW>();

        AnchorsExample.enabled = false;

    }

    void Update()
    {
        if (disable) return;

        if (placementIndicator.transform.position != Vector3.zero)
        {
            if (!planeDetected)
            {
                planeDetected = true;
                
            }
            if (colorIndi) colorIndi.color = Color.green;
        }

        if (playerImage == null)
            playerImage = GameObject.FindGameObjectWithTag("PlayerImage");

        if (playerImage == null) return;//!setToAcive && 
        else
        {
            if (!modelPlaced)
            {
                Debug.Log("placing with image target");
                Model.transform.parent = trackedImage.transform;
                Model.transform.localPosition = Vector3.zero;

                trackedImage.GetComponentInChildren<Canvas>().enabled = false;
                //if(!setToAcive)trackedImage.transform.GetComponentInChildren<Canvas>().enabled = false;
                FindObjectOfType<AnchorsW>().image = playerImage.transform;
                Model.SetActive(true);
                movementManager.activate();
                modelPlaced = true;
                StaticVars.Hearts += 10;
                planeDetectorAnimator.SetTrigger("Found");
                planeDetectorAnimator.SetLayerWeight(1, 1);
            }
        }

        if (modelPlaced && !attachedToPlane)
        {
            if(AnchorsExample.lastAnchor == null)
                AnchorsExample.attachCenterAnchor = true;
            else
            {
                targetImageWings.ImageDetectedWings();//to show action menu
                Debug.Log("Did Hit: ");
                planeDetectorAnimator.SetTrigger("FoundBoth");
                planeDetectorAnimator.SetLayerWeight(1, 0);
                attachedToPlane = true;
                //Model.transform.parent = AnchorsExample.lastAnchor.transform; // set new plane pos and rot
                Model.transform.parent = AnchorsExample.lastAnchor.transform; // set new plane pos and rot
                Model.transform.localPosition = Vector3.zero;
                Model.transform.eulerAngles = new Vector3(0, Model.transform.eulerAngles.y, 0);  //    Model.transform.localPosition = Vector3.zero;
            }
        }

        if (!AnchorsExample.enabled)
        {
            AnchorsExample.enabled = true ;
        }
    }

    public Vector3 hitPos;
}