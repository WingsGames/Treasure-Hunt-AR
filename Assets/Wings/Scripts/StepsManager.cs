using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StepsManager : MonoBehaviour
{
    public bool animate, vis;
    float timer;
    public Sprite[] normalSprite, glowSprite;
    public float repeatTime;
    void Start()
    {
        foreach (Image go in gameObject.GetComponentsInChildren<Image>())
        {
            //go.enabled = false;// (false);
            go.sprite = normalSprite[Random.Range(0, 3)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (animate)
        {
            animate = false;
            //vis = !vis;
            timer = 0;

            foreach (Image go in gameObject.GetComponentsInChildren<Image>(true))
            {
                timer += .1f;//Random.Range(.1f,.1f);
                StartCoroutine(SetVisible(go, timer));
            }
            StartCoroutine(RepeatPlay());
        }
    }

    IEnumerator SetVisible(Image go, float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Play step");
        go.GetComponent<Animator>().SetTrigger("PlayW");
    }

    IEnumerator RepeatPlay()
    {
        yield return new WaitForSeconds(repeatTime);
        animate = true;
    }
}
