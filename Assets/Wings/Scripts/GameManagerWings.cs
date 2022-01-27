using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.Events;
public class GameManagerWings : MonoBehaviour
{
    public string[] sceneIndex; // only for editor view
    // Kanguru = 0 | Bear = 1 | Fox = 2 | Hippo = 3
    public int randomImage, randomListImage;
    public GameObject[] imageTarget;
    public GameObject TreasureImageTarget;
    public int[] randomArry;
    public Text text;
    public List<int> RandomList = new List<int>();
    GameObject selectedImageTarget;
    public UnityEngine.UI.Image DetectIndicator;
    public int imageIndex;
    Vector3 lastImagePos;
    public GameObject stage;
    public static int maxImages;
    public bool firstScene, oneByOne;
    public int sceneIndexW;
    public int loadLevelBypass;
    public int remainingImages;
    AudioSource audioSource;
    public AudioClip[] foundImageSFX, searchSFX; int SFXID;
    public bool fadeOutSFX, fadeToHalfSFX, fadeInSFX;
    string foundImageName;
    public float fadeOutFactor;
   
    void Start()
    {
        //DontDestroyOnLoad(this);
        //gameLevel = MainMenuManager.gameLevel;
        audioSource = GetComponent<AudioSource>();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        firstScene = true;
        if (maxImages == 0) maxImages = 11;
        remainingImages = maxImages;
        StartCoroutine(LoadFirstTarget());

    }
    IEnumerator LoadFirstTarget()
    {
        Clouds.FadeInClouds();
        yield return new WaitForSeconds(2);
        RandomizeImage();
    }
    // Update is called once per frame
    void Update()
    {
        if (fadeOutSFX)
        {
            if(audioSource.volume > 0)
                audioSource.volume -= Time.deltaTime * fadeOutFactor;
            else
            {
                fadeOutSFX = false;
            }
        }

        if (fadeToHalfSFX)
        {
            if (audioSource.volume > .4f)
                audioSource.volume -= Time.deltaTime * fadeOutFactor;
            else
            {
                fadeToHalfSFX = false;
            }
        }

        if (fadeInSFX)
        {
            if (audioSource.volume < 1f)
                audioSource.volume += Time.deltaTime * fadeOutFactor;
            else
            {
                fadeInSFX = false;
            }
        }
    }
    public void MyAction()
    {
        Debug.Log("Do Stuff");
    }
    void SelectTargetImageObject(int TargetId)
    {
        int index = 0;

        foreach (GameObject image in imageTarget)
        {
            if (TargetId == index)
            {
                Debug.Log("loading scene - " + index.ToString());
                SceneManager.LoadSceneAsync(index.ToString(), LoadSceneMode.Additive);

            }

            index++;
        }
    }
    
    public void RandomizeImage()
    {
        //Clouds.FadeInClouds();
        PlaySearchSFX();
        if (firstScene) // skip unloading for the first scene
        {
            //TempCam.SetActive(false);
            
            Debug.Log("Loading first scene");
            SceneManager.UnloadSceneAsync("MainMenu");
        }
        else
        {
            //TempCam.SetActive(true);
            SceneManager.UnloadSceneAsync(sceneIndexW.ToString());
            Debug.Log("Unloaded: " + sceneIndexW.ToString());
        }


        if (remainingImages > 0)
        {
            remainingImages--;
            // -- bypass
            if (oneByOne)
            {
                sceneIndexW++;
                //SceneManager.LoadSceneAsync(sceneIndexW.ToString()); //tempprary for testing one by one, not random
                StartCoroutine(LoadYourAsyncScene(randomListImage.ToString()));
                //
                return;
            }

            if (loadLevelBypass > -1)
            {
                Debug.Log("loaded from bypass");
                randomImage = loadLevelBypass;
                loadLevelBypass = -1;
            }
            else
            {
                randomImage = Random.Range(0, RandomList.Count - 1);// get a random number by List length
            }

            randomListImage = RandomList[randomImage]; // getting the image index from the list
            //SceneManager.LoadSceneAsync(randomListImage.ToString(), LoadSceneMode.Additive); // loading 
            StartCoroutine(LoadYourAsyncScene(randomListImage.ToString()));
            sceneIndexW = randomListImage;
            RandomList.Remove(RandomList[randomImage]); // removing the item from the list

        }
        else
        {
            Debug.Log("Loading Treasure");
            //SceneManager.LoadSceneAsync("Treasure", LoadSceneMode.Additive);
            StartCoroutine(LoadYourAsyncScene("Treasure"));

        }

    }
    IEnumerator LoadYourAsyncScene(string sceneID)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        //if (firstScene) // skip unloading for the first scene
        //{
        //    SceneManager.UnloadSceneAsync("MainMenu");
        //}

        yield return new WaitForSeconds(2f);
        if (firstScene)
        {
            firstScene = false;

        }
        //else
        {
            Clouds.FadeOutClouds();
        }
        Debug.Log("Loaded scene: "+ sceneID);
    }
    public void SetText(string ImageText)
    {
        text.text = "image ID: " + RandomList[randomImage] + ImageText;
    }

    public void ImageDetected(string imageName, Vector3 pos, GameObject model)
    {
        foundImageName = imageName;
        DetectIndicator.color = Color.green;
        Debug.Log("Detected");
        text.text = "image detected name: " + imageName;
        lastImagePos = pos;
        selectedImageTarget = model;
        fadeOutFactor = 1;
        fadeOutSFX = true;
        StartCoroutine(PlayFoundSFX());
    }

    public void PlayFindImageSFX()
    {
        fadeOutSFX = false;
        audioSource.volume = 1;
        Debug.Log("PlayFindImageSFX: " + foundImageName);
        if (foundImageName == "Boat")
        {
            audioSource.clip = foundImageSFX[2];
        }
        else
        {
            if (foundImageName == "Treasure")
            {
                audioSource.clip = foundImageSFX[3];
            }
            else
            {
                if (SFXID == 2)
                    audioSource.clip = foundImageSFX[1];
                else
                    audioSource.clip = foundImageSFX[SFXID];
            }

            
        }
        //SFXID = 2;
        audioSource.Play();
        //SFXID++;
        //if (SFXID > 1)
        //    SFXID = 0;
        SFXID++;
    }

    void PlaySearchSFX()
    {
        if (SFXID > 2)
            SFXID = 0;
        fadeOutSFX = false;
        audioSource.volume = 1;
        audioSource.clip = searchSFX[SFXID];
        audioSource.Play();
        StartCoroutine(FadeToHalf());   
    }

    IEnumerator PlayFoundSFX() //after 0.5 search fadeout
    {
        yield return new WaitForSeconds(0.5f);
        fadeOutFactor = 1;
        PlayFindImageSFX();
    }

    IEnumerator FadeToHalf()
    {
        yield return new WaitForSeconds(1f);
        fadeOutFactor = 1;
        fadeToHalfSFX = true;
    }
}
