using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour
{
    public GameObject[] Fire;
    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "TorchFire")
        {
            ActiveFireEft();
            weldingSceneMngr.instance.StageFail(FAIL_INDEX.FIRE);
        }
    }

    void ActiveFireEft()
    {
        for(int i = 0; i < Fire.Length; i++)
        {
            Fire[i].SetActive(true);
        }
    }
}
