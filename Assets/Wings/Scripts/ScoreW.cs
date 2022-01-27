using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreW : MonoBehaviour
{
    public int score;
    public bool hearts, diamonds;
    // Start is called before the first frame update

    public void AddScore()
    {
        score++;
        GetComponent<Text>().text = score.ToString();
        if (hearts)
        {
            StaticVars.Hearts += 10;
        }
        if (diamonds)
        {
            StaticVars.Diamonds += 10;

        }

    }
}
