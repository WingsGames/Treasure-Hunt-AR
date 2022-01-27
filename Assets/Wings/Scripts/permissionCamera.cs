using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class permissionCamera : MonoBehaviour
{
    //public bool ChedckPermission;
    //public bool permit;
    // Start is called before the first frame update
    public bool permitted;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            permitted = false;
        }
        else
        {
            permitted = true;
        }
    }
    public void ShowPermissionDialog()
    {
        Permission.RequestUserPermission(Permission.Camera);
    }

    public void CheckPermission(bool permit)
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            permit = false;
        }
        else
        {
            permit = true;
            permitted = true;
        }
    }
}
