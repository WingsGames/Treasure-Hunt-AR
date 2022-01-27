using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TargetImageWings : MonoBehaviour
{
    // Start is called before the first frame update
    public string imageName;
    public GameObject Model;
    public GameObject stage;
    public GameObject gameButtons, riddle, scroll, scrollBG, planeDetector;
    //public int power;
    GameObject gameManagerWings ;
    public bool fakeDetect;
    public bool isMesh;
    public bool fadein, fadeout;
    public float alpha = 0f;
    Color newColor;
    public Text[] texts;
    AudioSource audioSource;
    public float timescale = 5f;
    public AudioClip riddleAudio;
    public GameObject skipButotn;
    public ObjectSpawner objectSpawner;
    public bool skipRiddle, solo;
    public Button nextLvl;
    private void Start()
    {
        if(nextLvl != null)
        {
            nextLvl.interactable = false;
            StartCoroutine(ActivateNextLvlButton());
        }
        newColor = new Color(0, 0, 0, 0);
        foreach (Text text in texts)
        {
            text.color = newColor;
        }
        //fadein = true;
        audioSource = riddle.GetComponent<AudioSource>();
        planeDetector = riddle.transform.parent.Find("PlaneDetector").gameObject;
        planeDetector.SetActive(false);


        Model.SetActive(false);
        stage = GameObject.Find("Stage");

        if(!solo)gameButtons = GameObject.Find("CanvasLevel").transform.Find("GameButtons").gameObject;
        riddle = GameObject.Find("CanvasLevel").transform.Find("Riddle").gameObject;
        audioSource = riddle.GetComponent<AudioSource>();
        gameButtons.SetActive(false);

        gameManagerWings = GameObject.Find("GameManager");
        if(gameManagerWings != null)
        {
            gameManagerWings.GetComponent<GameManagerWings>().text.text = "image ID: " + imageName;
            gameManagerWings.GetComponent<GameManagerWings>().DetectIndicator.color = Color.red;
        }

        if (skipRiddle)
            StartCoroutine(SkipRiddle());
        else
            StartCoroutine(RiddleSequence());

    }
    IEnumerator ActivateNextLvlButton()
    {
        yield return new WaitForSeconds(30);
        nextLvl.interactable = true;
        //nextLvl.transform.parent = nextLvl.transform.parent.transform.parent;


    }
    public void AskRiddle()
    {
        StopCoroutine(RiddleAgain());
        StartCoroutine(RiddleAgain());
    }
    public void ImageDetectedWings()
    {

        //Model.SetActive(true);
        gameButtons.SetActive(true);
        nextLvl.interactable = true;

        if (gameManagerWings != null)
            gameManagerWings.GetComponent<GameManagerWings>().ImageDetected(imageName, Model.transform.position, gameObject);

        
    }
    public void NextLevel()
    {
        if (gameManagerWings != null)
            gameManagerWings.GetComponent<GameManagerWings>().RandomizeImage();
    }
    private void Update()
    {
        if (fakeDetect)
        {
            
            fakeDetect = false;
            Model.SetActive(true);
            Model.GetComponent<MovementManager>().activated = true;
            if (isMesh)
            {
                Component[] meshRenderers;
                meshRenderers = Model.transform.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer meshRenderer in meshRenderers)
                {
                    meshRenderer.enabled = true;
                }
            }
            else
            {
                Component[] meshRenderers;
                meshRenderers = Model.transform.GetComponentsInChildren<SkinnedMeshRenderer>();
                foreach (SkinnedMeshRenderer meshRenderer in meshRenderers)
                {
                    meshRenderer.enabled = true;
                }
            }

            gameButtons.SetActive(true);
            if(gameManagerWings !=null)
                gameManagerWings.GetComponent<GameManagerWings>().ImageDetected(imageName, Model.transform.position, gameObject);
        }

        if (fadeout)
        {
            if (alpha > 0)
            {
                alpha -= Time.deltaTime * timescale;
                //Debug.Log(alpha);
                newColor = new Color(0, 0, 0, alpha);
                foreach (Text text in texts)
                {
                    text.color = newColor;
                }
                newColor = new Color(1, 1, 1, alpha);
            }
            else
            {
                fadeout = false;
            }

        }
        if (fadein)
        {
            if (alpha < 1)
            {
                alpha += Time.deltaTime * timescale;
                //Debug.Log(alpha);
                newColor = new Color(0, 0, 0, alpha);
                foreach (Text text in texts)
                {
                    text.color = newColor;
                }
                newColor = new Color(1, 1, 1, alpha);
            }
            else
            {
                fadein = false;
                audioSource.Play();
            }
        }
    }
    IEnumerator RiddleAgain()
    {
        alpha = 0;
        scroll.SetActive(true);
        scrollBG.SetActive(true);

        scroll.GetComponent<Animation>().Play("Open");
        scrollBG.GetComponent<Animation>().Play("Open");

        skipButotn.SetActive(true);
        objectSpawner.disable = true;
        Debug.Log("Riddle Sequence");
        //yield return new WaitForSeconds(2.2f);
        //scroll.GetComponent<Animation>().Play("Take 001");
        yield return new WaitForSeconds(.1f);
        Debug.Log("Riddle Sequence fadein");
        fadein = true;
        yield return new WaitForSeconds(riddleAudio.length);
        gameManagerWings.GetComponent<GameManagerWings>().fadeInSFX = true; // setting volume back to 1
        Debug.Log("Riddle Sequence fadeout");
        fadeout = true;
        yield return new WaitForSeconds(2);
        Debug.Log("Riddle Sequence ScrollClose");
        scroll.GetComponent<Animation>().Play("ScrollClose");
        scrollBG.GetComponent<Animation>().Play("ScrollClose");

        yield return new WaitForSeconds(1.5f);
        Debug.Log("scroll.SetActive(false)");
        objectSpawner.disable = false;
        scroll.SetActive(false);
        scrollBG.SetActive(false);

        skipButotn.SetActive(false);
        audioSource.Stop();
        planeDetector.SetActive(true);

    }
    IEnumerator RiddleSequence() 
    {
        yield return new WaitForSeconds(3f);
        scroll.transform.parent.GetComponent<Animator>().SetTrigger("FlyIn");
        scrollBG.transform.parent.GetComponent<Animator>().SetTrigger("FlyIn");

        objectSpawner.disable = true;
        Debug.Log("Riddle Sequence");
        yield return new WaitForSeconds(2.2f);
        scroll.GetComponent<Animation>().Play("Open");
        scrollBG.GetComponent<Animation>().Play("Open");

        yield return new WaitForSeconds(.1f);
        Debug.Log("Riddle Sequence fadein");
        fadein = true;
        yield return new WaitForSeconds(riddleAudio.length);
        if(gameManagerWings != null)
            gameManagerWings.GetComponent<GameManagerWings>().fadeInSFX = true; // setting volume back to 1

        Debug.Log("Riddle Sequence fadeout");
        fadeout = true;
        yield return new WaitForSeconds(2);
        Debug.Log("Riddle Sequence ScrollClose");
        scroll.GetComponent<Animation>().Play("ScrollClose");
        scrollBG.GetComponent<Animation>().Play("ScrollClose");

        yield return new WaitForSeconds(1.5f);
        Debug.Log("scroll.SetActive(false)");
        objectSpawner.disable = false;
        scroll.SetActive(false);
        scrollBG.SetActive(false);

        skipButotn.SetActive(false);
        audioSource.Stop();
        planeDetector.SetActive(true);

    }

    IEnumerator SkipRiddle()
    {
        Debug.Log("skip riddle");
        StopCoroutine(RiddleSequence());
        audioSource.Stop();
        scroll.GetComponent<Animation>().Stop();
        scroll.SetActive(false);
        scrollBG.GetComponent<Animation>().Stop();
        scrollBG.SetActive(false);
        fadein = false;
        fadeout = true;
        skipButotn.SetActive(false);
        fadeout = true;
        yield return new WaitForSeconds(.1f);
        objectSpawner.disable = false;
        planeDetector.SetActive(true);
    }

    public void OnSkipRiddle()
    {
        StopAllCoroutines();
        StartCoroutine(SkipRiddle());

    }
}
