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
        //���͸��� �����ְ�, �տ� ����ִٸ�
        if (weldingSceneMngr.instance.isBattery && GetComponent<KHJ_Item>().isGrab)
        {

        }
    }
}
