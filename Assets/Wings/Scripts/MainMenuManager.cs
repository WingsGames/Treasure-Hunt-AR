using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuManager : MonoBehaviour
{
    //public GameObject[] menuItem;
    //public static int gameLevel;
    AudioSource audioSource;
    bool fadeAudio, looping;
    public AudioClip loopAudio;
    public GameObject scroll, scrollTxt;
    public GameObject shortPath, longPath, closeScroll, loadingText;
    public Color pressedColor;
    bool loadingLevel;
    public StepsManager StepsManagerShort, stepsManagerLong;
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        scroll.SetActive(false);
        scrollTxt.SetActive(false);
        closeScroll.SetActive(false);
        shortPath.SetActive(false);
        longPath.SetActive(false);
        loadingText.SetActive(false);
        //SceneManager.LoadSceneAsync("Clouds", LoadSceneMode.Additive);
        //menuItem[1].SetActive(false);
    }

    void Update()
    {
        if (!audioSource.isPlaying && !looping)
        {
            looping = true;
            audioSource.clip = loopAudio;
            audioSource.loop = true;
            audioSource.Play();
        }
        if (fadeAudio)
        {
            audioSource.volume -= Time.deltaTime;
        }
    }

    public void OnButtonPressed(int buttonID)
    {
        fadeAudio = true;
        StartCoroutine(LoadGame(buttonID));
    }

    void SetVisibility(GameObject item, bool visibility)
    {
        item.SetActive(visibility);
    }

    IEnumerator LoadGame(int id)
    {
        yield return new WaitForSeconds(.1f);
       // StartCoroutine(ScrollMenuClose());
        //loadingText.SetActive(true);

       // yield return new WaitForSeconds(1f);
        
        switch (id)
        {
            case 0:
                //SetVisibility(menuItem[0], false);
                //SetVisibility(menuItem[1], true);
                break;
            case 1:
                //gameLevel = 1;
                GameManagerWings.maxImages = 4;
                StepsManagerShort.animate = true;
                //SceneManager.LoadScene("Game");
                break;
            case 2:
                //gameLevel = 2;
                GameManagerWings.maxImages = 8;
                stepsManagerLong.animate = true;
                //SceneManager.LoadScene("Game");
                break;
            case 3:
                //gameLevel = 3;
                GameManagerWings.maxImages = 11;
                //SceneManager.LoadScene("Game");
                break;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void OnBottlePressed()
    {
        if (loadingLevel) return;
        shortPath.GetComponent<SpriteRenderer>().color = longPath.GetComponent<SpriteRenderer>().color = Color.white;
        if (!scroll.activeSelf)
            StartCoroutine(ScrollMenuOpen());
        else
            StartCoroutine(ScrollMenuClose());
    }

    IEnumerator ScrollMenuOpen()
    {
        scroll.SetActive(true);
        scrollTxt.SetActive(true);
        scroll.GetComponent<Animation>().Play("Open");
        scrollTxt.GetComponent<Animation>().Play("Open");
        yield return new WaitForSeconds(.2f);
        closeScroll.SetActive(true);
        shortPath.SetActive(true);
        longPath.SetActive(true);

    }
    IEnumerator ScrollMenuClose()
    {
        scroll.GetComponent<Animation>().Play("ScrollClose");
        scrollTxt.GetComponent<Animation>().Play("ScrollClose");
        closeScroll.SetActive(false);
        shortPath.SetActive(false);
        longPath.SetActive(false);
        yield return new WaitForSeconds(.2f);
        scroll.SetActive(false);
        scrollTxt.SetActive(false);
        closeScroll.GetComponent<SpriteRenderer>().color = Color.white;



    }

    public void OnSpritePressed(int id)
    {
        switch (id)
        {
            case 0:
                loadingLevel = true;
                shortPath.GetComponent<SpriteRenderer>().color = pressedColor;
                longPath.GetComponent<SpriteRenderer>().color = Color.white;
                OnButtonPressed(1);
                ScrollMenuClose();
                break;
            case 1:
                loadingLevel = true;
                longPath.GetComponent<SpriteRenderer>().color = pressedColor;
                shortPath.GetComponent<SpriteRenderer>().color = Color.white;
                OnButtonPressed(2);
                ScrollMenuClose();
                break;
            case 10:
                closeScroll.GetComponent<SpriteRenderer>().color = pressedColor;
                OnBottlePressed();
                break;
        }
        
    }
}
