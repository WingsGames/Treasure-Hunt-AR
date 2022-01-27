using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    //public string imageName;
    void Start()
    {
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

    //old scripts
/*
 * if (modelPlaced && !attachedToPlane)
        {

            if(AnchorsExample.lastAnchor == null)
                AnchorsExample.attachCenterAnchor = true;
            else
            {
                Debug.Log("Did Hit: ");
                planeDetectorAnimator.SetTrigger("FoundBoth");
                attachedToPlane = true;
                //Model.transform.parent = AnchorsExample.lastAnchor.transform; // set new plane pos and rot
                Model.transform.parent = AnchorsExample.lastAnchor.transform; // set new plane pos and rot
                Model.transform.localPosition = Vector3.zero;
                Model.transform.eulerAngles = Vector3.zero;  //    Model.transform.localPosition = Vector3.zero;
            }
            //int layerMask = 1 << 8;
            ////layerMask = ~layerMask;
            
            //Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width, Screen.height, 0));

            //RaycastHit hit;

            //if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            //{
            //    //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //    Debug.Log("Did Hit: " + hit.transform.name);

            //    attachedToPlane = true;
            //    //Model.transform.parent = AnchorsExample.lastAnchor.transform; // set new plane pos and rot
            //    Model.transform.position = hit.point;
            //    //Model.transform.localPosition = Vector3.zero;
            //    touchedPos = hitPos;
            //}

                // Does the ray intersect any objects excluding the player layer
            //if (Physics.Raycast(trackedImage.transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
            //{
            //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //    Debug.Log("Did Hit");
            //    attachedToPlane = true;
            //    Model.transform.parent = AnchorsExample.lastAnchor.transform; // set new plane pos and rot
            //    Model.transform.localPosition = Vector3.zero;
            //    touchedPos = hitPos;

            //}
            //planeDetectorAnimator.SetTrigger("FoundBoth");
        }
        //if (!planeAndImageDetected)
        //{
            //planeAndImageDetected = true;

            //planeDetectorAnimator.SetTrigger("FoundBoth");
        //}

        //distanceToTarget = Vector3.Distance(Camera.main.transform.position, playerImage.transform.position);
        
        //if(distanceToTarget < .5f && !playerImage.gameObject.activeSelf)
        //{
        //    playerImage.gameObject.SetActive(true);
        //}
        //if (distanceToTarget > .5f && playerImage.gameObject.activeSelf)
        //{
        //    playerImage.gameObject.SetActive(false);
        //}

       //setToAcive && 
            
        //if (imageTargetOnly)
        //{
        //    //Debug.Log("Image Detected");
        //    if(!Model.activeSelf) Model.SetActive(true);

        //    Model.transform.position = trackedImage.transform.position; // set new plane pos and rot
        //    return;
        //}
        if (!AnchorsExample.gameObject.activeSelf)
        {
            AnchorsExample.gameObject.SetActive(true);
        }

        //if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && !activated)
        //{
        //    //if (!playerImage.GetComponent<PlayerImageW>().Collide) return;
        //    Debug.Log("Tapping");
        //    //if (placementIndicator.transform.position == Vector3.zero) return;//setToAcive && 

        //    //Debug.Log("image found - " + playerImage.name);
        //    //touchedPos = placementIndicator.transform.position;

        //    if (AnchorsExample.lastAnchor != null)
        //    {
        //        Debug.Log("Activated");
        //        activated = true;
        //        touchedPos = AnchorsExample.lastAnchor.transform.position;
        //        Debug.Log("Placing with anchor");
        //        FindObjectOfType<PlaneManagerW>().HideInfinite();
        //    }
        //    else
        //    {
        //        touchedPos = trackedImage.transform.position;
        //        //return;
        //    }


            //RaycastHit hit;
            //var ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            //if (Physics.Raycast(ray, out hit))
            //{
            //    hitPos = hit.point;
            //    // Create a particle if hit
            //    Debug.Log("Hit with ray");
            //    touchedPos = hitPos;

            //}
            //DBtext.text = "image found - " + playerImage.name;


          //  StartCoroutine(Activate());
        //} 
    }

    public Vector3 hitPos;
    IEnumerator Activate() {

        
        yield return new WaitForSeconds(.1f);
        //moveControler.SetActive(true);

        if (touchedPos != Vector3.zero && activated)
        {
            Debug.Log("placing with placementIndicator");
            //Model.transform.parent = placementIndicator.transform.parent;
            //Model.transform.parent = trackedImage.transform;
            
            

            Model.transform.parent = AnchorsExample.lastAnchor.transform; // set new plane pos and rot
            Model.transform.localPosition = Vector3.zero;
            //Model.transform.rotation = placementIndicator.transform.rotation;

        }
        else
        {
            //Debug.Log("placing with image target");
            //Model.transform.parent = trackedImage.transform;
            //Model.transform.localPosition = Vector3.zero;
            //Model.transform.position = trackedImage.transform.position; // set new plane pos and rot
            //Model.transform.rotation = trackedImage.transform.rotation;
        }
        trackedImage.GetComponentInChildren<Canvas>().enabled = false;
        //if(!setToAcive)trackedImage.transform.GetComponentInChildren<Canvas>().enabled = false;
        targetImageWings.ImageDetectedWings();
        yield return new WaitForSeconds(.1f);
        Model.SetActive(true);
        movementManager.activate();
        modelPlaced = true;
        StaticVars.Hearts += 10;

    }

    IEnumerator EnableUpdate()
    {
        yield return new WaitForSeconds(1);
        disable = false;
    } 
*/
}