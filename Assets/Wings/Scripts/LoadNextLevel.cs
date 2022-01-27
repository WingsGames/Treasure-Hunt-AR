using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadNextLevel : MonoBehaviour
{
    public bool fadein, fadeout;
    float alpha = 1f;
    Color newColor;
    public SkinnedMeshRenderer[] skinnedMeshRenderers;
    public MeshRenderer[] meshRenderers;
    public GameObject model;
    public GameObject homeCanvas;
    public GameObject buttons;
    public AudioSource aboutMeAudio;
    public Animator fadeAnim;
    public void NextLevel()
    {
        StartCoroutine(FadeOutSequence());

        StaticVars.SaveHeartsAndDiamonds();
        if(model == null)
            model = FindObjectOfType<MovementManager>().gameObject;
        model.transform.GetComponentInChildren<AudioSource>().Stop();
    }
    private void Start()
    {
        if(model == null)
            model = FindObjectOfType<MovementManager>().gameObject;
        homeCanvas.SetActive(false);
    }
    private void Update()
    {
        if (fadeout)
        {
            if (alpha > 0)
            {
                alpha -= Time.deltaTime;
                //Debug.Log(alpha);
                newColor = new Color(1, 1, 1, alpha);
                
                foreach (SkinnedMeshRenderer skinMeshRenderer in skinnedMeshRenderers)
                {
                    skinMeshRenderer.material.color = newColor;
                }
                
            }
            else
            {
                fadeout = false;
            }

        }   
    }

    IEnumerator FadeOutSequence()
    {

        Button[] allbuttons = buttons.GetComponentsInChildren<Button>();
        if(model != null)
            model.GetComponent<MovementManager>().OnIdle();
        foreach (Button button in allbuttons)
        {
            button.interactable = false;
        }

        aboutMeAudio.Stop();
        fadeout = true;
        GetComponent<AudioSource>().Play();
        GameObject.Find("GameManager").GetComponent<GameManagerWings>().fadeOutFactor = .25f;
        GameObject.Find("GameManager").GetComponent<GameManagerWings>().fadeOutSFX = true;

        yield return new WaitForSeconds(4);
        Clouds.FadeInClouds();
        //fadeAnim.SetTrigger("FadeToBlack");
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(1f);
        GameObject.Find("GameManager").GetComponent<GameManagerWings>().RandomizeImage();

    }

    public void RestartApp()
    {
        StaticVars.SaveHeartsAndDiamonds();
        SceneManager.LoadScene(0);
    }

    public void Home()
    {
        homeCanvas.SetActive(true);
    }
}
