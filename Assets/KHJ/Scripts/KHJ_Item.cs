using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHJ_Item : MonoBehaviour
{
    public bool isGrab;

    public bool iscombustibles;


    void Start()
    {
        
    }

    void Update()
    {
        if (isGrab)
            print(name);



    }
    public void DisappearItem()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Torch" && other.GetComponent<KHJ_Item>().isGrab)
        {
            if(name == "Battery" && GetComponent<KHJ_Item>().isGrab)
            {
                print("isBattery");
                weldingSceneMngr.instance.isBattery = true;
            }            
        }

        if(name == "Pipe")
        {
            if(other.name == "PipePos")
            {
                weldingSceneMngr.instance.isPipe = true;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        
    }


}
