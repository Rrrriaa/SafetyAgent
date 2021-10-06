using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_00_Fail_Rio : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag  == "Player" && gameObject.name == "FailPoint")
        {
            other.GetComponent<SceneMngr_Rio>().StageFail();
        }

        if(gameObject.name == "ScreamPoint")
        {
            other.GetComponent<SceneMngr_Rio>().Scream();
        }
    }
}
