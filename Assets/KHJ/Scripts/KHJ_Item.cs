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
                weldingSceneMngr.instance.Audio.PlayOneShot(weldingSceneMngr.instance.SetBatterySound);
            }            
        }

        if(name == "Pipe")
        {
            if(other.name == "PipePos")
            {
                weldingSceneMngr.instance.isPipe = true;
                weldingSceneMngr.instance.PipeCanvas.SetActive(true);
                weldingSceneMngr.instance.PipeText.fontSize = 25;
                weldingSceneMngr.instance.PipeText.text = "용접 시작";
            }
        }

        if(name == "GhostBox")
        {
            if(other.gameObject.layer == 15)
            {
                LiftSceneMngr.instance.StageFail(FAIL_INDEX.FALLBOX);
            }
        }
        if (name == "GoalBox")
        {
            if (other.gameObject.layer == 14)
            {
                LiftSceneMngr.instance.StageSuccess();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }

    public void SetTarp()
    {
        weldingSceneMngr.instance.isTarp = true;
        weldingSceneMngr.instance.DeleteObj(GameObject.Find("Boxes"));
    }

}
