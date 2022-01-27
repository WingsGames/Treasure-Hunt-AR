using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AnchorsW : MonoBehaviour
{
    // Start is called before the first frame update
    ARAnchorManager anchorManager = null;
    ARRaycastManager raycastManager = null;
    ARSessionOrigin origin = null;
    ARPlaneManager planeManager = null;
    TrackableType raycastMask = TrackableType.PlaneWithinPolygon;
    [SerializeField] int fontSize = 35;

    public bool attachCenterAnchor;//wings
    public ARAnchor lastAnchor;//wings
    public ARAnchor centerAnchor;//wings
                                 //public Vector3 imagePos;//wings
    public Transform image;
    AnchorTestType type = AnchorTestType.Add;

    private void Start()
    {
        
    }

    void OnEnable()
    {
        anchorManager = FindObjectOfType<ARAnchorManager>();
        raycastManager = FindObjectOfType<ARRaycastManager>();
        origin = FindObjectOfType<ARSessionOrigin>();
        planeManager = FindObjectOfType<ARPlaneManager>();

    }

    void OnDisable()
    {
        anchorManager = null;
        raycastManager = null;
        origin = null;
        planeManager = null;
    }


    void Update()
    {
        if (attachCenterAnchor)  //wings all this part
        {
            if(image == null)
            {
                attachCenterAnchor = false;
            }
            Debug.Log("Trying to center anchor");
            //var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            var ray = new Ray(image.transform.position, Camera.main.transform.position - image.transform.position);
            var hits = new List<ARRaycastHit>();
            var hasHit = raycastManager.Raycast(ray, hits, raycastMask);

            Debug.DrawRay(image.transform.position, Camera.main.transform.position - image.transform.position, Color.green);
            if (hasHit)
            {

                switch (type)
                {
                    case AnchorTestType.Add:
                        {
#pragma warning disable 618
                            Debug.Log("hit: " + hits.First());
                            var anchor = anchorManager.AddAnchor(hits.First().pose);
                            centerAnchor = anchor;
                            lastAnchor = anchor;//wings
                            attachCenterAnchor = false;//wings
#pragma warning restore
                            print($"anchor added: {anchor != null}");
                            break;
                        }
                    case AnchorTestType.AttachToPlane:
                        {
                            var attachedToPlane = tryAttachToPlane(hits);
                            print($"anchor attached successfully: {attachedToPlane}");
                            break;
                        }
                        // default:
                        //    throw new Exception();
                }
                //return;
            }
            else
            {
                var ray2 = new Ray(image.transform.position, Vector3.down);
                var hits2 = new List<ARRaycastHit>();
                var hasHit2 = raycastManager.Raycast(ray2, hits2, raycastMask);

                Debug.DrawRay(image.transform.position, Vector3.down, Color.yellow);
                if (hasHit2)
                {

                    switch (type)
                    {
                        case AnchorTestType.Add:
                            {
#pragma warning disable 618
                                Debug.Log("hit: " + hits2.First());
                                var anchor = anchorManager.AddAnchor(hits.First().pose);
                                centerAnchor = anchor;
                                lastAnchor = anchor;//wings
                                attachCenterAnchor = false;//wings
#pragma warning restore
                                print($"anchor added: {anchor != null}");
                                break;
                            }
                        case AnchorTestType.AttachToPlane:
                            {
                                var attachedToPlane = tryAttachToPlane(hits);
                                print($"anchor attached successfully: {attachedToPlane}");
                                break;
                            }
                        default:
                            throw new Exception();
                    }
                    return;
                }
            }

        }
        ///--- till here
        for (int i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            if (touch.phase != TouchPhase.Began)
            {
                continue;
            }

            var ray = origin.camera.ScreenPointToRay(touch.position);
            var hits = new List<ARRaycastHit>();
            var hasHit = raycastManager.Raycast(ray, hits, raycastMask);
            if (hasHit)
            {
                switch (type)
                {
                    case AnchorTestType.Add:
                        {
#pragma warning disable 618
                            var anchor = anchorManager.AddAnchor(hits.First().pose);
#pragma warning restore
                            lastAnchor = anchor;//wings
                            print($"anchor added: {anchor != null}");
                            break;
                        }
                    case AnchorTestType.AttachToPlane:
                        {
                            var attachedToPlane = tryAttachToPlane(hits);
                            print($"anchor attached successfully: {attachedToPlane}");
                            break;
                        }
                    default:
                        throw new Exception();
                }
            }
            else
            {
                // print("no hit");
            }
        }
    }

    bool tryAttachToPlane(List<ARRaycastHit> hits)
    {
        foreach (var hit in hits)
        {
            var plane = planeManager.GetPlane(hit.trackableId);
            if (plane != null)
            {
                var anchor = anchorManager.AttachAnchor(plane, hit.pose);

                if (anchor != null)
                {
                    return true;
                }
            }
        }

        return false;
    }

    //void OnGUI() {
    //    var h = 200;
    //    var y = Screen.height;

    //    var style = new GUIStyle(GUI.skin.button) {fontSize = fontSize};

    //    y -= h;
    //    if (GUI.Button(new Rect(0, y,400,h), $"Current type: {type}", style)) {
    //        type = type == AnchorTestType.Add ? AnchorTestType.AttachToPlane : AnchorTestType.Add;
    //    }

    //    y -= h;
    //    if (GUI.Button(new Rect(0, y, 400, h), "Remove all anchors", style)) {
    //        removeAllAnchors();
    //    }
    //}

    void removeAllAnchors()
    {
        var copiedAnchors = new HashSet<ARAnchor>();
        foreach (var _ in anchorManager.trackables)
        {
            copiedAnchors.Add(_);
        }

        foreach (var anchor in copiedAnchors)
        {
            if (anchor == null)
            {
                continue;
            }

        }
    }

    enum AnchorTestType
    {
        Add,
        AttachToPlane
    }
}

