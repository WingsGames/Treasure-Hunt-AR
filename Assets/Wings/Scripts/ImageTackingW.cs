using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]

public class ImageTackingW : MonoBehaviour
{
    [SerializeField]
    //private GameObject[] placedPrefabs;
    //private Dictionary<string,GameObject> spawnedPrefabs = new Dictionary<string,GameObject>();
    private ARTrackedImageManager trackedImageManager;
    // Start is called before the first frame update
    public string imageName, imageName2, imageName3;
    public ObjectSpawner objectSpawner;
    public GameObject MainSurface, m_InfinitePlane, m_InfinitePlanePrefab;

    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        FindObjectOfType<ARSession>().Reset();

    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach(ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
            Debug.Log("eventArgs.added: " + trackedImage.referenceImage.name);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            //Debug.Log("eventArgs.updated: " + trackedImage.name);
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            Debug.Log("Image removed: " + trackedImage.name);
            //spawnedPrefabs[trackedImage.name].SetActive(false);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        if (trackedImage.referenceImage.name == imageName)
        {
            objectSpawner.trackedImage = trackedImage.gameObject;
            return;
        }

        if (trackedImage.referenceImage.name == imageName2)
        {
            objectSpawner.trackedImage = trackedImage.gameObject;
            return;
        }

        if (trackedImage.referenceImage.name == imageName2)
        {
            objectSpawner.trackedImage = trackedImage.gameObject;
            return;
        }

        // if no image name than hide
        trackedImage.gameObject.SetActive(false);
            

    }

    
    private void Update()
    {

    }
}
