using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    Animator ani;
    public GameObject Flame;


    public LayerMask lay;

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        //베터리가 껴져있고, 손에 들고있다면
        if (GetComponent<KHJ_Item>().isGrab)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
            { 
                ani.SetTrigger("On");
                if (weldingSceneMngr.instance.isBattery)
                {
                    Flame.SetActive(true);
                }
            }
            if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
            {
                if (weldingSceneMngr.instance.isBattery)
                {
                    welding_eft();
                }
            }
            if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
            {
                weldingSceneMngr.instance.isWelding = false;
                ani.SetTrigger("Off");
                Flame.SetActive(false);
                WeldingEft.SetActive(false);
            }
        }
        else
        {
            ani.SetTrigger("Off");
            Flame.SetActive(false);
            WeldingEft.SetActive(false);
        }
        
    }

    public GameObject RayPos; //레이캐스팅을 쏘는 위치
    public GameObject WeldingEft; //충돌하는 위치에 출력할 이펙트
    public GameObject Sphere;
    public Light spherelight;
    void welding_eft()
    {
        //레이캐스팅 결과정보를 hit라는 이름으로 정한다.
        RaycastHit hit;
        spherelight = Sphere.GetComponent<Light>();
        int layer = 1 << LayerMask.NameToLayer("TorchFire");
        //레이캐스트 쏘는 위치, 방향, 결과값, 최대인식거리
        if (Physics.Raycast(RayPos.transform.position, RayPos.transform.forward, out hit, 0.5f, lay))
        {
            if(hit.transform.gameObject.name == "Pipe")
            {
                weldingSceneMngr.instance.isWelding = true;
            }
            WeldingEft.SetActive(true);
            //레이캐스트가 닿은 곳에 오브젝트를 옮긴다.
            WeldingEft.transform.position = hit.point;

            //해당하는 오브젝트의 회전값을 닿은 면적의 노멀방향와 일치시킨다.
            WeldingEft.transform.rotation = Quaternion.LookRotation(hit.normal);

            if (spherelight.enabled == true)
                spherelight.enabled = false;
            else
                spherelight.enabled = true;


            //마스크나 보호구를 착용 안했다면
            if(!weldingSceneMngr.instance.isMask || !weldingSceneMngr.instance.isHelmet)
            {
                //스테이지 실패
                weldingSceneMngr.instance.StageFail(FAIL_INDEX.HELMET);
                weldingSceneMngr.instance.isMask = true;
                weldingSceneMngr.instance.isHelmet = false;
            }
        }
        else
        {
            WeldingEft.SetActive(false);
            weldingSceneMngr.instance.isWelding = false;
        }
    }
}
