using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerRot();
    }

    void playerRot()
    {
        transform.rotation = Quaternion.Euler(rot);
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.RTouch))
        {
            rotPlus();
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.RTouch))
        {
            rotMinus();
        }


    }

    Vector3 rot;
    void rotPlus()
    {
        rot.y += 45;
    }
    void rotMinus()
    {
        rot.y -= 45;
    }




}
