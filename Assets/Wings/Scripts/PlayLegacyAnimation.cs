using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayLegacyAnimation : MonoBehaviour
{
    Animation anim;
    float jumpTime;
    public AnimationClip jumpClip;
    //Vector3 currnentPos, lastPos;
    //string currentAnim;
    //bool MoveWalk, MoveRun;
    //float vertical;
    //public string walkAnim, idleAnim, runAnim;
    void Start()
    {
        anim = GetComponent<Animation>();
        //currentAnim = "idleAnim";
        anim.Play("Idle"); 
    }

    // Update is called once per frame
    void Update()
    {


    }
    public void PlayAnimation(string animName)
    {
        Debug.Log("playing: " + animName);
        StopCoroutine("Jump");
        anim.CrossFade(animName);
        if(animName == "Jump")
        {
            StartCoroutine(Jump());
        }
        ///currentAnim = animName;
    }

    IEnumerator Jump()
    {
        jumpTime = jumpClip.length;
        yield return new WaitForSeconds(jumpTime);
        anim.Play("Idle");
    }

}
