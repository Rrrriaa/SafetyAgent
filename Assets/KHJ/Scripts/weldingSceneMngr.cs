using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weldingSceneMngr : MonoBehaviour
{
    public static weldingSceneMngr instance;

    public GameObject Torch;
    public GameObject Battery;
    public bool isBattery;


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
        Test();
    }

    void Update()
    {

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

                Battery.GetComponent<Rigidbody>().isKinematic = true;
                Torch.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    void Test()
    {
        Battery.transform.parent = Torch.transform;
        Battery.transform.position = Torch.transform.position;
        Battery.transform.rotation = Torch.transform.rotation;

        Battery.GetComponent<Rigidbody>().isKinematic = true;
        Torch.GetComponent<Rigidbody>().isKinematic = true;
    }

}
