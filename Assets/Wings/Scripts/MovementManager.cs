using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovementManager : MonoBehaviour
{
    public float rotationSpeed = 45f;
    public float autoRotSpeed = 2f;
    public Vector3 pos, lastPos;
    public float walkSpeed = 0.1f, runSpeed = 0.3f;
    public bool autoForward, lookAtSet, destinationSet, looping;
    float forwardSpeed, closesetDistance;
    public bool activated = false, flipDirection, boat;
    float  startRotY,  endRotY; 
    public float t, distance, distanceUp, distanceCenter;
    public AnchorsW anchorsExample;
    public Transform destination, lastDestination, CopyRot, lastAnchor, destinationCenter, destinationUp;
    public bool wait, tapped, walkable, runnable, panelOpen, centerSet;
    PlayLegacyAnimation playLegacyAnimation;
    public GameObject particleRootBlue, particleYellowRoot;
    public ParticleSystem[] blueSystem, yellowSystem;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anchorsExample = GameObject.Find("AnchorsExample").GetComponent<AnchorsW>();
        var newTrans = new GameObject().transform; newTrans.gameObject.name = "CopyRot";
        var newTrans1 = new GameObject().transform; newTrans1.gameObject.name = "DestinationUp";
        var newTrans2 = new GameObject().transform; newTrans2.gameObject.name = "DestinationCenter";
        CopyRot = newTrans;
        destinationUp = newTrans1;
        destinationCenter = newTrans2;
        lastDestination = destination = transform;
        if(walkable) forwardSpeed = walkSpeed;
        if (runnable || boat) forwardSpeed = runSpeed;
        if (!walkable && !runnable && !boat) this.enabled = false;
        playLegacyAnimation = FindObjectOfType<PlayLegacyAnimation>();
        
        if (particleRootBlue.activeSelf)
        {
            blueSystem = particleRootBlue.GetComponentsInChildren<ParticleSystem>();
        }
        if (particleYellowRoot.activeSelf)
        {
            yellowSystem = particleYellowRoot.GetComponentsInChildren<ParticleSystem>();
        }


    }
    void Update()
    {
        if (!activated) return;
        

        if (centerSet) // to align targets height at all time with model
        {
            destinationCenter.position = new Vector3(destinationCenter.position.x, transform.position.y, destinationCenter.position.z);
            destinationUp.position = new Vector3(destinationCenter.position.x, destinationCenter.position.y, destinationCenter.position.z + .25f);
        }

        if (autoForward)
        {
            if (looping)
            {
                if (destination == destinationUp && Vector3.Distance(destinationUp.position, transform.position) < 0.05f)
                {
                    destinationSet = false;
                    Debug.Log("Flipped");
                }
                if (destination == destinationCenter && Vector3.Distance(destinationCenter.position, transform.position) < 0.05f)
                {
                    destinationSet = false;
                    Debug.Log("Flipped");
                }

                distanceUp = Vector3.Distance(destinationUp.position, transform.position);
                distanceCenter = Vector3.Distance(destinationCenter.position, transform.position);

                if (Vector3.Distance(destinationUp.position, transform.position) > Vector3.Distance(destinationCenter.position, transform.position))
                {
                    if (!destinationSet)
                    {
                        Debug.Log("Going up");
                        destinationSet = true;
                        tapped = false;
                        destination = destinationUp;
                        CopyRot.position = transform.position;
                        CopyRot.LookAt(destination);
                        startRotY = transform.eulerAngles.y;
                        endRotY = CopyRot.transform.eulerAngles.y;
                        t = 0;
                    }
                }
                else
                {
                    if (!destinationSet)
                    {
                        Debug.Log("Going down");
                        destinationSet = true;
                        tapped = false;
                        destination = destinationCenter;
                        CopyRot.position = transform.position;
                        CopyRot.LookAt(destination);
                        startRotY = transform.eulerAngles.y;
                        endRotY = CopyRot.transform.eulerAngles.y;
                        t = 0;
                    }
                }

                


            }
            if (t < 1)
            {
                lookAtSet = false;
                t += Time.deltaTime * autoRotSpeed;
                float currentY = Mathf.Lerp(startRotY, endRotY, t);
                transform.eulerAngles = new Vector3(0, currentY, 0);
                //Debug.Log("Current Y: " + currentY);
            }
            else
            {
                if (!lookAtSet)
                {
                    lookAtSet = true;
                    transform.LookAt(destination);
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                    Debug.Log("LookAt Y: " + transform.eulerAngles.y);
                }
        }
        transform.Translate(0, 0, forwardSpeed * Time.deltaTime);
           
            //transform.LookAt(destination);
            //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        if (anchorsExample == null) return;
        if (anchorsExample.lastAnchor == null) return;
        if (!centerSet)
        {
            centerSet = true;
            Debug.Log("Center Set");
            if(anchorsExample.centerAnchor != null)
            {
                destinationCenter.position = anchorsExample.centerAnchor.transform.position;
                destinationCenter.position = new Vector3(destinationCenter.position.x, transform.position.y, destinationCenter.position.z);
                destinationUp.position = new Vector3(destinationCenter.position.x, destinationCenter.position.y, destinationCenter.position.z + .25f);

            }

        }
        if (Input.touchCount > 0 && !tapped)
        {
            Touch touch = Input.GetTouch(0);
            {

                if (touch.position.y < Screen.height * .2f)
                {
                    tapped = false;
                    return;
                }
            } //checking if in bounds - tapped = true
            
            Debug.Log("Tapped!");
            looping = false;

            tapped = true;
            t = 0;
            autoForward = true;
            CancelInvoke("FlipLoop");
            lastAnchor = anchorsExample.lastAnchor.transform; // if tap is in clear area.
            destination = lastAnchor;
            if (boat)
            {
                audioSource.Play();
            }

            if (walkable) playLegacyAnimation.PlayAnimation("Walk");
            if (runnable) playLegacyAnimation.PlayAnimation("Run");
            if (walkable) forwardSpeed = walkSpeed;
            if (runnable || boat) forwardSpeed = runSpeed;
            CopyRot.position = transform.position;
            CopyRot.LookAt(destination);
            startRotY = transform.eulerAngles.y;
            endRotY = CopyRot.transform.eulerAngles.y;

        }

        if (tapped)
        {
            distance = Vector3.Distance(transform.position, new Vector3(destination.position.x, transform.position.y, destination.position.z));
            closesetDistance = distance;
            if (distance < .02f)
            {
                tapped = false;
                playLegacyAnimation.PlayAnimation("Idle");
                Debug.Log("Stopped!");
                autoForward = false;
                if (boat)
                {
                    audioSource.Stop();
                }
            }

            lastPos = transform.position;
        }
    }

    public void OnForward()
    {
        looping = true;
        destinationSet = false;
        autoForward = true;
        forwardSpeed = walkSpeed;
        flipDirection = false;
        CancelInvoke("FlipLoop");
        if(boat)
        {
            audioSource.Play();
        }
    }

    public void OnRun()
    {
        looping = true;
        destinationSet = false;
        autoForward = true;
        forwardSpeed = runSpeed;
        flipDirection = false;
        CancelInvoke("FlipLoop");
        if (boat)
        {
            audioSource.Play();
        }
    }
    public void OnIdle()
    {
        looping = false;
        tapped = false;
        autoForward = false;
        CancelInvoke("FlipLoop");
        if (boat)
        {
            audioSource.Stop();
        }
    }

    public void OnJump()
    {
        tapped = false;
        looping = false;

        autoForward = false;
        CancelInvoke("FlipLoop");
        //StartCoroutine(Jump());
    }

    public void activate()
    {
        activated = true;
        StartCoroutine(HideParticles());
    }

    void FlipLoop()
    {
        
        tapped = false;

        flipDirection = !flipDirection;
        if (flipDirection)
        {
            Debug.Log("Going up");
            destination = destinationUp;
        }
        else
        {
            Debug.Log("Going to center");
            destination = destinationCenter;

        }
        CopyRot.position = transform.position;
        CopyRot.LookAt(destination);
        startRotY = transform.eulerAngles.y;
        endRotY = CopyRot.transform.eulerAngles.y;
        t = 0;
    }  

    IEnumerator HideParticles()
    {
        yield return new WaitForSeconds(2);

        if (particleRootBlue.activeSelf)
        {
            foreach(ParticleSystem ps in blueSystem)
            {
                ps.enableEmission = false;
            }
            //particleRootBlue.GetComponentInChildren<Light>().enabled = false;
        }
        if (particleYellowRoot.activeSelf)
        {
            foreach (ParticleSystem ps in yellowSystem)
            {
                ps.enableEmission = false;
            }
           // particleYellowRoot.GetComponentInChildren<Light>().enabled = false;

        }
    }
}
