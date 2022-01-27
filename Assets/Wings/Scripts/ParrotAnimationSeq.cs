using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrotAnimationSeq : MonoBehaviour
{
    public Animation animation;
    bool walked, moveTo;
    Vector3 target;
    float X, Y, Z;
    void Start()
    {

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveTo)
        {
            transform.localPosition = new Vector3(transform.localPosition.x+X, transform.localPosition.y+Y, transform.localPosition.z+Z);
        }
    }
    private void OnEnable()
    {
        animation = GetComponent<Animation>();
        StartCoroutine(ParrotAnimation());
    }
    IEnumerator ParrotAnimation()
    {
        animation.Play("Macaw_|Idle_02");
        yield return new WaitForSeconds(3);
        
        animation.Play("Macaw_|Take_off");
        yield return new WaitForSeconds(1);

        StartCoroutine(FlyUp());
        yield return new WaitForSeconds(1);

        animation.Play("FlyInPlace");
        yield return new WaitForSeconds(4);

        yield return new WaitForSeconds(1);
        animation.Blend("Macaw_|Landing");

        yield return new WaitForSeconds(1);
        StartCoroutine(FlyDown());

        yield return new WaitForSeconds(1);
        animation.Play("Macaw_|Idle_02");

        yield return new WaitForSeconds(3);
        animation.Play("Walk");
        if(walked)
            StartCoroutine(WalkLeft());
        else
            StartCoroutine(WalkRight());

        walked = !walked;

        yield return new WaitForSeconds(5);
        animation.Play("Eat");

        yield return new WaitForSeconds(5);

        StartCoroutine(ParrotAnimation());

    }

    IEnumerator FlyUp()
    {
        moveTo = true;
        X = Y = Z = 0;
        Y = Time.deltaTime*.5f;
        yield return new WaitForSeconds(1);
        moveTo = false;
    }

    IEnumerator FlyDown()
    {
        moveTo = true;
        X = Y = Z = 0;
        Y = -Time.deltaTime*.5f;
        yield return new WaitForSeconds(1);
        moveTo = false;
    }

    IEnumerator WalkLeft()
    {
        moveTo = true;
        X = Y = Z = 0;
        X = -Time.deltaTime*.1f;
        yield return new WaitForSeconds(5);
        moveTo = false;

    }

    IEnumerator WalkRight()
    {
        moveTo = true;
        X = Y = Z = 0;
        X = -Time.deltaTime*.1f;
        yield return new WaitForSeconds(5);
        moveTo = false;
    }
}
