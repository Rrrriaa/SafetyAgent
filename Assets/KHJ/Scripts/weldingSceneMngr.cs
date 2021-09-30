using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FAIL_INDEX
{
    HELMET,
    FIRE
}

public class weldingSceneMngr : MonoBehaviour
{
    public static weldingSceneMngr instance;

    public GameObject Torch;
    public GameObject Battery;
    public bool isBattery;
    public GameObject Pipe;
    public GameObject PipePos;
    public bool isPipe;

    public bool isHelmet;
    public bool isMask;
    
    public bool isWelding;
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
    }

    void Update()
    {
        Test();
        SetBattery();
        SetPipe();
    }

    void SetBattery()
    {
        if(Torch.GetComponent<KHJ_Item>().isGrab || Battery.GetComponent<KHJ_Item>().isGrab)
        {
            if (isBattery)
            {
                //부딪힌 물체를 오른손의 자식으로 한다
                Battery.transform.parent = Torch.transform;
                Battery.transform.position = Torch.transform.position;
                Battery.transform.rotation = Torch.transform.rotation;
                Battery.layer = 0;
                Battery.GetComponent<Collider>().enabled = false;
                Battery.GetComponent<Rigidbody>().isKinematic = true;
                Torch.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }
    void SetPipe()
    {
        if (isPipe)
        {
            Pipe.transform.position = PipePos.transform.position;
            Pipe.transform.rotation = PipePos.transform.rotation;

            Pipe.GetComponent<Rigidbody>().isKinematic = true;
            Pipe.layer = 0;
        }
    }


    void Test()
    {
        isBattery = true;
        Battery.transform.parent = Torch.transform;
        Battery.transform.position = Torch.transform.position;
        Battery.transform.rotation = Torch.transform.rotation;
        Battery.layer = 0;
        Battery.GetComponent<Collider>().enabled = false;
        Battery.GetComponent<Rigidbody>().isKinematic = true;
        Torch.GetComponent<Rigidbody>().isKinematic = true;
    }


    public void StageFail(FAIL_INDEX index)
    {
        if(index == FAIL_INDEX.HELMET)
            print("보호장비 미착용으로 인한 부상");
        if (index == FAIL_INDEX.FIRE)
            print("발화");
    }

}
