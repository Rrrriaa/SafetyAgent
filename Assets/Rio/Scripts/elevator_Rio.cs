using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator_Rio : MonoBehaviour
{
    public GameObject player;
    public Transform startPoint;
    public Transform endPoint;
    public GameObject elevator;

    bool isOpen = true;
    bool isDown = true;
    
    public Transform OpenPoint;
    public Transform ClosePoint;
    public GameObject Door;

    public AudioSource Soundplayer;
    public List<AudioClip> clips;

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "UpDown")
        {
            ElevatorUpDown();
        }
        if(gameObject.name == "CloseOpen")
        {
            DoorOpenClose();
        }
    }

    // 위아래로 움직이는 함수
    void ElevatorUpDown()
    {
        //문여는 소리
        Soundplayer.PlayOneShot(clips[1]);
        //1층이면
        if (isDown)
        {
            print("------------충돌처리!!");
            //올라가는 애니메이션 실행
            player.transform.parent = elevator.transform;
            iTween.MoveTo(elevator, iTween.Hash("position", endPoint.position, "time", 18, "easeType", iTween.EaseType.easeOutQuart, "oncompleretarget", gameObject, "oncomplete", "escape"));
            isDown = !isDown;
        }
        else
        {
            //내려가는 애니메이션 실행
            player.transform.parent = elevator.transform;
            iTween.MoveTo(elevator, iTween.Hash("position", startPoint.position, "time", 18, "easeType", iTween.EaseType.easeOutQuart, "oncompleretarget", gameObject, "oncomplete", "escape"));
            isDown = !isDown;
        }
    }

    // itween이 종료 되고 player가 부모로 부터 분리되기 위한 함수
    void escape()
    {
        player.transform.parent = null;
    }

    //문 열고 닫기 함수
    void DoorOpenClose()
    {
        //올라가는 소리
        Soundplayer.PlayOneShot(clips[0]);
        //열려있으면
        if (isOpen)
        {
            iTween.MoveTo(Door, iTween.Hash("position", ClosePoint.position, "time", 3, "easeType", iTween.EaseType.easeOutQuart));
            isOpen = !isOpen;
        }
        //닫혀있으면
        else
        {
            iTween.MoveTo(Door, iTween.Hash("position", OpenPoint.position, "time", 3, "easeType", iTween.EaseType.easeOutQuart));
            isOpen = !isOpen;
        }

    }
}
