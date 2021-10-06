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

    // ���Ʒ��� �����̴� �Լ�
    void ElevatorUpDown()
    {
        //������ �Ҹ�
        Soundplayer.PlayOneShot(clips[1]);
        //1���̸�
        if (isDown)
        {
            print("------------�浹ó��!!");
            //�ö󰡴� �ִϸ��̼� ����
            player.transform.parent = elevator.transform;
            iTween.MoveTo(elevator, iTween.Hash("position", endPoint.position, "time", 18, "easeType", iTween.EaseType.easeOutQuart, "oncompleretarget", gameObject, "oncomplete", "escape"));
            isDown = !isDown;
        }
        else
        {
            //�������� �ִϸ��̼� ����
            player.transform.parent = elevator.transform;
            iTween.MoveTo(elevator, iTween.Hash("position", startPoint.position, "time", 18, "easeType", iTween.EaseType.easeOutQuart, "oncompleretarget", gameObject, "oncomplete", "escape"));
            isDown = !isDown;
        }
    }

    // itween�� ���� �ǰ� player�� �θ�� ���� �и��Ǳ� ���� �Լ�
    void escape()
    {
        player.transform.parent = null;
    }

    //�� ���� �ݱ� �Լ�
    void DoorOpenClose()
    {
        //�ö󰡴� �Ҹ�
        Soundplayer.PlayOneShot(clips[0]);
        //����������
        if (isOpen)
        {
            iTween.MoveTo(Door, iTween.Hash("position", ClosePoint.position, "time", 3, "easeType", iTween.EaseType.easeOutQuart));
            isOpen = !isOpen;
        }
        //����������
        else
        {
            iTween.MoveTo(Door, iTween.Hash("position", OpenPoint.position, "time", 3, "easeType", iTween.EaseType.easeOutQuart));
            isOpen = !isOpen;
        }

    }
}
