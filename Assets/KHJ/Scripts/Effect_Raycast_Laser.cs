using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Raycast_Laser : MonoBehaviour
{

    //2#
    public GameObject Raybody; //레이캐스팅을 쏘는 위치
    public GameObject ScaleDistance; //거리에 따른 스케일 변화를 위한 오브젝트 대상
    public GameObject RayResult; //충돌하는 위치에 출력할 결과

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //레이캐스팅 결과정보를 hit라는 이름으로 정한다.
        RaycastHit hit;

        //레이캐스트 쏘는 위치, 방향, 결과값, 최대인식거리
        Physics.Raycast(transform.position, transform.forward, out hit, 200);

        //거리에 따른 레이저 스케일 변화
        ScaleDistance.transform.localScale = new Vector3(1, hit.distance, 1);

        //레이캐스트가 닿은 곳에 오브젝트를 옮긴다.
        RayResult.transform.position = hit.point;

        //해당하는 오브젝트의 회전값을 닿은 면적의 노멀방향와 일치시킨다.
        RayResult.transform.rotation = Quaternion.LookRotation(hit.normal);
    }
}