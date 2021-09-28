using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHJ_Item : MonoBehaviour
{
    public bool isGrab;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void DisappearItem()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Torch")
        {
            if(gameObject.name == "Battery")
            {
                weldingSceneMngr.instance.isBattery = true;
            }            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }


}
