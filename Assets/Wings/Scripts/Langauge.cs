using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Langauge : MonoBehaviour
{
    // Start is called before the first frame update
    public Dropdown dropdown;
    public GameObject txtHeb, txtEng;
    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        SelectItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectItem()
    {
        if (dropdown.value == 0)
        {
            txtHeb.SetActive(true);
            txtEng.SetActive(false);
        }
        else
        {
            txtHeb.SetActive(false);
            txtEng.SetActive(true);

        }
    }
}
