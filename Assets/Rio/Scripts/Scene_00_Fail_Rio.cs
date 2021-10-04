using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_00_Fail_Rio : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag  == "Player")
        {
            other.GetComponent<SceneMngr_Rio>().StageFail();
        }
    }
}
