using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forklift : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            LiftSceneMngr.instance.isRide = true;
        }
        if(other.gameObject.name.Contains("WorkMan"))
        {
            LiftSceneMngr.instance.StageFail(FAIL_INDEX.HUMAN);
        }
    }
}
