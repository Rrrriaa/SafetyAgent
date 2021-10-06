using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class weldingSceneMngr : KHJ_SceneMngr
{
    public static weldingSceneMngr instance;

    public GameObject Torch;
    public GameObject Battery;
    public bool isBattery;
    public GameObject Pipe;
    public GameObject PipeS;
    public GameObject PipePos;
    public bool isPipe;
    public GameObject PipeCanvas;
    public Image PipeProgressImg;
    public Text PipeText;

    public bool isHelmet;
    public bool isMask;
    public bool isTarp;
    
    public bool isWelding;
    public Burn[] burnArray;

    public AudioClip SetBatterySound;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
   
    public float currTime = 0;
    float FireTime = 2;
    float SuccessTime = 10;
    void Update()
    {
        //Test();
        SetBattery();
        SetPipe();
        //currTime += Time.deltaTime;
        if (isWelding)
        {
            PipeText.fontSize = 30;
            currTime += Time.deltaTime;
            float leftTime = SuccessTime - currTime;
            if(leftTime < 0)
            {
                leftTime = 0;
                PipeCanvas.SetActive(false);
            }
            PipeProgressImg.fillAmount = currTime / SuccessTime;
            string str = string.Format("{0:0.0}", leftTime);
            PipeText.text = str;
        }
        CheckFire();
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


    void CheckFire()
    {
        if (currTime > FireTime)
        {
            for(int i = 0; i < burnArray.Length; i++)
            {
                if(burnArray[i])
                    burnArray[i].ActiveFireEft();
            }
        }
        if (currTime > SuccessTime)
        {
            StageSuccess();
        }
    }

    public void DeleteObj(GameObject obj)
    {
        print("wgfhkfhslkjgl : " + burnArray.Length);
        for (int i = 0; i < burnArray.Length; i++)
        {
            if (burnArray[i] == null)
                continue;
            if (burnArray[i].gameObject == obj)
            {
                burnArray[i] = null;
                break;
            }
        }
    }
}
