using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    Animator ani;
    public GameObject Flame;


    void Start()
    {
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        //���͸��� �����ְ�, �տ� ����ִٸ�
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

    public GameObject RayPos; //����ĳ������ ��� ��ġ
    public GameObject WeldingEft; //�浹�ϴ� ��ġ�� ����� ����Ʈ
    public GameObject Sphere;
    public Light spherelight;
    void welding_eft()
    {
        //����ĳ���� ��������� hit��� �̸����� ���Ѵ�.
        RaycastHit hit;
        spherelight = Sphere.GetComponent<Light>();
        //����ĳ��Ʈ ��� ��ġ, ����, �����, �ִ��νİŸ�
        if (Physics.Raycast(RayPos.transform.position, RayPos.transform.forward, out hit, 0.5f))
        {
            if(hit.transform.gameObject.name == "Pipe")
            {
                weldingSceneMngr.instance.isWelding = true;
            }
            WeldingEft.SetActive(true);
            //����ĳ��Ʈ�� ���� ���� ������Ʈ�� �ű��.
            WeldingEft.transform.position = hit.point;

            //�ش��ϴ� ������Ʈ�� ȸ������ ���� ������ ��ֹ���� ��ġ��Ų��.
            WeldingEft.transform.rotation = Quaternion.LookRotation(hit.normal);

            if (spherelight.enabled == true)
                spherelight.enabled = false;
            else
                spherelight.enabled = true;


            //����ũ�� ��ȣ���� ���� ���ߴٸ�
            if(!weldingSceneMngr.instance.isMask || !weldingSceneMngr.instance.isHelmet)
            {
                //�������� ����
                weldingSceneMngr.instance.StageFail(FAIL_INDEX.HELMET);
            }
        }
        else
        {
            WeldingEft.SetActive(false);
            weldingSceneMngr.instance.isWelding = false;
        }
    }
}
