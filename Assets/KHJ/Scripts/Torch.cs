using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        //베터리가 껴져있고, 손에 들고있다면
        if (weldingSceneMngr.instance.isBattery && GetComponent<KHJ_Item>().isGrab)
        {

        }
    }
}
