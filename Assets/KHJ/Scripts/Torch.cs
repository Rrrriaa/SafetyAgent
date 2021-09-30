using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    Animator ani;
    public GameObject Flame;
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        //���͸��� �����ְ�, �տ� ����ִٸ�
        if (GetComponent<KHJ_Item>().isGrab)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.Touch))
            { 
                ani.SetTrigger("On");
                if(weldingSceneMngr.instance.isBattery)
                    Flame.SetActive(true);
            }
            if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.Touch))
            {
                ani.SetTrigger("Off");
                Flame.SetActive(false);
            }
        }
    }
}
